using GestaodeBiblioteca.Data;
using GestaodeBiblioteca.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestaodeBiblioteca.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        public UsuarioController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpPost]
        public async Task<ActionResult<UsuarioDB>> CreateUsuario([FromBody] UsuarioDB usuario)
        {
            _dbContext.Usuario.Add(usuario);
            await _dbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetUsuarioById), new { id = usuario.Id }, usuario);
        }

        [HttpGet]
        public async Task<List<UsuarioDB>> GetUsuarios()
        {
            return await _dbContext.Usuario.ToListAsync();
        }

        [HttpGet("id/{id}")]
        public async Task<UsuarioDB?> GetUsuarioById(int id)
        {
            return await _dbContext.Usuario.FirstOrDefaultAsync(x => x.Id == id);
        }

        [HttpGet("nome/{nome}")]
        public async Task<UsuarioDB?> GetUsuarioByNome(string nome)
        {
            return await _dbContext.Usuario.FirstOrDefaultAsync(x => x.NomeCompleto == nome);
        }

        [HttpGet("cpf/{cpf}")]
        public async Task<UsuarioDB?> GetUsuarioByCPF(string cpf)
        {
            return await _dbContext.Usuario.FirstOrDefaultAsync(x => x.CPF == cpf);
        }

        [HttpGet("matricula/{matricula}")]
        public async Task<UsuarioDB?> GetUsuarioByMatricula(string matricula)
        {
            return await _dbContext.Usuario.FirstOrDefaultAsync(x => x.Matricula == matricula);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUsuario(int id, [FromBody] UsuarioDB usuarioAtualizado)
        {
            if (id != usuarioAtualizado.Id)
                return BadRequest("ID missmatched");

            var usuarioExistente = await _dbContext.Usuario.FindAsync(id);
            if (usuarioExistente == null)
                return NotFound();

            
            _dbContext.Entry(usuarioExistente).CurrentValues.SetValues(usuarioAtualizado);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

    }
}
