//using Mangomic.Context;
//using Mangomic.Model;
//using Microsoft.AspNetCore.Mvc;

//namespace Mangomic.WebAPI.Controllers {
//    [ApiController]
//    [Route("v1")]
//    public class TokenController : Controller {
//        private readonly MangomicDataContext _context;
//        private readonly UnitOfWork unitOfWork;

//        public TokenController(MangomicDataContext context) {
//            _context = context;
//            unitOfWork = new UnitOfWork(_context);
//        }

//        [HttpPost]
//        [Route("/Token/Store")]
//        public async Task<IActionResult> StoreAsync([FromServices] ITokenService tokenService, [FromBody] UserTokenModel model) {
//            var user = unitOfWork.Users.Find(u => u.Email == model.Email).FirstOrDefault();
//            if (user == null) return NotFound();

//            var isValid = PasswordHash.VerifyPassword(model.Password, user.PasswordHash);
//            if (!isValid) return BadRequest();

//            var token = tokenService.GenerateToken(user);
//            return Ok(token.ToString());
//        }
//    }
//}
