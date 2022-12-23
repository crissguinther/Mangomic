using System.ComponentModel.DataAnnotations;

namespace Mangomic.Application.DTO.Request {
    public class UserRegisterRequestDTO {
        [Required(ErrorMessage = "The field {0} is required")]
        [EmailAddress(ErrorMessage = "The field {0} is invalid")]
        public string Email { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [StringLength(50, ErrorMessage = "The field must be between {2} and {1} characters long", MinimumLength = 8)]
        public string Password { get; set; }

        [Compare(nameof(Password), ErrorMessage = "Passwords does not match")]
        public string ConfirmPassword { get; set; }
    }
}
