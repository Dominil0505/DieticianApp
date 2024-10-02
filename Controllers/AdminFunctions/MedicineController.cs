using DieticianApp.Data;
using DieticianApp.Models.Entities;
using DieticianApp.Models.ViewModel.AdminFunctions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DieticianApp.Controllers.AdminFunctions
{
    public class MedicineController : AdminFunctionBaseController
    {
        public MedicineController(AppDbContext context) : base(context)
        {
        }

        [Authorize(Roles = "Admin")]
        [Route("/Medicines")]
        public async Task<IActionResult> ListMedicine(int pageNumber = 1, int pageSize = 10)
        {
            var medicineQuery = _context.Set<Medicines>().AsQueryable();

            var paginatedMedicine = await PaginateAsync(medicineQuery, pageNumber, pageSize);

            var model = new MedicinesViewModel
            {
                Medicines = paginatedMedicine.Items,
                CurrentPage = paginatedMedicine.CurrentPage,
                PageSize = paginatedMedicine.PageSize,
                TotalItems = paginatedMedicine.TotalItems
            };

            return View("Views/AdminFunctions/Medicines.cshtml", model);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddMedicine(MedicinesViewModel model)
        {
            if (ModelState.IsValid && model != null)
            {
                bool isMedicineUsed = await EntityExist<Medicines>(_ => _.Medicine_Name == model.Medicine_Name);

                if (isMedicineUsed)
                {
                    SetTempDataMessage("MessageUsed", $"{model.Medicine_Name} medicine is already in the database!");
                }
                else
                {
                    try
                    {
                        Medicines medicine = new Medicines(model.Medicine_Name);

                        await AddEntity(medicine);
                        SetTempDataMessage("MessageAdded", $"{model.Medicine_Name} successfully added!");
                    }
                    catch (DbUpdateException e)
                    {
                        ModelState.AddModelError("", $"Something went wrong, error: {e.Message}");
                    }
                }
            }

            var medicines = await GetAllEntites<Medicines>();
            var viewModel = new MedicinesViewModel(medicines);
            return View("Views/AdminFunctions/Medicines.cshtml", viewModel);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> EditMedicine(MedicinesViewModel model, int id)
        {
            if (ModelState.IsValid)
            {
                var medicine = await _context.Medicines.FindAsync(id);
                bool medicineUsed = await EntityExist<Medicines>(_ => _.Medicine_Name == model.Medicine_Name);

                if (medicineUsed)
                {
                    SetTempDataMessage("MessageUsed", $"{model.Medicine_Name} medicine is already in the database!");
                }
                else if (medicine != null)
                {
                    medicine.Medicine_Name = model.Medicine_Name;
                    await UpdateEntity(medicine);
                    SetTempDataMessage("MessageUpdated", $"Successfully updated {model.Medicine_Name}");
                }
            }

            return RedirectToAction(nameof(ListMedicine));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> DeleteMedicine(int id)
        {
            var medicine = await _context.Medicines.FindAsync(id);

            if (medicine != null)
            {
                await DeleteEntity(medicine);
                SetTempDataMessage("MessageDelete", $"Successfully deleted {medicine.Medicine_Name}");
            }

            return RedirectToAction(nameof(ListMedicine));
        }
    }
}
