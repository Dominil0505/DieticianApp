using DieticianApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using System.Configuration;
using DieticianApp.Models.Entities;
using DieticianApp.Models.JoinTables;
using DieticianApp.Models.ViewModel;

namespace DieticianApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;

        public AccountController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Registration()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Registration(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var isUserUsed = await _context.Users.Where(_ => _.Email == model.Email).FirstOrDefaultAsync();

                if (isUserUsed != null)
                {
                    ModelState.AddModelError("", "The email address is already in use.");
                }
                else
                {
                    if (model.Role == "Dietician" || model.Role == "Patient" || model.Role == "Admin")
                    {
                        Users user = new Users(model.Name, BCrypt.Net.BCrypt.EnhancedHashPassword(model.Password), model.Email);
                        _context.Users.Add(user);
                        _context.SaveChanges();

                        if (model.Role == "Dietician")
                        {
                            try
                            {
                                var role = _context.Roles.Where(_ => _.Role_Name == "Dietician").FirstOrDefault();
                                Dieticians dietician = new Dieticians(user.User_Id);
                                User_Roles user_roles = new User_Roles(user.User_Id, role.Role_Id);

                                _context.Dieticians.Add(dietician);
                                _context.User_Roles.Add(user_roles);
                                _context.SaveChanges();
                                ViewBag.Message = $"{user.User_Name} registered successfully. Please login";
                            }
                            catch(DbUpdateException e) 
                            {
                                ModelState.AddModelError("", $"Something is wrong: ->  {e.Message}");
                            }
                            
                        }
                        else if (model.Role == "Patient")
                        {
                            try
                            {
                                var role = _context.Roles.Where(_ => _.Role_Name == "Patient").FirstOrDefault();
                                Patients patient = new Patients(user.User_Id);
                                User_Roles user_roles = new User_Roles(user.User_Id, role.Role_Id);

                                _context.Patients.Add(patient);
                                _context.User_Roles.Add(user_roles);
                                _context.SaveChanges();
                                ViewBag.Message = $"{user.User_Name} registered successfully. Please login";
                            }
                            catch (DbUpdateException e)
                            {
                                ModelState.AddModelError("", $"Something is wrong: ->  {e.Message}");
                            }
                            
                        }
                        else if (model.Role == "Admin")
                        {
                            try
                            {
                                var role = _context.Roles.Where(_ => _.Role_Name == "Admin").FirstOrDefault();
                                Admins admin = new Admins(user.User_Id);
                                User_Roles user_roles = new User_Roles(user.User_Id, role.Role_Id);

                                _context.Admins.Add(admin);
                                _context.User_Roles.Add(user_roles);
                                _context.SaveChanges();
                                ModelState.Clear();
                                ViewBag.Message = $"{user.User_Name} registered successfully. Please login";
                            }
                            catch (DbUpdateException e)
                            {
                                ModelState.AddModelError("", $"Something is wrong: ->  {e.Message}");
                            }   
                        }
                    }

                    else
                    {
                        ModelState.AddModelError("", "Youre chosen role is not avaliable! Please chosee another one");
                    }

                }

                return View();
            }

            return View(model);
        }

        [HttpPost]
        public async Task <IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {

                var user = await _context.Users
                    .Include(u => u.UserRoles)
                        .ThenInclude(ur => ur.Role)
                    .Where(_ => _.Email == model.Email)
                    .FirstOrDefaultAsync();

                if (user != null && BCrypt.Net.BCrypt.EnhancedVerify(model.Password, user.Password))
                {
                    // Create cookies

                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.User_Name),
                        new Claim("Name", user.User_Name),
                        new Claim(ClaimTypes.Email, user.Email),
                        new Claim("Email", user.User_Name),
                    };

                    foreach (var userRole in user.UserRoles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, userRole.Role.Role_Name));
                    }

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                    return RedirectToAction("SecurePage");
                }
                else
                {
                    ModelState.AddModelError("", "Username or Password is not correct");
                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public IActionResult SecurePage()
        {
            ViewBag.Name = HttpContext.User.Identity.Name;
            return View();
        }


    }
}
