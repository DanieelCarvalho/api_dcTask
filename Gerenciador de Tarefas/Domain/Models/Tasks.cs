using System.ComponentModel.DataAnnotations.Schema;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Gerenciador_de_Tarefas.Domain.Models;

public class Tasks : Entity
{
    public string  tarefa { get; set; }

    public DateTime DataInitio { get; set; }
    public DateTime DataFim { get; set; }

    public bool EstarDeletado { get; set; } = false;
    public string Status { get; set; }

    public string  Descricao { get; set; }

    [ForeignKey("User")]
    public string UserId { get; set; }
    public virtual User User { get; set; }


}
