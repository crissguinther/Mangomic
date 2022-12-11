using Mangomic.Context;
using Mangomic.DAL;
using Mangomic.Domain;
using Mangomic.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Web.Http.Results;

namespace Mangomic.WebAPI.Controllers {
    [ApiController]
    [Route("/v1")]
    public class UserController : ControllerBase {
        private readonly MangomicContext _context;
        private readonly UnitOfWork unitOfWork;

        public UserController(MangomicContext context) {
            _context = context;
            unitOfWork = new UnitOfWork(_context);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("/Users/Store")]
        public async Task<IActionResult> StoreAsync([FromServices] MangomicContext context, [FromBody] CreateUserModel model) {
            if (!ModelState.IsValid) return BadRequest();

            var user = new User {
                Username = model.Username,
                Email = model.Email,
                PasswordHash = model.PasswordHash
            };

            try {
                await unitOfWork.Users.AddUser(user);
                await unitOfWork.Complete();
                return Created($"/v1/Users/{user.Id}", user);
            } 
            catch(ArgumentException ex) {
                return BadRequest("Bad request or username already exists");
            }
            catch (Exception e) {
                return StatusCode(500);
            }
        }
    }
}
