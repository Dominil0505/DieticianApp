using DieticianApp.Models.JoinTables;
using System.ComponentModel.DataAnnotations;

namespace DieticianApp.Models.Entities
{
    public class Roles
    {
        [Key]
        public int Role_Id { get; set; }

        public string? Role_Name { get; set; }

        public virtual ICollection<User_Roles>? Users { get; set; } = new List<User_Roles>();
    }
}
