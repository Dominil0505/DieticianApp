using DieticianApp.Data;
using DieticianApp.Models.Entities;
using DieticianApp.Models.ViewModel.AdminFunctions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace DieticianApp.Controllers.AdminFunctions
{
    public class PatientController : AdminFunctionBaseController
    {
        public PatientController(AppDbContext context) : base(context)
        {
        }

        [Authorize(Roles = "Admin")]
        [Route("/Patients")]
        public async Task<IActionResult> ListPatient(int pageNumber = 1, int pageSize = 10)
        {
            var user_patient = _context.Patients
                .Include(u => u.User)
                .Where(_ => _.Dietician_Id == null)
                .AsQueryable();

            var dieticians = await _context.Dieticians
                .Include(u => u.Users)
                .ToListAsync();

            var paginatedPatient = await PaginateAsync(user_patient, pageNumber, pageSize);
            var model = new PatientViewModel
            {
                Dieticians = dieticians,
                Patients = paginatedPatient.Items,
                CurrentPage = paginatedPatient.CurrentPage,
                PageSize = paginatedPatient.PageSize,
                TotalItems = paginatedPatient.TotalItems
            };

            return View("Views/AdminFunctions/Patient.cshtml", model);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> SelectDietitan(int id, PatientViewModel model)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var selectedDietitan = await _context.Dieticians
                .Include(u => u.Users)
                .FirstOrDefaultAsync(d => d.Dietician_Id == model.Selected_Dietitan);

            if(selectedDietitan != null)
            {
                var patient = await _context.Patients.FirstOrDefaultAsync(_ => _.Patient_Id == id);
                if (patient != null) 
                {
                    patient.Dietician_Id = model.Selected_Dietitan;
                    patient.DieticianName = selectedDietitan.Users.User_Name;
                    patient.Created_By_Admin_Id = userId;
                    

                    _context.Patients.Update(patient);
                    await _context.SaveChangesAsync();
                    SetTempDataMessage("MessageAdded", "");

                    return View("Views/AdminFunctions/Patient.cshtml");
                }
                else
                {
                    ModelState.AddModelError("", "Something went wrong");
                    return View("Views/AdminFunctions/Patient.cshtml");
                }
            }
            else
            {
                return View("Views/AdminFunctions/Patient.cshtml");
            }

            var patients = await GetAllEntites<Patients>();
            if(patients != null)
            {
                var viewModel = new PatientViewModel(patients);

                return View("Views/AdminFunctions/Patient.cshtml", model);
            }
            else
            {
                return View("Views/AdminFunctions/Patient.cshtml", model);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> DeletePatient(int id)
        {
            var patient = await _context.Patients
                .Include(u => u.User)
                .FirstOrDefaultAsync(_ => _.Patient_Id == id);

            if (patient != null)
            {
                await DeleteEntity(patient);
                SetTempDataMessage("MessageDelete", $"Successfully deleted {patient.User.User_Name}");
            }

            return RedirectToAction(nameof(ListPatient));
        }
    }
}
