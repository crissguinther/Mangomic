using Mangomic.Domain;
using Mangomic.Domain.Enums;

using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Mangomic.Services {

    public interface ITokenService {
        public string GenerateToken(User user);
    }

    public class TokenService : ITokenService {
        private int Hours { get; } = 6;

        public string GenerateToken(User user) {
            var handler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Environment.GetEnvironmentVariable("JWT_SECRET"));

            var descriptor = new SecurityTokenDescriptor {
                Subject = new ClaimsIdentity(new[] {
                    new Claim("Id", user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, Enum.GetName(typeof(UserRoles), user.isAdmin))
                }),
                Expires = DateTime.Now.AddHours(Hours),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            var token = handler.CreateToken(descriptor);
            return handler.WriteToken(token);
        }
    }
}