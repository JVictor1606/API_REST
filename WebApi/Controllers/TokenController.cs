using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApi.Entities;
using WebApi.Models;
using WebApi.Token;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public TokenController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [AllowAnonymous]
        [HttpPost("/api/CreateToken")]
        [Produces("application/json")]
        public async Task<IActionResult> CreateToken([FromBody] InputLoginRequest input)
        {
            if(string.IsNullOrWhiteSpace(input.Email) || string.IsNullOrWhiteSpace(input.Password))
                return Unauthorized();

            var result = await _signInManager.PasswordSignInAsync(input.Email, input.Password, false, lockoutOnFailure: false);

            if (result.Succeeded) 
            {
                var token = new TokenJWTBuilder()
                    .AddSecuriryKey(JwtSecurityKey.Create("43443FDFDF34DF34343fdf344SDFSDFSDFSDFSDF4545354345SDFGDFGDFGDFGdffgfdGDFGDGR%"))
                    .AddSubject("João Dev .Net")
                    .AddIssuer("JoaoDev.Security.Bearer")
                    .AddAudience("JoaoDev.Security.Bearer")
                    .AddExpiry(5)
                    .Builder();

                return Ok(token.value);
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}
