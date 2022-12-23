using System.ComponentModel.DataAnnotations;

namespace Mangomic.Application.DTO.Request {
    public class UserLoginRequestDTO {
        [Required(ErrorMessage = "The field {0} is required")]
        [EmailAddress(ErrorMessage = "The field {0} must be a valid password")]
        public string Email { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        public string Password { get; set; }
    }
}
