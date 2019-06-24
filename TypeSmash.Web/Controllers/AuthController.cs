using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TypeSmash.Web.DTO;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;
using TypeSmash.ApplicationCore.Interfaces;
using TypeSmash.ApplicationCore.Entities;
using TypeSmash.ApplicationCore.Services;

namespace TypeSmash.Web.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        //JWT Expires in
        //TODO - Switch to using appsettings.json instead
        private readonly double EXPIRE_TIME = 300;
        //Get JWT Secret Key
        private readonly IConfiguration configuration;
        private readonly IAuthRepository authRepo;
        private readonly string secretKey;
        private readonly string JWT_COOKIE_NAME = "jwtcookie";
        public AuthController(IConfiguration configuration, IAuthRepository authRepo)
        {
            this.configuration = configuration;
            this.authRepo = authRepo;
            this.secretKey = configuration.GetSection("JWTKey").Value;
        }
        //Returns a 200 OK response if the user contains a non-expired JWT token in it's cookie.
        //As it is an authorized route, it return 401 if JWT is expired
        [HttpGet]
        public IActionResult IsLoggedIn()
        {
            return Ok();
        }

        /*
        How logging in works->
            Request -> Verify -> Add HttpOnly Cookie that stores JWT Cookie.
            On every request, have a middleware that extracts the JWT from the Cookie and places it in the header.
            TODO - Add Refresh Tokens (To be added later on)
         */

        [AllowAnonymous]
        [HttpPost("username-login")]
        public async Task<IActionResult> UsernameLogin([FromBody]UsernameLoginDTO userIn)
        {
            UserOut result = await authRepo.UsernameLogin(userIn.username, userIn.password);
            if (!result.success)
            {
                return BadRequest(result);
            }
            string token = JWTHelper.GenerateToken(result.email, result.username, secretKey, EXPIRE_TIME);
            Response.Cookies.Append(JWT_COOKIE_NAME, token, new Microsoft.AspNetCore.Http.CookieOptions
            {
                HttpOnly = true,
                Expires = System.DateTime.Now.AddMonths(1),
            });

            return Ok(result);
        }

        [HttpPost("email-login")]
        [AllowAnonymous]
        public async Task<IActionResult> EmailLogin([FromBody]EmailLoginDTO userIn)
        {
            UserOut result = await authRepo.EmailLogin(userIn.email, userIn.password);
            if (!result.success)
            {
                return BadRequest(result);
            }
            string token = JWTHelper.GenerateToken(result.email, result.username, secretKey, EXPIRE_TIME);
            Response.Cookies.Append(JWT_COOKIE_NAME, token, new Microsoft.AspNetCore.Http.CookieOptions
            {
                HttpOnly = true
            });
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpPost("signup")]
        public async Task<IActionResult> Signup([FromBody]SignupDTO userIn)
        {
            UserOut result = await authRepo.Register(userIn.email, userIn.username, userIn.password);
            if (result.success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        //Simply expire the jwt cookie now to remove it
        [AllowAnonymous]
        [HttpGet("logout")]
        public IActionResult SignOut()
        {
            Response.Cookies.Append(JWT_COOKIE_NAME, string.Empty, new Microsoft.AspNetCore.Http.CookieOptions 
            {
                Expires = System.DateTime.Now.AddDays(-1),
            });
            return Ok();
        }
    }
}
