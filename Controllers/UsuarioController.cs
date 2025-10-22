using Microsoft.AspNetCore.Mvc;
using Exo.WebApi.Contexts;
using Exo.WebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace Exo.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly ExoContext _context;

        public UsuariosController(ExoContext context)
        {
            _context = context;
        }

        // GET: api/usuarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> Get()
        {
            return Ok(await _context.Usuarios.ToListAsync());
        }

        // GET: api/usuarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetById(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
                return NotFound(new { message = "Usuário não encontrado." });

            return Ok(usuario);
        }

        // POST: api/usuarios
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Usuario novoUsuario)
        {
            if (novoUsuario == null)
                return BadRequest(new { message = "Dados inválidos." });

            _context.Usuarios.Add(novoUsuario);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = novoUsuario.Id }, novoUsuario);
        }

        // PUT: api/usuarios/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] Usuario usuarioAtualizado)
        {
            if (id != usuarioAtualizado.Id)
                return BadRequest(new { message = "ID do usuário inválido." });

            var usuarioExistente = await _context.Usuarios.FindAsync(id);
            if (usuarioExistente == null)
                return NotFound(new { message = "Usuário não encontrado." });

            usuarioExistente.Email = usuarioAtualizado.Email;
            usuarioExistente.Senha = usuarioAtualizado.Senha;

            _context.Entry(usuarioExistente).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(new { message = "Usuário atualizado com sucesso!" });
        }

        // DELETE: api/usuarios/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
                return NotFound(new { message = "Usuário não encontrado." });

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Usuário deletado com sucesso!" });
        }
    }
}
