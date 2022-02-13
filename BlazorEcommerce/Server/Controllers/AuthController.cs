using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorEcommerce.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _auth;

        public AuthController(IAuthService auth)
        {
            _auth = auth;
        }

        [HttpPost("register")]
        public async Task<ActionResult<ServiceResponse<int>>> Register(UserRegister rq)
        {
            var response = await _auth.Register(new Shared.User { Email = rq.Email}, rq.Password);
            if(!response.Success)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<ActionResult<ServiceResponse<string>>> Login(UserLogin rq)
        {
            var response = await _auth.Login(rq.Email, rq.Password);
            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }

    }
}
