using DieticianApp.Data;
using DieticianApp.Models.ViewModel.Profile;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace DieticianApp.Controllers
{
    public class ProfileController : Controller
    {
        private readonly AppDbContext _context;

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
        public async Task<IActionResult> EditPatient(PatientProfileViewModel model) 
        {
            if(model != null)
            {
                var userEmail = User.FindFirstValue(ClaimTypes.Email);
                if(userEmail != null)
                {
                    var patient = _context.Users
                        .Include(u => u.Patients)
                        .FirstOrDefault(u => u.Email == userEmail)?.Patients;
                    
                    patient.User.User_Name = model.Username;
                    patient.DoB = model.DoB;
                    patient.Height = model.Height;
                    patient.Weight = model.Weight;
                    patient.Gender = model.Gender;
                    
                }
                
            }
            else
            {
                return View(model);
            }

            return View(model);
        }

    }
}
