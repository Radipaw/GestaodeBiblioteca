using GestaodeBiblioteca.Models.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestaodeBiblioteca.Models.Entities
{
    public class EmprestimoDB
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Usuario")]
        public int IdUsuario { get; set; }

        public UsuarioDB Usuario { get; set; }  

        [Required]
        [ForeignKey("Livro")]
        public int IdLivro { get; set; }

        public LivroDB Livro { get; set; } 

        [Required]
        public DateTime DataEmprestimo { get; set; } = DateTime.Now;

        [Required]
        public DateTime DataDevolucaoPrevista { get; set; }

        public DateTime? DataDevolucaoEfetiva { get; set; }

        [Required]
        [EnumDataType(typeof(StatusEmprestimo))]
        public StatusEmprestimo Status { get; set; } = StatusEmprestimo.Ativo;
    }

    public enum StatusEmprestimo
    {
        Ativo,
        Finalizado,
        Atrasado
    }
}
