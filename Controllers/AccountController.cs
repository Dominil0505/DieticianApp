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
using System.Diagnostics;
using DieticianApp.Models.ViewModel.Patient;

namespace DieticianApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;

        public AccountController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            if (User.IsInRole("Patient"))
            {
                var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

                var patient = await _context.Patients.Where(_ => _.User_Id == userId).FirstOrDefaultAsync();
                
                if(patient.Dietician_Id == null)
                {
                    var dietRole = await _context.Roles.FirstOrDefaultAsync(_ => _.Role_Name == "Dietician");
                    var dietUserRole = await _context.User_Roles.FirstOrDefaultAsync(_ => _.RoleId == dietRole.Role_Id);

                    var dietUser = _context.Users
                    .Where(_ => _.User_Id == dietUserRole.UserId)
                    .ToList();

                    var model = new PendingPatientViewModel(dietUser);
                    return View("Views/Patient/PendingPatient.cshtml", model);
                }
                else
                {
                    return View("Index", "Home");
                }
            }
            else
            {
                return View("Index", "Home");
            }
            
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
            var patients = await _context.Patients.ToListAsync();

            var viewModel = new CompleteProfileViewModel
            {
                Allergies = allergies ?? new List<Allergies>(),
                Diseases = diseases ?? new List<Diseases>(),
                Medicines = medicines ?? new List<Medicines>(),
                Patients = patients ?? new List<Patients>()
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
                                user.Is_profile_completed = true;

                                _context.Dieticians.Add(dietician);
                                _context.User_Roles.Add(user_roles);
                                _context.Users.Update(user);
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
                                    user.Is_profile_completed = true;

                                    _context.Admins.Add(admin);
                                    _context.User_Roles.Add(user_roles);
                                    _context.Users.Update(user);

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
                        new Claim("Email", user.User_Name),
                        // Save ID
                        new Claim(ClaimTypes.NameIdentifier, user.User_Id.ToString())
                    };

                    foreach (var userRole in user.UserRoles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, userRole.Role.Role_Name));
                    }

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                    if(user.Is_profile_completed == true)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        return RedirectToAction(nameof(CompleteProfile));
                    }
                    

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

            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var patient = await _context.Patients.Where(_ => _.User_Id == userId).FirstOrDefaultAsync();
            var user = await _context.Users.FindAsync(userId);

            if (patient != null)
            {
                // Update basic patient details
                patient.DoB = model.DoB;
                patient.Height = model.Height;
                patient.Weight = model.Weight;
                patient.Gender = model.Gender;

                user.Is_profile_completed = true;

                _context.Users.Update(user);
                _context.Patients.Update(patient);

                // Allergies update
                if (model.SelectedAllergies != null && model.SelectedAllergies.Any())
                {
                    foreach (var allergyId in model.SelectedAllergies)
                    {
                        var patientAllergy = new Patient_Allergies(patient.Patient_Id, allergyId);
                        _context.Patient_Allergies.Add(patientAllergy);
                    }
                }

                // Diseases update
                if (model.SelectedDiseases != null && model.SelectedDiseases.Any())
                {
                    foreach (var diseaseId in model.SelectedDiseases)
                    {
                        var patientDisease = new Patient_Disease(patient.Patient_Id, diseaseId);
                        _context.Patient_Diseases.Add(patientDisease);
                    }
                }

                // Medications update
                if (model.SelectedMedicines != null && model.SelectedMedicines.Any())
                {
                    foreach (var medicationId in model.SelectedMedicines)
                    {
                        var patientMedication = new Patient_Medication(patient.Patient_Id, medicationId);
                        _context.patient_Medications.Add(patientMedication);
                    }
                }

                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "No patient information found.");
            }

            return View(model);

        }

    }
}
