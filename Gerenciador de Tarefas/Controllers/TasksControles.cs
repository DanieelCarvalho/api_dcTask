using AutoMapper;
using Gerenciador_de_Tarefas.Domain.Dtos;
using Gerenciador_de_Tarefas.Domain.Models;
using Gerenciador_de_Tarefas.Infra.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Gerenciador_de_Tarefas.Controllers;
[ApiController]
[Route("dados")]
[Authorize(AuthenticationSchemes = "Bearer")]
public class TasksControles : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;
    private readonly ITaskRepository _taskRepository;
   

    public TasksControles(UserManager<User> userManager,IMapper mapper, ITaskRepository taskRepository)
    {
        _userManager = userManager;
        _mapper = mapper;
        _taskRepository = taskRepository;
    }

    [HttpGet("{userName}")]
    public async Task<IActionResult> GetDados(string userName)
    {
        return Ok("Consegui acessar os dados"); 
    }


    [HttpPost]

    [HttpPost]
    public async Task<IActionResult> CreateTask(TaskDto taskDto)
    {
     
        var currentUser = await _userManager.GetUserAsync(HttpContext.User);

        
        if (currentUser == null)
        {
            return NotFound("Usuário não encontrado.");
        }

        
        var newTask = _mapper.Map<Tasks>(taskDto);

      
        newTask.UserId = currentUser.Id;

        try
        {
            // Adicionar a nova tarefa ao repositório
            await _taskRepository.Add(newTask);

            return Ok("Tarefa criada com sucesso.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao criar tarefa: {ex.Message}");
        }
    }

}
