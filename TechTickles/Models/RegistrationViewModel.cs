using System.ComponentModel.DataAnnotations;

namespace TechTickles.Models
{
    public class RegistrationViewModel
    {
        [Required(ErrorMessage = "First Name Required")]
        public String FirstName { get; set; }

        [Required(ErrorMessage = "Last Name Required")]
        public String LastName { get; set; }

        [Required(ErrorMessage = "Email Required")]
        [StringLength(60, MinimumLength = 6, ErrorMessage = "Max 60 or 6 min characters required")]
        [RegularExpression(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Valid Email Required")]
        public String Email { get; set; }

        [Required(ErrorMessage = "Username Required")]
        [StringLength(30, MinimumLength = 4, ErrorMessage = "Max 30 or 4 min characters required")]
        public String UserName { get; set; }

        [Required(ErrorMessage = "Password Required")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Max 20 or 6 min characters required")]
        [DataType(DataType.Password)]
        public String Password { get; set; }

        [Compare("Password", ErrorMessage = "Password Confirmation Required")]
        [DataType(DataType.Password)]
        public String ConfirmPassword { get; set; }
    }
}
