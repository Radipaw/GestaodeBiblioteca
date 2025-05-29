using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestaodeBiblioteca.Models.Entities
{
    [Table("usuario")]
    public class UsuarioDB
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("nome_completo")]
        [MaxLength(100)]
        public string NomeCompleto { get; set; }

        [Required]
        [Column("data_nascimento")]
        public DateTime DataNascimento { get; set; }

        [Required]
        [Column("cpf")]
        [MaxLength(11)]
        public string CPF { get; set; }

        [Required]
        [Column("telefone")]
        [MaxLength(20)]
        public string Telefone { get; set; }

        [Required]
        [Column("endereco")]
        public string Endereco { get; set; }

        [Required]
        [Column("categoria")]
        [MaxLength(20)]
        public string Categoria { get; set; } 

        [Column("email")]
        [MaxLength(100)]
        public string? Email { get; set; }

        [Column("matricula")]
        [MaxLength(50)]
        public string? Matricula { get; set; }

        [Column("departamento_curso")]
        [MaxLength(100)]
        public string? DepartamentoCurso { get; set; }

        [Required]
        [Column("ativo")]
        public bool Ativo { get; set; } = true;
    }
}
