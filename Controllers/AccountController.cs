using DieticianApp.Data;
using Microsoft.AspNetCore.Mvc;
using DieticianApp.Entities;
using DieticianApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

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

        public IActionResult RegistrationDietician()
        {
            return View();
        }

        public IActionResult LoginDietician()
        {
            return View();
        }

        public IActionResult RegistrationPatient()
        {
            return View();
        }

        public IActionResult LoginPatient()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RegistrationDietician(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                
                Dietician dietician = new Dietician();
                dietician.Dietician_Email = model.Email;
                dietician.Dietician_Name = model.Name;
                dietician.Password = BCrypt.Net.BCrypt.EnhancedHashPassword(model.Password);

                if(dietician.Password is null)
                {
                    ModelState.AddModelError("", "The password is null.");
                }
                try
                {
                    _context.Dieticians.Add(dietician);
                    _context.SaveChanges();

                    ModelState.Clear();
                    ViewBag.Message = $"{dietician.Dietician_Name} registered successfully. Please login";
                }
                catch (DbUpdateException ex)
                {
                    var user = _context.Dieticians.Where(_ => _.Dietician_Email == model.Email).FirstOrDefault();
                    if(user is not null) 
                    { 
                        ModelState.AddModelError("", "The email address is already in use."); 
                    }
                    else
                    {
                        ModelState.AddModelError("", "An error occurred while processing your request.\n" + ex.Message);
                    }
                    //ModelState.AddModelError("", "Please enter unique Email");
                    return View(model);
                }

                return View();
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult LoginDietician(LoginViewModel model) 
        {
            if(ModelState.IsValid)
            {
                var dietician = _context.Dieticians.Where(_ => _.Dietician_Email == model.Email).FirstOrDefault();

                if(dietician is not null && BCrypt.Net.BCrypt.EnhancedVerify(model.Password, dietician.Password))
                {
                    // Create cookies

                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, dietician.Dietician_Name),
                        new Claim("Name", dietician.Dietician_Name),
                        new Claim(ClaimTypes.Role, "Dietician")
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

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
        public IActionResult RegistrationPatient(RegisterViewModel model)
        {
            if (ModelState.IsValid) 
            {
                Patient patient = new Patient();
                patient.Patient_Email = model.Email;
                patient.Patient_Name = model.Name;
                patient.Password = BCrypt.Net.BCrypt.EnhancedHashPassword(model.Password);


                try
                {
                    _context.Add(patient);
                    _context.SaveChanges();

                    ModelState.Clear();
                    ViewBag.Message = $"{patient.Patient_Name} registered successfully. Please login";
                }
                catch (DbUpdateException ex)
                {
                    var user = _context.Patients.Where(_ => _.Patient_Email == model.Email).FirstOrDefault();
                    if (user is not null)
                    {
                        ModelState.AddModelError("", "The email address is already in use.");
                    }
                    else
                    {
                        ModelState.AddModelError("", "An error occurred while processing your request.\n" + ex.Message);
                    }
                    //ModelState.AddModelError("", "Please enter unique Email");
                    return View(model);
                }
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult LoginPatient(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var patient = _context.Patients.Where(_ => _.Patient_Email == model.Email).FirstOrDefault();

                if(patient is not null && BCrypt.Net.BCrypt.EnhancedVerify(model.Password, patient.Password))
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, patient.Patient_Name),
                        new Claim("Name", patient.Patient_Name),
                        new Claim(ClaimTypes.Role, "Patient")
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                    return RedirectToAction("");
                }
                else
                {
                    ModelState.AddModelError("", "Username or Password is not correct");
                }
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult Logout() 
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
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
