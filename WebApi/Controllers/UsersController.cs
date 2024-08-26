using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using WebApi.Entities;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public UsersController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [AllowAnonymous]
        [Produces("application/json")]
        [HttpPost("/api/AdicionarUsuario")]

        public async Task<IActionResult> AdicionarUsuario([FromBody] AddUserRequest login)
        {
            if (string.IsNullOrWhiteSpace(login.email) || string.IsNullOrWhiteSpace(login.senha))
                return Ok("Falta alguns Dados");


            var user = new ApplicationUser
            {
                UserName = login.email,
                Email = login.email,
                RG = login.rg
            };

            var resultado = await _userManager.CreateAsync(user, login.senha);

            if(resultado.Errors.Any())
            {
                return Ok(resultado.Errors);
            }

            var userId = await _userManager.GetUserIdAsync(user);
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));

            var result2 = await _userManager.ConfirmEmailAsync(user, code);

            if (result2.Succeeded)
                return Ok("Usuario adicionado com Sucesso!");
            else
                return Ok("Error ao confirmar o usuario");
        }

    }
}
