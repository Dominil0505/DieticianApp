using DieticianApp.Data;
using DieticianApp.Models.Entities;
using DieticianApp.Models.ViewModel.AdminFunctions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DieticianApp.Controllers.AdminFunctions
{
    public class IngredientController : AdminFunctionBaseController
    {
        public IngredientController(AppDbContext context) : base(context)
        {
        }

        [Authorize(Roles = "Admin")]
        [Route("/Ingredients")]
        public async Task<IActionResult> ListIngredient(int pageNumber = 1, int pageSize = 10)
        {
            var ingredientQuery = _context.Set<Ingredient>().AsQueryable();
            var paginatedIngredient = await PaginateAsync(ingredientQuery, pageNumber, pageSize);

            var model = new IngredientsViewModel
            {
                Ingredient = paginatedIngredient.Items,
                CurrentPage = paginatedIngredient.CurrentPage,
                PageSize = paginatedIngredient.PageSize,
                TotalItems = paginatedIngredient.TotalItems
            };

            return View("Views/AdminFunctions/Ingredients.cshtml", model);
        }




        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddIngredient(IngredientsViewModel model)
        {
            if (ModelState.IsValid && model != null)
            {
                bool ingredientUsed = await EntityExist<Ingredient>(_ => _.Ingredient_Name == model.Ingredient_Name);
                if (ingredientUsed)
                {
                    SetTempDataMessage("MessageUsed", $"{model.Ingredient_Name} ingredient is already in the database.");
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
            var viewModel = new IngredientsViewModel(ingredients);
            return View("Views/AdminFunctions/Ingredients.cshtml", viewModel);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> EditIngredient(IngredientsViewModel model, int id)
        {
            if (ModelState.IsValid)
            {
                bool ingredientUsed = await EntityExist<Ingredient>(_ => _.Ingredient_Name == model.Ingredient_Name);
                var ingredient = await _context.Ingredients.FindAsync(id);

                if (ingredientUsed)
                {
                    SetTempDataMessage("MessageUsed", $"{model.Ingredient_Name} ingredient is already in the database.");
                }
                else if (ingredient != null)
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
            else if (model.Ingredient_Name == null)
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

            return RedirectToAction(nameof(ListIngredient));
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

            return RedirectToAction(nameof(ListIngredient));

        }
    }
}
