using Gerenciador_de_Tarefas.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Gerenciador_de_Tarefas.Domain.Context;

public class AppDbContext : DbContext
{
    public DbSet<User> users { get; set; }


    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
}
