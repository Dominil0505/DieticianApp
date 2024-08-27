﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace DieticianApp.Entities
{
    [Index(nameof(Ingredient_Name), IsUnique = true)]
    public class Ingredient
    {
        [Key]
        public int Ingredient_Id{ get; set; }

        [Required(ErrorMessage = "Ingredient name is required")]
        [StringLength(255, MinimumLength = 3, ErrorMessage = "Ingredient name minimum length is 3")]
        public string? Ingredient_Name{ get; set; }

        [Required(ErrorMessage = "Calorie is required")]
        public int? Calorie{ get; set; }

        [Required(ErrorMessage = "Proteint is required")]
        public int? Protein { get; set; }

        [Required(ErrorMessage = "Fat is required")]
        public int? Fat { get; set; }

        [Required(ErrorMessage = "Carbohydrate is required")]
        public int? Carbohydrate { get; set; }

        // Relation
        public ICollection<Food>? Foods { get; set; }
    }
}

