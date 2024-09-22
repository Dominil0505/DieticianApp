using DieticianApp.Data;
using DieticianApp.Models.Entities;
using DieticianApp.Models.JoinTables;
using DieticianApp.Models.ViewModel.AdminFunctions;
using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.Linq.Expressions;
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

        [Authorize(Roles = "Admin")]
        [Route("/Allergies")]
        public async Task<IActionResult> Allergy()
        {
            var allergies = await GetAllEntites<Allergies>();
            var model = new AddAllergyViewModel(allergies);

            return View("Allergy", model);
        }

        [Authorize(Roles = "Admin")]
        [Route("/Medicines")]
        public async Task<IActionResult> Medicines()
        {
            var medicines = await GetAllEntites<Medicines>();
            var model = new AddMedicinesViewModel(medicines);

            return View("Medicines", model);
        }

        [Authorize(Roles = "Admin")]
        [Route("/Patient")]
        public async Task<IActionResult> Patient()
        {
            var patients = await GetAllEntites<Patients>();
            var model = new AddPatientViewModel();
            return View("Patient", model);
        }

        [Authorize(Roles = "Admin")]
        [Route("/Disease")]
        public async Task<IActionResult> Diseases()
        {
            var disease = await GetAllEntites<Diseases>();
            var model = new AddDiseaseViewModel(disease);
            return View("Diseases", model);
        }

        [Authorize(Roles = "Admin")]
        [Route("/Ingredients")]
        public async Task<IActionResult> Ingredients()
        {
            var ingredient = await GetAllEntites<Ingredient>();
            var model = new AddIngredientsViewModel(ingredient);
            return View("Ingredients", model);
        }

        [Authorize(Roles = "Admin")]
        [Route("/Foods")]
        public async Task<IActionResult> Foods(){

            var food = await GetAllEntites<Foods>();
            var ingredeint = await GetAllEntites<Ingredient>() ?? new List<Ingredient>();
            var allergy = await GetAllEntites<Allergies>() ?? new List<Allergies>();

            List<Food_Allergies> getFoodAllergy = new List<Food_Allergies>();
            List<Food_Ingredients> getFoodIngredient = new List<Food_Ingredients>();

            foreach (var foods in food)
            {
                getFoodAllergy = await _context.Food_Allergies.Where(_ => _.FoodId == foods.Food_Id).ToListAsync();
                getFoodIngredient = await _context.Food_Ingredients.Where(_ => _.FoodId == foods.Food_Id).ToListAsync();
            }

            var model = new AddFoodsViewModel
            {
                Ingredients = ingredeint ?? new List<Ingredient>(),
                Foods = food ?? new List<Foods>(),
                Allergies = allergy ?? new List<Allergies>(),
                GetFood_Allergies = getFoodAllergy,
                GetFood_Ingredients = getFoodIngredient
            };

            return View("Foods", model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddAllergy(AddAllergyViewModel model)
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
                        return RedirectToAction("Allergy", "AdminFunctions");
                    }
                    catch (DbUpdateException e)
                    {
                        ModelState.AddModelError("", $"Something went wrong, error: {e.Message}");
                    }
                }
            }

            var allergies = await GetAllEntites<Allergies>();
            var viewModel = new AddAllergyViewModel(allergies);
            return View("Allergy", viewModel);
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> EditAllergy(AddAllergyViewModel model, int id)
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

            return RedirectToAction("Allergy", "AdminFunctions");
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

            return RedirectToAction("Allergy", "AdminFunctions");
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddMedicine(AddMedicinesViewModel model)
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
            var viewModel = new AddMedicinesViewModel(medicines);
            return View("Medicines", viewModel);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> EditMedicine(AddMedicinesViewModel model, int id)
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

            return RedirectToAction("Medicines", "AdminFunctions");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> DeleteMedicine(int id)
        {
            var medicine = await _context.Medicines.FindAsync(id);

            if(medicine != null)
            {
                await DeleteEntity(medicine);
                SetTempDataMessage("MessageDelete", $"Successfully deleted {medicine.Medicine_Name}");
            }

            return RedirectToAction("Medicines");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddDisease(AddDiseaseViewModel model)
        {
            if (ModelState.IsValid && model != null)
            {
                bool isDiseaseUsed = await EntityExist<Diseases>(_ => _.Disease_Name == model.Disease_Name);

                if(isDiseaseUsed)
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
                    }
                    catch (DbUpdateException e)
                    {
                        ModelState.AddModelError("", $"Something went wrong, error: {e.Message}");
                    }
                }
            }

            var diseases = await GetAllEntites<Diseases>();
            var viewModel = new AddDiseaseViewModel(diseases);
            return View("Diseases", viewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditDisease(AddDiseaseViewModel model, int id)
        {
            if (ModelState.IsValid)
            {
                var disease = await _context.Diseases.FindAsync(id);
                bool isDiseaseUsed = await EntityExist<Diseases>(_ => _.Disease_Name == model.Disease_Name);

                if (isDiseaseUsed){
                    SetTempDataMessage("MessageUsed", $"{model.Disease_Name} disease is already in the database.");
                }
                else if(disease != null)
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

            return RedirectToAction("Diseases", "AdminFunctions");
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

            return RedirectToAction("Diseases", "AdminFunctions");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddIngredient(AddIngredientsViewModel model)
        {
            if(ModelState.IsValid && model != null)
            {
                bool ingredientUsed = await EntityExist<Ingredient>(_ => _.Ingredient_Name == model.Ingredient_Name);
                if(ingredientUsed)
                {
                    SetTempDataMessage("MessageUsed", $"{model.Ingredient_Name} disease is already in the database.");
                }
                else
                {
                    try
                    {
                        Ingredient ingredient = new Ingredient(model.Ingredient_Name, model.Calorie, model.Protein, model.Fat, model.Carbohydrate);
                        await AddEntity(ingredient);
                        SetTempDataMessage("MessageAdded", $"{model.Ingredient_Name} successfully added!");

                    }
                    catch (DbUpdateException e)
                    {
                        ModelState.AddModelError("", $"Something went wrong, error: {e.Message}");
                    }
                }
            }

            var ingredients = await GetAllEntites<Ingredient>();
            var viewModel = new AddIngredientsViewModel(ingredients);
            return View("Ingredients", viewModel);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> EditIngredient(EditIngredientViewModel model, int id)
        {
            if (ModelState.IsValid)
            {
                bool ingredientUsed = await EntityExist<Ingredient>(_ => _.Ingredient_Name == model.Ingredient_Name);
                var ingredient = await _context.Ingredients.FindAsync(id);

                if (ingredientUsed)
                {
                    SetTempDataMessage("MessageUsed", $"{model.Ingredient_Name} food is already in the database.");
                }
                else if(ingredient != null)
                {
                    
                    try
                    {
                        ingredient.Ingredient_Name = model.Ingredient_Name;
                        ingredient.Calorie = model.Calorie;
                        ingredient.Protein = model.Protein;
                        ingredient.Fat = model.Fat;
                        ingredient.Carbohydrate = model.Carbohydrate;
                        await UpdateEntity(ingredient);
                        SetTempDataMessage("MessageUpdated", $"{model.Ingredient_Name} successfully updated!");

                    }
                    catch (DbUpdateException e)
                    {
                        ModelState.AddModelError("", $"Something went wrong, error: {e.Message}");
                    }
                }
            }
            else if(model.Ingredient_Name == null)
            {
                var ingredient = await _context.Ingredients.FindAsync(id);
                if (ingredient != null)
                {

                    try
                    {
                        ingredient.Calorie = model.Calorie;
                        ingredient.Protein = model.Protein;
                        ingredient.Fat = model.Fat;
                        ingredient.Carbohydrate = model.Carbohydrate;
                        await UpdateEntity(ingredient);
                        SetTempDataMessage("MessageUpdated", $"{ingredient.Ingredient_Name} successfully updated!");

                    }
                    catch (DbUpdateException e)
                    {
                        ModelState.AddModelError("", $"Something went wrong, error: {e.Message}");
                    }
                }
            }

            return RedirectToAction("Ingredients", "AdminFunctions");
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteIngredient(int id)
        {
            var ingredient = await _context.Ingredients.FindAsync(id);
            if (ingredient != null)
            {
                await DeleteEntity(ingredient);
                SetTempDataMessage("MessageDelete", $"Successfully deleted {ingredient.Ingredient_Name}");
            }

            return RedirectToAction("Ingredients", "AdminFunctions");

        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddFood(AddFoodsViewModel model)
        {
            if (ModelState.IsValid)
            {
                bool isFoodUsed = await EntityExist<Foods>(_ => _.Food_Name == model.Food_Name);

                if (isFoodUsed)
                {
                    SetTempDataMessage("MessageUsed", $"{model.Food_Name} food is already in the database.");
                }
                else
                {
                    try
                    {
                        Foods food = new Foods(model.Food_Name, model.Calorie, model.Protein, model.Fat,model.Carbohydrate);
                        await AddEntity(food);

                        if(model.SelectedIngredients != null && model.SelectedIngredients.Any())
                        {
                            foreach(var selectedIngredientId  in model.SelectedIngredients)
                            {
                                Food_Ingredients food_Ingredients = new Food_Ingredients(food.Food_Id, selectedIngredientId);
                                await _context.Food_Ingredients.AddAsync(food_Ingredients);
                            }
                        }

                        if(model.SelectedAllergies != null && model.SelectedAllergies.Any())
                        {
                            foreach(var selectedAllergyId in model.SelectedAllergies)
                            {
                                Food_Allergies food_allergies = new Food_Allergies(food.Food_Id, selectedAllergyId);
                                await _context.Food_Allergies.AddAsync(food_allergies);
                            }
                        }

                        await _context.SaveChangesAsync();

                        SetTempDataMessage("MessageAdded", $"{model.Food_Name} successfully added!");
                        return RedirectToAction("Foods", "AdminFunctions");
                    }
                    catch (DbUpdateException e)
                    {
                        ModelState.AddModelError("", $"Something went wrong, error: {e.Message}");
                    }
                }
            }

            var foods = await GetAllEntites<Foods>();
            var viewModel = new AddFoodsViewModel(foods);
            return View("Foods", viewModel);
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> EditFood(AddFoodsViewModel model, int id)
        {
            if (ModelState.IsValid)
            {
                bool isFoodUsed = await EntityExist<Foods>(_ => _.Food_Name == model.Food_Name);
                var food = await _context.Foods.FindAsync(id);

                if (isFoodUsed)
                {
                    SetTempDataMessage("MessageUsed", $"{model.Food_Name} allergy is already in the database!");
                }
                else if (food != null)
                {
                    try
                    {
                        food.Food_Name = model.Food_Name;
                        food.Calorie = model.Calorie;
                        food.Protein = model.Protein;
                        food.Fat = model.Fat;
                        food.Carbohydrate = model.Carbohydrate;

                        await UpdateEntity(food);
                        SetTempDataMessage("MessageUpdated", $"Successfully updated {model.Food_Name}");
                    }
                    catch (DbUpdateException e)
                    {
                        ModelState.AddModelError("", $"Something went wrong, error: {e.Message}");
                    }
                }
            }
            else if(model.Food_Name == null)
            {
                try
                {
                    var food = await _context.Foods.FindAsync(id);

                    food.Calorie = model.Calorie;
                    food.Protein = model.Protein;
                    food.Fat = model.Fat;
                    food.Carbohydrate = model.Carbohydrate;

                    await UpdateEntity(food);
                    SetTempDataMessage("MessageUpdated", $"Successfully updated {food.Food_Name}");
                }
                catch (DbUpdateException e)
                {
                    ModelState.AddModelError("", $"Something went wrong, error: {e.Message}");
                }
            }

            return RedirectToAction("Foods", "AdminFunctions");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> DeleteFood(int id)
        {
            var food = await _context.Foods.FindAsync(id);

            if (food != null)
            {
                await DeleteEntity(food);
                SetTempDataMessage("MessageDelete", $"Successfully deleted {food.Food_Name}");
            }

            return RedirectToAction("Foods", "AdminFunctions");
        }

        private async Task<List<T>> GetAllEntites<T>() where T : class
        {
            return await _context.Set<T>().ToListAsync();
        }

        private async Task<bool> EntityExist<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return await _context.Set<T>().AnyAsync(predicate);
        }

        private async Task AddEntity<T>(T entity) where T : class
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        private async Task UpdateEntity<T>(T entity) where T : class
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
        }

        private async Task DeleteEntity<T>(T entity) where T : class
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        private void SetTempDataMessage(string messageType, string message)
        {
            TempData[messageType] = message;
        }


    }
}
