using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuE_Teste.Models
{
    [Table("Usuario")]
    public class Usuario
    {
        [Display(Name = "Código")]
        [Column("Id")]
        public int Id { get; set; }

        [Display(Name = "Nome")]
        [Column("Nome")]
        public string Nome { get; set; }

        [Display(Name = "Sexo")]
        [Column("Sexo")]
        public string Sexo { get; set; }

        [Display(Name = "Data")]
        [Column("Data")]
        [DataType(DataType.Date)]
        public DateTime Data { get; set; }

        [Display(Name = "Cidade")]
        [Column("Cidade")]
        public string Cidade { get; set; }

        public Usuario() 
        {
            Data = DateTime.Now;
        }

    }
}
