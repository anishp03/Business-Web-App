using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TechTickles.Models
{
    public class LoginViewModel
    {

        [Required(ErrorMessage = "Username or Email Required")]
        [StringLength(60, MinimumLength = 4, ErrorMessage = "Max 60 or 4 min characters required")]
        [DisplayName("Username or Email")]
        public String UserNameOrEmail { get; set; }

        [Required(ErrorMessage = "Password Required")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Max 20 or 6 min characters required")]
        [DataType(DataType.Password)]
        public String Password { get; set; }
    }
}
