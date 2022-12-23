using Mangomic.Application.DTO.Request;
using Mangomic.Application.Interfaces.Services;
using Microsoft.AspNetCore.Identity;

namespace Mangomic.Services.Identity {
    public class IdentityService : IIdentityService {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly JwtOptions _jwtOptions;

        Task<UserLoginResponseDTO> IIdentityService.LoginUser(UserLoginRequestDTO request) {
            throw new NotImplementedException();
        }

        Task<UserRegisterResponseDTO> IIdentityService.RegisterUser(UserLoginRequestDTO request) {
            throw new NotImplementedException();
        }
    }
}
