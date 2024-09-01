using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DieticianApp.Models.Entities
{
    public class Admins
    {
        [Key]
        public int Admin_Id { get; set; }

        [ForeignKey("Users")]
        public int User_Id{ get; set; }
        public virtual Users? Users { get; set; }

        public Admins(int user_Id)
        {
            User_Id = user_Id;
        }
    }
}
