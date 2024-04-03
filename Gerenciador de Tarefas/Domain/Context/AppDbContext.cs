using Gerenciador_de_Tarefas.Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace Gerenciador_de_Tarefas.Domain.Context;

public class AppDbContext : IdentityDbContext<User>
{
    public DbSet<Tasks> Tasks { get; set; }
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
}
