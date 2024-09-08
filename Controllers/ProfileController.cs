using DieticianApp.Data;
using DieticianApp.Models.Entities;
using DieticianApp.Models.JoinTables;
using DieticianApp.Models.ViewModel.Profile;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;
using System.Security.Claims;

namespace DieticianApp.Controllers
{
    public class ProfileController : Controller
    {
        private readonly AppDbContext _context;

        public ProfileController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Profile()
        {
            // get User claims
            var userClaims = User.Claims;

            var roles = User.FindAll(ClaimTypes.Role).Select(_ => _.Value).ToList();

            if (roles.Contains("Admin"))
            {
                return AdminProfile();
            }
            else if (roles.Contains("Dietician"))
            {
                return DieticianProfile();
            }
            else if (roles.Contains("Patient"))
            {
                return PatientProfile();
            }
            else
            {
               ModelState.AddModelError("", "You don't have permission to this page");
            }
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult AdminProfile() 
        {
            return View("AdminProfile");
        }

        [Authorize(Roles = "Dietician")]
        public IActionResult DieticianProfile()
        {
            return View("DieticianProfile");
        }

        [Authorize(Roles = "Patient")]
        public IActionResult PatientProfile()
        {
            return View("PatientProfile");
        }

        [Authorize(Roles = "Patient")]
        [HttpPost]
        public async Task<IActionResult> SavePatient(PatientProfileViewModel model) 
        {
            if(model != null)
            {
                var userEmail = User.FindFirstValue(ClaimTypes.Email);
                if(userEmail != null)
                {
                    var user = await _context.Users.Where(_ => _.Email == userEmail).FirstOrDefaultAsync();

                    if (user != null)
                    {
                        var patients = new Patients(model.DoB, model.Height, model.Weight, model.Gender);

                        await _context.Patients.AddAsync(patients);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        ModelState.AddModelError("",$"No user found in the database email: {userEmail}");
                    }
                }
            }
            else
            {
                return View(model);
            }

            return View(model);
        }

        
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CompleteProfile(CompleteProfileViewModel model)
        {
            if (model != null)
            {
                var userEmail = User.FindFirstValue(ClaimTypes.Email);
                var user = await _context.Users.Where(_ => _.Email == userEmail).FirstOrDefaultAsync();

                if (user != null) {

                    Patients patient = new Patients(model.DoB, model.Height, model.Weight, model.Gender);
                    

                    user.Patients = patient;
                    await _context.Patients.AddAsync(patient);
                    await _context.SaveChangesAsync();
                    
                }
            }

            return View(model);
        }
    }
}
