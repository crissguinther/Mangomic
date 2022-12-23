using BC = BCrypt.Net.BCrypt;

namespace Mangomic.Services.Helpers {
    public static class PasswordHash {
        public static string HashPassword(string password) {
            return BC.HashPassword(password);
        }

        public static bool VerifyPassword(string password, string hash) {
            return BC.Verify(password, hash);
        }
    }
}
