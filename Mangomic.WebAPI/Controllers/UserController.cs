using Mangomic.Context;
using Mangomic.Domain;
using Mangomic.Model;

using Microsoft.AspNetCore.Mvc;
using System.Web.Http.Results;

namespace Mangomic.WebAPI.Controllers {
    [ApiController]
    [Route("/v1")]
    public class UserController : ControllerBase {

        [HttpPost]
        [Route("/Users/Store")]
        public async Task<IActionResult> StoreAsync([FromServices] MangomicContext context, [FromBody] CreateUserModel model) {
            if (!ModelState.IsValid) {
                return BadRequest();
            }

            var user = new User {
                Username = model.Username,
                Email = model.Email,
                PasswordHash = model.PasswordHash
            };

            try {
                await context.Users.AddAsync(user);
                await context.SaveChangesAsync();

                return Created($"/v1/Users/{user.Id}", user);
            } catch (Exception e) {
                return StatusCode(500);
            }
        }
    }
}
