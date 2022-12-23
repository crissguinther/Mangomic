using Mangomic.Application.DTO.Request;
using Mangomic.Application.DTO.Response;
using Mangomic.Application.Models;
using Mangomic.Application.Interfaces.Services;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Mangomic.Services.Identity {
    public class IdentityService : IIdentityService {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly JwtOptions _jwtOptions;

        public IdentityService(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, JwtOptions jwtOptions) {
            _signInManager = signInManager;
            _userManager = userManager;
            _jwtOptions = jwtOptions;
        }

        public async Task<UserLoginResponseDTO> LoginUser(UserLoginRequestDTO request) {
            var response = await _signInManager.PasswordSignInAsync(request.Email, request.Password, false, false);

            var user = new UserLoginResponseDTO(response.Succeeded);

            if (!response.Succeeded) {
                if (response.IsNotAllowed) user.Errors.Add("User not allowed");
            }

            return user;
        }

        public async Task<UserRegisterResponseDTO> RegisterUser(UserRegisterRequestDTO request) {
            var user = new IdentityUser {
                UserName = request.Email,
                Email = request.Email,
                EmailConfirmed = true
            };

            var response = await _userManager.CreateAsync(user, request.Password);

            var createdUser = new UserRegisterResponseDTO(response.Succeeded);

            if (response.Succeeded) await _userManager.SetLockoutEnabledAsync(user, false);
            if (!response.Succeeded && response.Errors.Count() > 0) {
                createdUser.AddError(response.Errors.Select(e => e.Description));
            }

            return createdUser;
        }

        public async Task<IList<Claim>> GetClaimsAndRoles(IdentityUser user) {
            var claims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToString()));

            foreach(var role in roles) {
                claims.Add(new Claim("role", role));
            }

            return claims;
        }

        public async Task<UserLoginResponseDTO> GetToken(string request) {
            var user = await _userManager.FindByEmailAsync(request);
            var claim = await GetClaimsAndRoles(user);

            var expirationDate = DateTime.UtcNow.AddSeconds(_jwtOptions.Expiration);

            var jwt = new JwtSecurityToken(
                    issuer: _jwtOptions.Issuer,
                    audience: _jwtOptions.Audience,
                    claims: claim,
                    signingCredentials: _jwtOptions.SigningCredentials,
                    expires: expirationDate
                );

            var token = new JwtSecurityTokenHandler().WriteToken(jwt);

            return new UserLoginResponseDTO {
                Success = true,
                Token = token,
                ExpirationDate = expirationDate
            };
        }
    }
}
