using GestaodeBiblioteca.Data;
using GestaodeBiblioteca.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestaodeBiblioteca.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LivroController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public LivroController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LivroDB>>> GetLivros()
        {
            return await _dbContext.Livro.ToListAsync();
        }

        [HttpGet("/id/{id}")]
        public async Task<ActionResult<LivroDB>> GetLivroById(int id)
        {
            var livro = await _dbContext.Livro.FindAsync(id);

            if (livro == null)
                return NotFound();

            return livro;
        }
        [HttpGet("/titulo/{titulo}")]
        public async Task<ActionResult<LivroDB>> GetLivroByTitulo(string titulo)
        {
            var livro = await _dbContext.Livro.FindAsync(titulo);

            if (livro == null)
                return NotFound();

            return livro;
        }
        [HttpGet("/isbn/{isbn}")]
        public async Task<ActionResult<LivroDB>> GetLivroByISBN(string isbn)
        {
            var livro = await _dbContext.Livro.FindAsync(isbn);

            if (livro == null)
                return NotFound();

            return livro;
        }


        [HttpPost]
        public async Task<ActionResult<LivroDB>> CreateLivro(LivroDB livro)
        {
            _dbContext.Livro.Add(livro);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetLivroById), new { id = livro.Id }, livro);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLivro(int id, LivroDB livroAtualizado)
        {
            if (id != livroAtualizado.Id)
                return BadRequest("ID incompatível");

            var livroExistente = await _dbContext.Livro.FindAsync(id);
            if (livroExistente == null)
                return NotFound();

            _dbContext.Entry(livroExistente).CurrentValues.SetValues(livroAtualizado);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLivro(int id)
        {
            var livro = await _dbContext.Livro.FindAsync(id);
            if (livro == null)
                return NotFound();

            _dbContext.Livro.Remove(livro);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
