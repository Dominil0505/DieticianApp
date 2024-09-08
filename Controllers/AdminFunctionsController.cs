using DieticianApp.Data;
using DieticianApp.Models.Entities;
using DieticianApp.Models.JoinTables;
using DieticianApp.Models.ViewModel.AdminFunctions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.Security.Claims;

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

        [Authorize]
        public async Task<IActionResult> Allergy()
        {
            var allergies = await _context.Allergy.ToListAsync();
            var model = new AddAllergyViewModel(allergies);

            return View("Allergy", model);
        }

        [Authorize]
        public async Task<IActionResult> Medicines()
        {
            var medicines = await _context.Medicines.ToListAsync();
            var model = new AddMedicinesViewModel(medicines);

            return View("Medicines", model);
        }

        public async Task<IActionResult> AddPatient()
        {
            var patient = await _context.Patients.ToListAsync();
            var model = new AddPatientViewModel();
            return View("Patient");
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
        public async Task<IActionResult> EditAllergy(AddAllergyViewModel model, int id)
        {
            if (ModelState.IsValid)
            {
                var allergy = await _context.Allergy.FindAsync(id);
                var allergyUsed = await _context.Allergy.Where(_ => _.Allergy_Name == model.AllergyName).FirstOrDefaultAsync();

                if (allergyUsed != null)
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

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> DeleteAllergy(int id)
        {
            var allergy = await _context.Allergy.FindAsync(id);

            if (allergy != null)
            {
                _context.Allergy.Remove(allergy);
                await _context.SaveChangesAsync();
                TempData["MessageDelete"] = $"Successfully deleted {allergy.Allergy_Name}";
            }

            return RedirectToAction("Allergy", "AdminFunctions");
        }


        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddMedicine(AddMedicinesViewModel model)
        {
            if (ModelState.IsValid && model != null)
            {
                var isMedicineUsed = await _context.Medicines.Where(_ => _.Medicine_Name == model.Medicine_Name).FirstOrDefaultAsync();
                
                if (User.IsInRole("Patient"))
                {
                    var userEmail = User.FindFirstValue(ClaimTypes.Email);
                    var user = await _context.Users.Where(_ => _.Email == userEmail).FirstOrDefaultAsync();

                    if (user != null)
                    {
                        if (isMedicineUsed == null)
                        {
                            Medicines medicine = new Medicines(model.Medicine_Name);
                            Patient_Medication patient_medication = new Patient_Medication(user.User_Id, medicine.Medicine_Id);

                            await _context.Medicines.AddAsync(medicine);
                            await _context.patient_Medications.AddAsync(patient_medication);
                            await _context.SaveChangesAsync();
                            TempData["MessageAdded"] = $"{model.Medicine_Name} successfully added!";
                        }
                        else
                        {
                            TempData["MedicineUsed"] = $"{model.Medicine_Name} medicine is already in the database.";
                        }
                    }
                }
                else if (User.IsInRole("Admin"))
                {
                    if (isMedicineUsed == null) 
                    {
                        Medicines medicine = new Medicines(model.Medicine_Name);

                        await _context.Medicines.AddAsync(medicine);
                        await _context.SaveChangesAsync();
                        TempData["MessageAdded"] = $"{model.Medicine_Name} successfully added!";
                    }
                    else
                    {
                        TempData["MedicineUsed"] = $"{model.Medicine_Name} medicine is already in the database.";
                    }
                }               
            }

            var currentMedicines = await _context.Medicines.ToListAsync();
            var viewModelWithErrors = new AddMedicinesViewModel(currentMedicines);
            return View("Medicines", viewModelWithErrors);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> EditMedicine(AddMedicinesViewModel model, int id)
        {
            if (ModelState.IsValid) 
            {
                var medicine = await _context.Medicines.FindAsync(id);
                var medicineUsed = await _context.Medicines.Where(_ => _.Medicine_Name == model.Medicine_Name).FirstOrDefaultAsync();
                if (medicineUsed != null)
                {
                    ModelState.AddModelError("", "Medicine name is used");
                }
                else if (medicine != null) 
                {
                    medicine.Medicine_Name = model.Medicine_Name;
                    _context.Update(medicine);
                    await _context.SaveChangesAsync();
                    TempData["Message"] = $"Successfully updated {model.Medicine_Name}";
                }
            }

            return RedirectToAction("Medicines", "AdminFunctions");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> DeleteMedicine(int id)
        {
            var medicine = await _context.Medicines.FindAsync(id);

            if(medicine != null)
            {
                _context.Medicines.Remove(medicine);
                await _context.SaveChangesAsync();
                TempData["MessageDelete"] = $"Successfully deleted {medicine.Medicine_Name}";
            }

            return RedirectToAction("Medicines");
        }
    }
}
