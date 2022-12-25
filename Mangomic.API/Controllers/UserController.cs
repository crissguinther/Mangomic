using Mangomic.Application.DTO.Request;
using Mangomic.Application.DTO.Response;
using Mangomic.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Mangomic.API.Controllers {
    [Route(template: "/v1")]
    public class UserController : ControllerBase {
        private readonly IIdentityService _identityService;

        public UserController(IIdentityService identityService) {
            _identityService = identityService;
        }

        [HttpPost]
        [Route("/user/store")]
        public async Task<ActionResult<UserRegisterResponseDTO>> Register([FromBody] UserRegisterRequestDTO request) {
            if (!ModelState.IsValid) return BadRequest();

            var result = await _identityService.RegisterUser(request);

            if (result.Success) return Created("/user/store",result);
            if (result.Errors.Count() > 0) return BadRequest(result);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpPost]
        [Route("/user/login")]
        public async Task<ActionResult<UserLoginResponseDTO>> Login([FromBody] UserLoginRequestDTO request) {
            if (!ModelState.IsValid) return BadRequest();

            var result = await _identityService.LoginUser(request);
            if (result.Success) return Ok(result);
            if (result.Errors.Count > 0) return BadRequest(result);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
