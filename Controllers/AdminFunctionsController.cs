using DieticianApp.Data;
using DieticianApp.Models.Entities;
using DieticianApp.Models.ViewModel.AdminFunctions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddAllergy(AddAllergyViewModel model)
        {
            if (ModelState.IsValid)
            {
                var isAllergyUsed = await _context.Allergy
                    .Where(a => a.Allergy_Name == model.AllergyName)
                    .FirstOrDefaultAsync();

                if (isAllergyUsed == null) // Ha az allergia még nem létezik
                {
                    try
                    {
                        // Új allergia létrehozása és mentése
                        Allergies allergy = new Allergies(model.AllergyName);
                        _context.Allergy.Add(allergy);
                        await _context.SaveChangesAsync();

                        ViewBag.Message = $"Successfully added {model.AllergyName}";

                        var allergies = await _context.Allergy.ToListAsync();
                        var viewModel = new AddAllergyViewModel(allergies);
                        return View("Allergy", viewModel);
                    }
                    catch (DbUpdateException e)
                    {
                        ModelState.AddModelError("", $"Something went wrong, error: {e.Message}");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "This allergy is already in the database.");
                }
            }

            // Ha a ModelState nem érvényes, visszaadjuk a modellt a nézetnek
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
                TempData["Message"] = $"Successfully deleted {allergy.Allergy_Name}";
            }

            return RedirectToAction("Allergy", "AdminFunctions");
        }
    }
}
