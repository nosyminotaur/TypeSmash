using System.ComponentModel.DataAnnotations;

namespace TypeSmash.Web.DTO {
    public class EmailLoginDTO {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Incorrect Email Format")]
        [MaxLength(40, ErrorMessage="Username must be atmost 40 characters long")]
        public string email;

        [Required(ErrorMessage = "Password is required")]
        [MinLength(6, ErrorMessage="Password must be atleast 6 characters long")]
        [MaxLength(40, ErrorMessage="Password must be atmost 40 characters long")]
        public string password;
    }
}