using System.ComponentModel.DataAnnotations;

namespace Gerenciador_de_Tarefas.Domain.Dtos;

public class UserRequestDto
{
    [EmailAddress]
    [Required]
    
    public string? Email { get; set; }

    [Required]
    public string? UserName { get; set; }

    
    [Required]
    [DataType(DataType.Password)]
    public string? Password {  get; set; }

    [Required]
    [Compare("Password")]
    public string? PasswordConfirmation { get; set;}
}
