using System.ComponentModel.DataAnnotations;

namespace Gerenciador_de_Tarefas.Domain.Dtos;

public class LoginDto
{
    [Required]
    public string UserName { get; set; }
    [Required]
    public string Password { get; set; }
}
