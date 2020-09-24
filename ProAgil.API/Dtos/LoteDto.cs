using System.ComponentModel.DataAnnotations;

namespace ProAgil.API.Dtos
{
    public class LoteDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage="O campo {0} é Obrigatório")]
        public string Nome { get; set; }
        [Required(ErrorMessage="O campo {0} é Obrigatório")]
        public decimal Preco { get; set; }
        [Required(ErrorMessage="O campo {0} é Obrigatório")]
        public string DataInicio { get; set; }
        public string DataFim { get; set; }
        [Required(ErrorMessage="O campo {0} é Obrigatório")]
        [Range(2,120000)]
        public int Quantidade { get; set; }
    }
}