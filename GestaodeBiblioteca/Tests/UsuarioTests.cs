using GestaodeBiblioteca.Data;
using GestaodeBiblioteca.Models.Entities;
using GestaodeBiblioteca.Services;
using Microsoft.EntityFrameworkCore;
using NuGet.ContentModel;
using Xunit;

namespace GestaodeBiblioteca.Tests
{
    public class UsuarioTests
    {
        [Fact]
        public void CadastrarUsuario_VerificarCPFInvalido()
        {
            
            var usuario = new UsuarioDB
            {
                NomeCompleto = "Mario Jr.",
                CPF = "123", 
                Telefone = "999999999",
                Endereco = "Rua A, 123",
                Categoria = "Estudante",
                DataNascimento = new DateTime(1990, 1, 1)
            };

            var validator = new UsuarioValidator();

            
            var result = validator.Validate(usuario);
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "CPF");
        }

        [Fact]
        public async Task CadastrarUsuario_DeveSalvarNoBanco()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
    .UseNpgsql("Server=ep-bold-silence-acbsyc3d-pooler.sa-east-1.aws.neon.tech;Port=5432;Database=neondb;User Id=neondb_owner;Password=npg_WTczpoB5fy9U;SslMode=Require;Trust Server Certificate=true;")
    .Options;


            var usuario = new UsuarioDB
            {
                NomeCompleto = "Maria Oliveira",
                CPF = "14578986254",
                Telefone = "888888888",
                Endereco = "Av. Central, 456",
                Categoria = "professor",
                DataNascimento = DateTime.SpecifyKind(new DateTime(1985, 5, 15), DateTimeKind.Utc)
            };

            using (var context = new AppDbContext(options))
            {
                context.Usuario.Add(usuario);
                await context.SaveChangesAsync();
            }

            using (var context = new AppDbContext(options))
            {
                var savedUsuario = await context.Usuario.FirstOrDefaultAsync(u => u.CPF == "14578986254");
                Assert.NotNull(savedUsuario);
                Assert.Equal("Maria Oliveira", savedUsuario.NomeCompleto);
            }
        }

        [Fact]
        public void AtualizarUsuario_VerificarNomeVazio()
        {
            var usuario = new UsuarioDB
            {
                NomeCompleto = "",
                CPF = "98765432100",
                Telefone = "777777777",
                Endereco = "Rua B, 789",
                Categoria = "Visitante",
                DataNascimento = new DateTime(1992, 3, 10)
            };

            var validator = new UsuarioValidator();


            var result = validator.Validate(usuario);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "NomeCompleto");
        }

        
    }

}
