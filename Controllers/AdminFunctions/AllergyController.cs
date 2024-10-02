using DieticianApp.Data;
using DieticianApp.Models.Entities;
using DieticianApp.Models.ViewModel.AdminFunctions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

namespace DieticianApp.Controllers.AdminFunctions
{
    public class AllergyController : AdminFunctionBaseController
    {
        public AllergyController(AppDbContext context) : base(context)
        {
        }

        [Authorize(Roles = "Admin")]
        [Route("/Allergies")]
        public async Task<IActionResult> ListAllergy(int pageNumber = 1, int pageSize = 10)
        {
            var allergiesQuery = _context.Set<Allergies>().AsQueryable();
            
            var paginatedAllergies = await PaginateAsync(allergiesQuery, pageNumber, pageSize);
            
            var model = new AllergyViewModel
            {
                Allergies = paginatedAllergies.Items,
                CurrentPage = paginatedAllergies.CurrentPage,
                PageSize = paginatedAllergies.PageSize,
                TotalItems = paginatedAllergies.TotalItems
            };

            return View("Views/AdminFunctions/Allergy.cshtml", model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddAllergy(AllergyViewModel model)
        {
            if (ModelState.IsValid)
            {
                bool isAllergyUsed = await EntityExist<Allergies>(_ => _.Allergy_Name == model.AllergyName);

                if (isAllergyUsed)
                {
                    SetTempDataMessage("MessageUsed", $"{model.AllergyName} allergy is already in the database.");
                }
                else
                {
                    try
                    {
                        Allergies allergy = new Allergies(model.AllergyName);
                        await AddEntity(allergy);

                        SetTempDataMessage("MessageAdded", $"{model.AllergyName} successfully added!");
                    }
                    catch (DbUpdateException e)
                    {
                        ModelState.AddModelError("", $"Something went wrong, error: {e.Message}");
                    }
                }
            }

            var allergies = await GetAllEntites<Allergies>();
            var viewModel = new AllergyViewModel(allergies);
            return View("Views/AdminFunctions/Allergy.cshtml", viewModel);
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> EditAllergy(AllergyViewModel model, int id)
        {
            if (ModelState.IsValid)
            {
                bool allergyUsed = await EntityExist<Allergies>(_ => _.Allergy_Name == model.AllergyName);
                var allergy = await _context.Allergy.FindAsync(id);

                if (allergyUsed)
                {
                    SetTempDataMessage("MessageUsed", $"{model.AllergyName} allergy is already in the database!");
                }
                else if (allergy != null)
                {
                    try
                    {
                        allergy.Allergy_Name = model.AllergyName;
                        await UpdateEntity(allergy);
                        SetTempDataMessage("MessageUpdated", $"Successfully updated {model.AllergyName}");
                    }
                    catch (DbUpdateException e)
                    {
                        ModelState.AddModelError("", $"Something went wrong, error: {e.Message}");
                    }
                }
            }

            return RedirectToAction(nameof(ListAllergy));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> DeleteAllergy(int id)
        {
            var allergy = await _context.Allergy.FindAsync(id);

            if (allergy != null)
            {
                await DeleteEntity(allergy);
                SetTempDataMessage("MessageDelete", $"Successfully deleted {allergy.Allergy_Name}");
            }

            return RedirectToAction(nameof(ListAllergy));
        }
    }
}
