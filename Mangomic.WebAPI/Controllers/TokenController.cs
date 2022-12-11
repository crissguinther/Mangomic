using BC = BCrypt.Net.BCrypt;

using Microsoft.AspNetCore.Mvc;

using Mangomic.Context;
using Mangomic.Domain;
using Mangomic.Services;
using Microsoft.EntityFrameworkCore;
using Mangomic.Model;

namespace Mangomic.WebAPI.Controllers {
    [ApiController]
    [Route("v1")]
    public class TokenController : Controller {
        [HttpPost]
        [Route("/Token/Store")]
        public async Task<IActionResult> StoreAsync([FromServices] ITokenService tokenService, [FromServices] MangomicContext context,[FromBody] UserTokenModel model) {
            User user = await context.Users.FirstOrDefaultAsync<User>(u => u.Email == model.Email);
            if (user == null) return NotFound();

            var isValid = BC.Verify(model.Password, user.PasswordHash);
            if (!isValid) return BadRequest();

            var token = tokenService.GenerateToken(user);
            return Ok(token.ToString());
        }
    }
}
