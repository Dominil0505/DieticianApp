using DieticianApp.Data;
using DieticianApp.Models.Entities;
using DieticianApp.Models.ViewModel.AdminFunctions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DieticianApp.Controllers.AdminFunctions
{
    public class DiseaseController : AdminFunctionBaseController
    {
        public DiseaseController(AppDbContext context) : base(context)
        {
        }

        [Authorize(Roles = "Admin")]
        [Route("/Diseases")]
        public async Task<IActionResult> ListDisease(int pageNumber = 1, int pageSize = 10)
        {
            var diseaseQuery = _context.Set<Diseases>().AsQueryable();

            var paginatedDisease = await PaginateAsync(diseaseQuery, pageNumber, pageSize);

            var model = new DiseaseViewModel
            {
                Diseases = paginatedDisease.Items,
                CurrentPage = paginatedDisease.CurrentPage,
                PageSize = paginatedDisease.PageSize,
                TotalItems = paginatedDisease.TotalItems
            };

            return View("Views/AdminFunctions/Disease.cshtml", model);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddDisease(DiseaseViewModel model)
        {
            if (ModelState.IsValid && model != null)
            {
                bool isDiseaseUsed = await EntityExist<Diseases>(_ => _.Disease_Name == model.Disease_Name);

                if (isDiseaseUsed)
                {
                    SetTempDataMessage("MessageUsed", $"{model.Disease_Name} disease is already in the database.");
                }
                else
                {
                    try
                    {
                        Diseases disease = new Diseases(model.Disease_Name);
                        await AddEntity(disease);
                        SetTempDataMessage("MessageAdded", $"{model.Disease_Name} successfully added!");
                        return RedirectToAction(nameof(ListDisease));
                    }
                    catch (DbUpdateException e)
                    {
                        ModelState.AddModelError("", $"Something went wrong, error: {e.Message}");
                    }
                }
            }

            var diseases = await GetAllEntites<Diseases>();
            var viewModel = new DiseaseViewModel(diseases);
            return View("Views/AdminFunctions/Disease.cshtml", viewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditDisease(DiseaseViewModel model, int id)
        {
            if (ModelState.IsValid)
            {
                var disease = await _context.Diseases.FindAsync(id);
                bool isDiseaseUsed = await EntityExist<Diseases>(_ => _.Disease_Name == model.Disease_Name);

                if (isDiseaseUsed)
                {
                    SetTempDataMessage("MessageUsed", $"{model.Disease_Name} disease is already in the database.");
                }
                else if (disease != null)
                {
                    try
                    {
                        disease.Disease_Name = model.Disease_Name;
                        await UpdateEntity(disease);
                        SetTempDataMessage("MessageUpdated", $"Successfully updated {model.Disease_Name}");
                    }
                    catch (DbUpdateException e)
                    {
                        ModelState.AddModelError("", $"Something went wrong, error: {e.Message}");
                    }
                }
            }

            return RedirectToAction(nameof(ListDisease));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> DeleteDisease(int id)
        {
            var disease = await _context.Diseases.FindAsync(id);

            if (disease != null)
            {
                await DeleteEntity(disease);
                SetTempDataMessage("MessageDelete", $"Successfully deleted {disease.Disease_Name}");
            }

            return RedirectToAction(nameof(ListDisease));
        }

    }
}
