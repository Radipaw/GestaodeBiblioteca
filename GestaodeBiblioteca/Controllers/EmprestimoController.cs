using GestaodeBiblioteca.Data;
using GestaodeBiblioteca.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestaodeBiblioteca.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmprestimoController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public EmprestimoController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        public async Task<IActionResult> RegistrarEmprestimo(int idUsuario, int idLivro)
        {
            var usuario = await _dbContext.Usuario.FirstOrDefaultAsync(u => u.Id == idUsuario);
            if (usuario == null || !usuario.Ativo)
                return BadRequest("O usuário não foi encontrado ou está inativo");
            
            var livro = await _dbContext.Livro.FirstOrDefaultAsync(l => l.Id == idLivro);
            if (livro == null)
                return NotFound("O livro não foi encontrado");

            if (!livro.Disponivel)
                return BadRequest("O livro já foi emprestado");

            var emprestimosAtivos = await _dbContext.Emprestimo
                .CountAsync(e => e.IdUsuario == idUsuario && e.Status == "Ativo");

            if (emprestimosAtivos >= 3)
                return BadRequest("Esse usuário já atingiu o limite de itens emprestados");

            int diasEmprestimo = 7;


            var dataPrevista = DateTime.Now.AddDays(diasEmprestimo);

      
            var emprestimo = new EmprestimoDB
            {
                IdUsuario = idUsuario,
                IdLivro = idLivro,
                DataEmprestimo = DateTime.Now,
                DataDevolucaoPrevista = dataPrevista,
                Status = "Ativo"
            };

            _dbContext.Emprestimo.Add(emprestimo);

            livro.Disponivel = false;
            await _dbContext.SaveChangesAsync();

            return Ok(new
            {
                Mensagem = "Empréstimo realizado com sucesso",
                Usuario = usuario.NomeCompleto,
                Livro = livro.Titulo,
                DevolucaoPrevista = dataPrevista.ToString("dd/MM/yyyy")
            });
        }
    }
}
