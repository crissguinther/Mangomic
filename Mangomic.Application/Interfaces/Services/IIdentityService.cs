using Mangomic.Application.DTO.Request;
using Mangomic.Application.DTO.Response;

namespace Mangomic.Application.Interfaces.Services {
    public interface IIdentityService {
        Task<UserRegisterResponseDTO> RegisterUser(UserRegisterRequestDTO request);
        Task<UserLoginResponseDTO> LoginUser(UserLoginRequestDTO request);
    }
}
