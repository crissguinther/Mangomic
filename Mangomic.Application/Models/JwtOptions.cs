using Microsoft.IdentityModel.Tokens;

namespace Mangomic.Application.Models {
    public class JwtOptions {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int Expiration { get; set; }
        public SigningCredentials SigningCredentials { get; set; }
    }
}
