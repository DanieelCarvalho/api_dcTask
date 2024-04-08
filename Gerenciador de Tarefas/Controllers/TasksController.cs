using AutoMapper;
using Gerenciador_de_Tarefas.Domain.Context;
using Gerenciador_de_Tarefas.Domain.Dtos;
using Gerenciador_de_Tarefas.Domain.Models;
using Gerenciador_de_Tarefas.Infra.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Gerenciador_de_Tarefas.Controllers;
[ApiController]
[Route("/tasks")]
[Authorize(AuthenticationSchemes = "Bearer")]
public class TasksController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;
    private readonly ITaskRepository _taskRepository;
    private readonly AppDbContext _appDbContext;

    public TasksController(UserManager<User> userManager,IMapper mapper, ITaskRepository taskRepository, AppDbContext appDbContext)
    {
        _userManager = userManager;
        _mapper = mapper;
        _taskRepository = taskRepository;
        _appDbContext = appDbContext;
    }



    [HttpPost("create")]
    
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
    [HttpGet("getTask")]

    public async Task<IActionResult> GetAll()
    {
       var userID = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;


        if (userID == null)
        {
            return NotFound("Usuário não encontrado.");
        }

        var tasks = await _taskRepository.GetByUserId(userID);

        if (tasks == null) return NotFound(new
        {
            Moment = DateTime.Now,
            Message = $"Cannot find user with user= {userID}"
        });
        var teste = _mapper.Map<List<TaskDto>>(tasks);

        return Ok(teste);
    }
    [HttpDelete]

    public async Task<bool> DeleteTaskId(int id)
    {
        var task = await _appDbContext.Tasks.FindAsync(id);
        if (task == null)
        {
            return false; 
        }

        task.EstarDeletado = true;
        await _appDbContext.SaveChangesAsync();

        return true;
    }
}
