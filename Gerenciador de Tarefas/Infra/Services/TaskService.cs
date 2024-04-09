using Gerenciador_de_Tarefas.Infra.Repositories.Interfaces;

namespace Gerenciador_de_Tarefas.Infra.Services;

public class TaskService
{
    private readonly ITaskRepository _taskRepository;

    public TaskService(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }




}
