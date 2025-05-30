using FluentValidation;
using GestaodeBiblioteca.Models.Entities;
using System;
using System.Linq;

namespace GestaodeBiblioteca.Services
{
    public class UsuarioValidator : AbstractValidator<UsuarioDB>
    {
        public UsuarioValidator()
        {
            RuleFor(usuario => usuario.NomeCompleto)
                .NotEmpty().WithMessage("O nome é obrigatório")
                .MaximumLength(100).WithMessage("O nome não pode exceder 100 caracteres");

            RuleFor(usuario => usuario.CPF)
                .NotEmpty().WithMessage("CPF é obrigatório")
                .Length(11).WithMessage("CPF deve ter 11 dígitos")
                .Must(ValidarCPF).WithMessage("CPF inválido");

            RuleFor(usuario => usuario.Telefone)
                .NotEmpty().WithMessage("Telefone é obrigatório")
                .Length(9, 15).WithMessage("Telefone deve ter entre 9 e 15 caracteres");

            RuleFor(usuario => usuario.Endereco)
                .NotEmpty().WithMessage("Endereço é obrigatório")
                .MaximumLength(200).WithMessage("Endereço não pode exceder 200 caracteres");

            RuleFor(usuario => usuario.Categoria)
                .NotEmpty().WithMessage("Categoria é obrigatória")
                .Must(c => new[] { "Estudante", "Professor", "Visitante" }.Contains(c))
                .WithMessage("Categoria inválida");

            RuleFor(usuario => usuario.DataNascimento)
                .NotEmpty().WithMessage("Data de nascimento é obrigatória")
                .LessThan(DateTime.Now.AddYears(-5)).WithMessage("Usuário deve ter pelo menos 5 anos");
        }

        private bool ValidarCPF(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf) || cpf.Length != 11)
                return false;


            if (cpf.Distinct().Count() == 1)
                return false;

            return true;
        }
    }
}