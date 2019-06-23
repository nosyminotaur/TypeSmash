using System.ComponentModel.DataAnnotations;

namespace TypeSmash.Web.DTO
{
    public class SignupDTO
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Incorrect Email Format")]
        [MaxLength(40, ErrorMessage="Username must be atmost 40 characters long")]
        public string email;

        [Required(ErrorMessage = "Username is required")]
        [RegularExpression(@"^[-_.A-Za-z0-9]+$", ErrorMessage = "Only underscore, dash and period special characters allowed in Username")]
        [MinLength(6, ErrorMessage="Username must be atleast 6 characters long")]
        [MaxLength(40, ErrorMessage="Username must be atmost 40 characters long")]
        public string username;

        [Required(ErrorMessage = "Password is required")]
        [MinLength(6, ErrorMessage="Password must be atleast 6 characters long")]
        [MaxLength(40, ErrorMessage="Password must be atmost 40 characters long")]
        public string password;
    }
}