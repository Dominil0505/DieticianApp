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
using DieticianApp.Models.ViewModel.Profile;
using Humanizer;

namespace DieticianApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;

        public AccountController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Registration()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> CompleteProfile()
        {
            var allergies = await _context.Allergy.ToListAsync();
            var diseases = await _context.Diseases.ToListAsync();
            var medicines = await _context.Medicines.ToListAsync();
            var completedUser = await _context.Users.Where(_ => _.Is_profile_completed == false).FirstOrDefaultAsync();

            var viewModel = new CompleteProfileViewModel
            {
                Allergies = allergies ?? new List<Allergies>(),
                Diseases = diseases ?? new List<Diseases>(),
                Medicines = medicines ?? new List<Medicines>(),
                CompletedUser = completedUser
            };

            return View("CompleteProfile", viewModel);
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
                                if (role != null) 
                                {
                                    Admins admin = new Admins(user.User_Id);
                                    User_Roles user_roles = new User_Roles(user.User_Id, role.Role_Id);

                                    _context.Admins.Add(admin);
                                    _context.User_Roles.Add(user_roles);
                                    _context.SaveChanges();
                                    ModelState.Clear();
                                    ViewBag.Message = $"{user.User_Name} registered successfully. Please login";
                                }
                                else
                                {
                                    ModelState.AddModelError("", "Roles table is empty!");
                                }
                                
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
                        new Claim("Email", user.User_Name)
                    };

                    foreach (var userRole in user.UserRoles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, userRole.Role.Role_Name));
                    }

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                    return RedirectToAction("CompleteProfile");
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
            return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        public async Task<IActionResult> CompleteProfile(CompleteProfileViewModel model)
        {
            if (model != null)
            {
                var userEmail = User.FindFirstValue(ClaimTypes.Email);
                if(userEmail != null) 
                {
                    try
                    {
                        var user = await _context.Users.Where(_ => _.Email == userEmail).FirstOrDefaultAsync();
                        var patient = await _context.Patients.Where(_ => _.User_Id == user.User_Id).FirstOrDefaultAsync();

                        if (patient != null)
                        {

                            patient.DoB = model.DoB;
                            patient.Height = model.Height;
                            patient.Weight = model.Weight;
                            patient.Gender = model.Gender;
                            user.Is_profile_completed = true;

                            _context.Patients.Update(patient);
                            _context.Users.Update(user);


                            if (model.SelectedAllergies != null && model.SelectedAllergies.Any())
                            {
                                foreach (var allergyId in model.SelectedAllergies)
                                {
                                    Patient_Allergies patinet_allergies = new Patient_Allergies(user.Patients.Patient_Id, allergyId);
                                    await _context.Patient_Allergies.AddAsync(patinet_allergies);
                                }
                            }

                            if (model.SelectedDiseases != null && model.SelectedDiseases.Any())
                            {
                                foreach (var diseaseId in model.SelectedDiseases)
                                {
                                    Patient_Disease patinet_disease = new Patient_Disease(user.Patients.Patient_Id, diseaseId);
                                    await _context.Patient_Diseases.AddAsync(patinet_disease);
                                }
                            }

                            if (model.SelectedMedicines != null && model.SelectedMedicines.Any())
                            {
                                foreach (var medicationId in model.SelectedMedicines)
                                {
                                    Patient_Medication patinet_medication = new Patient_Medication(user.Patients.Patient_Id, medicationId);
                                    await _context.patient_Medications.AddAsync(patinet_medication);
                                }
                            }

                            await _context.SaveChangesAsync();
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            ModelState.AddModelError("", $"Patient is null");
                        }
                    }
                    catch (DbUpdateException e)
                    {

                        ModelState.AddModelError("", $"Something went wrong, error: {e.Message}");
                    }
                }
                else
                {
                    ModelState.AddModelError("", $"No user found in the database email: {userEmail}");
                }

            }
           return View(model);
        }

        //private async Task AddPatientInfo<T>(List<int> selectedIds, Func<int, T> createEntity) where T : class
        //{
        //    if(selectedIds != null && selectedIds.Any())
        //    {
        //        foreach(var id in selectedIds)
        //        {
        //            var entity = createEntity(id);
        //            await _context.Set<T>().AddAsync(entity);
        //        }
        //    }
        //}
    }
}
