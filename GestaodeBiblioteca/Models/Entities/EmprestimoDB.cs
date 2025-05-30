using GestaodeBiblioteca.Models.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestaodeBiblioteca.Models.Entities
{
    [Table("emprestimo")]
    public class EmprestimoDB
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("id_usuario")]
        [ForeignKey("Usuario")]
        public int IdUsuario { get; set; }

        public UsuarioDB Usuario { get; set; } 

        [Required]
        [Column("id_livro")]
        [ForeignKey("Livro")]
        public int IdLivro { get; set; }

        public LivroDB Livro { get; set; } 

        [Required]
        [Column("data_emprestimo")]
        public DateTime DataEmprestimo { get; set; } = DateTime.Now;

        [Required]
        [Column("data_devolucao_prevista")]
        public DateTime DataDevolucaoPrevista { get; set; }

        [Column("data_devolucao_efetiva")]
        public DateTime? DataDevolucaoEfetiva { get; set; }

        [Required]
        [Column("status")]
        public string Status { get; set; }

    }


}
