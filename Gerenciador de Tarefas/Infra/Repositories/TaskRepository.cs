using Gerenciador_de_Tarefas.Domain.Context;
using Gerenciador_de_Tarefas.Domain.Models;
using Gerenciador_de_Tarefas.Infra.Repositories.Interfaces;

namespace Gerenciador_de_Tarefas.Infra.Repositories;

public class TaskRepository : BaseRepository<Tasks>, ITaskRepository
{
    public TaskRepository(AppDbContext appDbContext) : base(appDbContext)
    {
    }
}
