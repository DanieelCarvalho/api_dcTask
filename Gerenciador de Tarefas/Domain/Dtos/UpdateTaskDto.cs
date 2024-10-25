using Gerenciador_de_Tarefas.Domain.Models;

namespace Gerenciador_de_Tarefas.Domain.Dtos;

public class UpdateTaskDto
{
  
    // [Required]
    public string tarefa { get; set; }
    //[Required]
    public DateTime DataInitio { get; set; }
    //  [Required]
    public DateTime DataFim { get; set; }
    //  [Required]


    public string Descricao { get; set; }


}
