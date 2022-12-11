using BC = BCrypt.Net.BCrypt;

namespace Mangomic.Model {
    public class CreateUserModel {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? PasswordHash { get; set; } = null;

        public CreateUserModel(string username, string email, string password) {
            Username = username;
            Email = email;
            Password = password;
            PasswordHash = BC.HashPassword(Password);
        }
    }
}