using System.ComponentModel.DataAnnotations;

namespace Gerenciador_de_Tarefas.Domain.Dtos;

public class UserRequestDto
{
    [EmailAddress]
    [Required]
    
    public string? Email { get; set; }

    [Required]
    public string? UserNAme { get; set; }

    [MinLength(8)]
    [Required]
    public string? Password {  get; set; }
}
