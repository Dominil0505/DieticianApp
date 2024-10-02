using DieticianApp.Data;
using DieticianApp.Models.Entities;
using DieticianApp.Models.JoinTables;
using DieticianApp.Models.ViewModel.AdminFunctions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DieticianApp.Controllers.AdminFunctions
{
    public class FoodController : AdminFunctionBaseController
    {
        public FoodController(AppDbContext context) : base(context)
        {
        }

        [Authorize(Roles = "Admin")]
        [Route("/Foods")]
        public async Task<IActionResult> ListFood()
        {

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

            var model = new FoodsViewModel
            {
                Ingredients = ingredeint ?? new List<Ingredient>(),
                Foods = food ?? new List<Foods>(),
                Allergies = allergy ?? new List<Allergies>(),
                GetFood_Allergies = getFoodAllergy,
                GetFood_Ingredients = getFoodIngredient
            };

            return View("Views/AdminFunctions/Foods.cshtml", model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddFood(FoodsViewModel model)
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
                        Foods food = new Foods(model.Food_Name, model.Calorie, model.Protein, model.Fat, model.Carbohydrate);
                        await AddEntity(food);

                        if (model.SelectedIngredients != null && model.SelectedIngredients.Any())
                        {
                            foreach (var selectedIngredientId in model.SelectedIngredients)
                            {
                                Food_Ingredients food_Ingredients = new Food_Ingredients(food.Food_Id, selectedIngredientId);
                                await _context.Food_Ingredients.AddAsync(food_Ingredients);
                            }
                        }

                        if (model.SelectedAllergies != null && model.SelectedAllergies.Any())
                        {
                            foreach (var selectedAllergyId in model.SelectedAllergies)
                            {
                                Food_Allergies food_allergies = new Food_Allergies(food.Food_Id, selectedAllergyId);
                                await _context.Food_Allergies.AddAsync(food_allergies);
                            }
                        }

                        await _context.SaveChangesAsync();

                        SetTempDataMessage("MessageAdded", $"{model.Food_Name} successfully added!");
                        return RedirectToAction(nameof(ListFood));
                    }
                    catch (DbUpdateException e)
                    {
                        ModelState.AddModelError("", $"Something went wrong, error: {e.Message}");
                    }
                }
            }

            var foods = await GetAllEntites<Foods>();
            var viewModel = new FoodsViewModel(foods);
            return View("Views/AdminFunctions/Foods.cshtml", viewModel);
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> EditFood(FoodsViewModel model, int id)
        {
            if (ModelState.IsValid)
            {
                bool isFoodUsed = await EntityExist<Foods>(_ => _.Food_Name == model.Food_Name);
                var food = await _context.Foods.FindAsync(id);

                if (isFoodUsed)
                {
                    SetTempDataMessage("MessageUsed", $"{model.Food_Name} food is already in the database!");
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
            else if (model.Food_Name == null)
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

            return RedirectToAction(nameof(ListFood));
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

            return RedirectToAction(nameof(ListFood));
        }
    }
}
