using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Gerenciador_de_Tarefas.Domain.Models;

public class User : IdentityUser
{

    public virtual ICollection<Tasks> Tasks { get; set; }

}
