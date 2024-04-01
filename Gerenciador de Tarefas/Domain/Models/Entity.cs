using System.ComponentModel.DataAnnotations;

namespace Gerenciador_de_Tarefas.Domain.Models;

public interface IEntity
{
  
    public int Id { get; set; }
}

public abstract class Entity : IEntity
{
    [Key]
    [Required]
    public int Id { get; set; }
}
