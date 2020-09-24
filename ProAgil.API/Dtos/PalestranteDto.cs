using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProAgil.API.Dtos
{
    public class PalestranteDto
    {
       public int Id { get; set; } 
       [Required(ErrorMessage="O campo {0} é Obrigatório")]
       public string Nome { get; set; }
       public string MiniCurriculo { get; set; }
       public string ImagemURL { get; set; }
       [Phone]
       public string Telefone { get; set; }
       [EmailAddress]
       public string Email { get; set; }
       public List<RedeSocialDto> RedeSociais { get; set; }
       public List<EventoDto> Eventos { get; set; }
    }
}