using Microsoft.AspNetCore.Mvc;
using Exo.WebApi.Contexts;
using Exo.WebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace Exo.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjetosController : ControllerBase
    {
        private readonly ExoContext _context;

        public ProjetosController(ExoContext context)
        {
            _context = context;
        }

        // 🔹 GET: api/projetos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Projeto>>> Get()
        {
            return Ok(await _context.Projetos.ToListAsync());
        }

        // 🔹 GET: api/projetos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Projeto>> GetById(int id)
        {
            var projeto = await _context.Projetos.FindAsync(id);

            if (projeto == null)
                return NotFound(new { message = "Projeto não encontrado." });

            return Ok(projeto);
        }

        // 🔹 POST: api/projetos
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Projeto novoProjeto)
        {
            if (novoProjeto == null)
                return BadRequest(new { message = "Dados inválidos." });

            _context.Projetos.Add(novoProjeto);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = novoProjeto.Id }, novoProjeto);
        }

        // 🔹 PUT: api/projetos/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] Projeto projetoAtualizado)
        {
            if (id != projetoAtualizado.Id)
                return BadRequest(new { message = "ID do projeto inválido." });

            var projetoExistente = await _context.Projetos.FindAsync(id);
            if (projetoExistente == null)
                return NotFound(new { message = "Projeto não encontrado." });

            // Atualiza os campos
            projetoExistente.NomeDoProjeto = projetoAtualizado.NomeDoProjeto;
            projetoExistente.Area = projetoAtualizado.Area;
            projetoExistente.Status = projetoAtualizado.Status;

            _context.Entry(projetoExistente).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(new { message = "Projeto atualizado com sucesso!" });
        }

        // 🔹 DELETE: api/projetos/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var projeto = await _context.Projetos.FindAsync(id);
            if (projeto == null)
                return NotFound(new { message = "Projeto não encontrado." });

            _context.Projetos.Remove(projeto);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Projeto deletado com sucesso!" });
        }
    }
}
