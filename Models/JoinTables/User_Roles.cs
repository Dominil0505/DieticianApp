using DieticianApp.Models.Entities;

namespace DieticianApp.Models.JoinTables
{
    public class User_Roles
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }

        // Relations
        public virtual Users? User { get; set; }
        public virtual Roles? Role { get; set; }

        public User_Roles(int userId, int roleId)
        {
            UserId = userId;
            RoleId = roleId;
        }
    }
}
