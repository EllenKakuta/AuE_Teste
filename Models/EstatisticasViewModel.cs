using System.ComponentModel.DataAnnotations.Schema;

namespace AuE_Teste.Models
{
    [NotMapped]

    public class EstatisticasViewModel
    {
        public string Cidade { get; set; }
        public int TotalCadastros { get; set; }
        public IEnumerable<SexoQuantidadeViewModel> QuantidadePorSexo { get; set; }
    }

    [NotMapped]
    public class SexoQuantidadeViewModel
    {
        public string Sexo { get; set; }
        public int Quantidade { get; set; }
    }
}
