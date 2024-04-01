using System.ComponentModel.DataAnnotations;

namespace Gerenciador_de_Tarefas.Domain.Models;

public class User : Entity
{
   

    [Required]
    public string? UserName { get; set; }

    public string? Password { get; set; }
    public string? Email { get; set; }

   // public int? EnderecoId { get; set; }
   // public virtual Endereco Endereco { get; set; }
}
