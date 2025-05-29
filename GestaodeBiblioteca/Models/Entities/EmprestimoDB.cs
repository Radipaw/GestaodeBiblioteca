using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestaodeBiblioteca.Models.Entities
{
    [Table("emprestimos")]
    public class EmprestimoDB
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Usuario")]
        public int IdUsuario { get; set; }

        [ForeignKey("Livro")]
        public int IdLivro { get; set; }

        public DateTime DataEmprestimo { get; set; } = DateTime.Now;

        public DateTime DataDevolucaoPrevista { get; set; }

        public DateTime? DataDevolucaoEfetiva { get; set; }

        public string Status { get; set; } = "Ativo";

        public UsuarioDB? Usuario { get; set; }
        public LivroDB? Livro { get; set; }
    }
}
