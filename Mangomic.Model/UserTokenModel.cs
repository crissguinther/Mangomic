using Mangomic.Domain;
using System.ComponentModel.DataAnnotations;

namespace Mangomic.Model {
    public class UserTokenModel {
        [Required]
        public string Email { get; }
        [Required]
        public string Password { get; }

        public UserTokenModel(string email, string password) {
            Email = email;
            Password = password;
        }
    }
}
