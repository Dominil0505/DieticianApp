using DieticianApp.Data;
using DieticianApp.Models.Entities;
using DieticianApp.Models.ViewModel.AdminFunctions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

namespace DieticianApp.Controllers
{
    public class AdminFunctionsController : Controller
    {
        private readonly AppDbContext _context;

        public AdminFunctionsController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Allergy()
        {
            var allergies = await _context.Allergy.ToListAsync();
            var model = new AddAllergyViewModel(allergies);

            return View("Allergy", model);
        }

        public async Task<IActionResult> AddPatient()
        {
            var patient = await _context.Patients.ToListAsync();
            var model = new AddPatientViewModel();
            return View("AddPatient");
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddAllergy(AddAllergyViewModel model)
        {
            if (ModelState.IsValid)
            {
                var isAllergyUsed = await _context.Allergy
                    .Where(_ => _.Allergy_Name == model.AllergyName)
                    .FirstOrDefaultAsync();

                if (isAllergyUsed == null)
                {
                    try
                    {
                        Allergies allergy = new Allergies(model.AllergyName);
                        _context.Allergy.Add(allergy);
                        await _context.SaveChangesAsync();

                        TempData["MessageAdded"] = $"Successfully added {model.AllergyName}";

                        var allergies = await _context.Allergy.ToListAsync();
                        var viewModel = new AddAllergyViewModel(allergies);
                        return RedirectToAction("Allergy", "AdminFunctions");
                    }
                    catch (DbUpdateException e)
                    {
                        ModelState.AddModelError("", $"Something went wrong, error: {e.Message}");
                    }
                }
                else
                {
                    TempData["AllergyUsed"] = $"{model.AllergyName} allergy is already in the database.";
                }
            }

            var currentAllergies = await _context.Allergy.ToListAsync();
            var viewModelWithErrors = new AddAllergyViewModel(currentAllergies);
            return View("Allergy", viewModelWithErrors);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> DeleteAllergy(int id)
        {
            var allergy = await _context.Allergy.FindAsync(id);

            if(allergy != null)
            {
                _context.Allergy.Remove(allergy);
                await _context.SaveChangesAsync();
                TempData["MessageDelete"] = $"Successfully deleted {allergy.Allergy_Name}";
            }

            return RedirectToAction("Allergy", "AdminFunctions");
        }

        [Authorize(Roles="Admin")]
        [HttpPost]
        public async Task<IActionResult> EditAllergy(AddAllergyViewModel model, int id)
        {
            if(ModelState.IsValid)
            {
                var allergy = await _context.Allergy.FindAsync(id);
                var allergyUsed = await _context.Allergy.Where(_ => _.Allergy_Name == model.AllergyName).FirstOrDefaultAsync();

                if(allergyUsed != null)
                {
                    ModelState.AddModelError("", "This allergy name is already used");
                }
                else
                {
                    if (allergy != null)
                    {
                        allergy.Allergy_Name = model.AllergyName;
                        _context.Allergy.Update(allergy);
                        await _context.SaveChangesAsync();
                        TempData["Message"] = $"Successfully updated {model.AllergyName}";
                    }
                }
            }

            return RedirectToAction("Allergy", "AdminFunctions");
        }
    }
}
