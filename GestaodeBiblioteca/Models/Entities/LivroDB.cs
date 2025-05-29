using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestaodeBiblioteca.Models.Entities
{
    [Table("livro")]
    public class LivroDB
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("titulo")]
        [MaxLength(200)]
        public string Titulo { get; set; } = string.Empty;

        [Required]
        [Column("autor")]
        [MaxLength(100)]
        public string Autor { get; set; } = string.Empty;

        [Column("isbn")]
        [MaxLength(20)]
        public string? ISBN { get; set; }

        [Required]
        [Column("disponivel")]
        public bool Disponivel { get; set; } = true;

        [Required]
        [Column("data_cadastro")]
        public DateTime DataCadastro { get; set; } = DateTime.Now;
    }
}
