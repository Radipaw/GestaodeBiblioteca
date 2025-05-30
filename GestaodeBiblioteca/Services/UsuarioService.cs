using GestaodeBiblioteca.Data;
using GestaodeBiblioteca.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace GestaodeBiblioteca.Services
{
    public class UsuarioService
    {
        private readonly AppDbContext _dbContext;

        public UsuarioService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<(bool IsValid, string ErrorMessage)> ValidarCPF(string cpf)
        {
            if (await IsCPFDuplicado(cpf))
            {
                return (false, "CPF já existe");
            }

            if (cpf.Length != 11 || !Regex.IsMatch(cpf, @"^\d+$"))
            {
                return (false, "CPF inválido");
            }

            return (true, string.Empty);
        }

        private async Task<bool> IsCPFDuplicado(string cpf)
        {
            var usuario = await _dbContext.Usuario
                .FirstOrDefaultAsync(u => u.CPF != null && u.CPF == cpf);
            return usuario != null;
        }
        public async Task<bool> IsUsuarioAtivoAsync(int idUsuario)
        {
            var usuario = await _dbContext.Usuario.FindAsync(idUsuario);
            return usuario != null && usuario.Ativo;
        }

        public async Task<(bool IsValid, string ErrorMessage)> ValidarUsuario(UsuarioDB usuarioDB)
        {

            if (string.IsNullOrWhiteSpace(usuarioDB.NomeCompleto))
            {
                return (false, "Nome completo é obrigatório");
            }

            if (usuarioDB.DataNascimento == default)
            {
                return (false, "Data de nascimento é obrigatória");
            }


            if (usuarioDB.DataNascimento > DateTime.Now.AddYears(-5))
            {
                return (false, "Data de nascimento inválida (muito recente)");
            }

            if (usuarioDB.DataNascimento < DateTime.Now.AddYears(-120))
            {
                return (false, "Data de nascimento inválida (muito antiga)");
            }


            var cpfValidation = await ValidarCPF(usuarioDB.CPF);
            if (!cpfValidation.IsValid)
            {
                return (false, cpfValidation.ErrorMessage);
            }


            var telefoneValidation = ValidarTelefone(usuarioDB.Telefone);
            if (!telefoneValidation.IsValid)
            {
                return (false, telefoneValidation.ErrorMessage);
            }


            if (!string.IsNullOrWhiteSpace(usuarioDB.Email) && !IsValidEmail(usuarioDB.Email))
            {
                return (false, "E-mail inválido");
            }

            return (true, string.Empty);
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }


        public static (bool IsValid, string ErrorMessage) ValidarTelefone(string telefone)
        {
            var regex = new Regex(@"^(\(?\d{2}\)?[\s-]?\d{4,5}[\s-]?\d{4})$");

            if (!regex.IsMatch(telefone))
            {
                return (false, "Formato de telefone inválido.");
            }

            return (true, string.Empty);
        }


    }
}
