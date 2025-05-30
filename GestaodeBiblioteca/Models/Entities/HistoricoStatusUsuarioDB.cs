using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestaodeBiblioteca.Models.Entities
{
    [Table("historico_status_usuario")]
    public class HistoricoStatusUsuarioDB
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("usuario_id")]
        [Required(ErrorMessage = "O ID do usuário é obrigatório")]
        public int UsuarioId { get; set; }

        [ForeignKey("UsuarioId")]
        public UsuarioDB Usuario { get; set; }

        [Column("status")]
        [Required(ErrorMessage = "O status é obrigatório")]
        public bool Status { get; set; }

        [Column("data_alteracao")]
        [Required]
        public DateTime DataAlteracao { get; set; } = DateTime.UtcNow;

        [Column("motivo")]
        [MaxLength(500)]
        public string? Motivo { get; set; }

        [Column("observacoes")]
        [MaxLength(1000)]
        public string? Observacoes { get; set; }
    }
}