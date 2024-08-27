using System.ComponentModel.DataAnnotations;

namespace DieticianApp.Models
{
    public class RegisterViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Full name is Required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password is required")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Password is too short")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords is not match")]
        public string ConfirmPassword { get; set; }

    }
}
