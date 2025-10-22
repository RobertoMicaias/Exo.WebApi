using Microsoft.AspNetCore.Mvc;
using Exo.WebApi.Contexts;
using Exo.WebApi.Models;

namespace Exo.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ExoContext _context;

        public LoginController(ExoContext context)
        {
            _context = context;
        }

        // POST: api/login
        [HttpPost]
        public IActionResult Login([FromBody] Usuario login)
        {
            var usuario = _context.Usuarios
                .FirstOrDefault(u => u.Email == login.Email && u.Senha == login.Senha);

            if (usuario == null)
                return Unauthorized(new { message = "E-mail ou senha inválidos." });

            return Ok(new
            {
                message = "Login efetuado com sucesso!",
                usuario.Email
            });
        }
    }
}
