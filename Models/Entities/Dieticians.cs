using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DieticianApp.Models.Entities
{
    public class Dieticians
    {
        [Key]
        public int Dietician_Id { get; set; }

        [StringLength(255, MinimumLength = 2)]
        public string? Description { get; set; }

        // Relations
        [ForeignKey("Users")]
        public int? User_Id { get; set; }
        public virtual Users? Users { get; set; }

        public Dieticians(int? user_Id)
        {
            User_Id = user_Id;
        }
    }
}
