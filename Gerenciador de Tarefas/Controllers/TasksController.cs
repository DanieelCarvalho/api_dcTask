using AutoMapper;
using Gerenciador_de_Tarefas.Domain.Context;
using Gerenciador_de_Tarefas.Domain.Dtos;
using Gerenciador_de_Tarefas.Domain.Models;
using Gerenciador_de_Tarefas.Infra.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.Json.Serialization;
using System.Text.Json;

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



    [HttpPost]
    [Route("create")]
    
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
           
            await _taskRepository.Add(newTask);
            var responseText = new
            {
                Message = "Successful",
                CreatedAt = DateTime.UtcNow
            };

            return CreatedAtAction(nameof(CreateTask), responseText);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao criar tarefa: {ex.Message}");
        }
        
    }


    [HttpGet]
    [Route("getTask")]

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
        tasks = tasks.Where(t => !t.EstarDeletado).ToList();
        var teste = _mapper.Map<List<GetTasksDto>>(tasks);

        return Ok(teste);
    }
    [HttpDelete]
    [Route("{id}")]

    public async Task<bool> DeleteTaskId(int id)
    {
       var taskDelete = await _taskRepository.Delete(id);
        return (taskDelete);
    }


    [HttpPut]
    [Route("{id}")]

    public async Task<IActionResult> UpdateTask(int id, UpdateTaskDto updatedTaskDto)
    {
        if (id <= 0)
        {
            return BadRequest("ID inválido");
        }

        var existingTask = await _taskRepository.GetById(id);
        if (existingTask == null)
        {
            return NotFound("Tarefa não encontrada");
        }
        var responseText = new
        {
            Message = "Successful",
            CreatedAt = DateTime.UtcNow
        };

        existingTask.tarefa = updatedTaskDto.tarefa;
        existingTask.DataInitio = updatedTaskDto.DataInitio;
        existingTask.DataFim = updatedTaskDto.DataFim;
        existingTask.Descricao = updatedTaskDto.Descricao;

        await _taskRepository.Update(existingTask);
        return CreatedAtAction(nameof(UpdateTask), responseText);
    } 

   

    [HttpGet]
    public async Task<IActionResult> GetTarefasAtraso()
    {
        try
        {
            var task = await _taskRepository.GetStatus();
            DateTime dataAtual = DateTime.Now;
            int diaEmMilisegundos = 1000 * 60 * 60 * 24;

            foreach (var t in task)
            {
                double dataDif = (t.DataFim - dataAtual).TotalMilliseconds / diaEmMilisegundos;
                if (t.Status == "Realizada")
                {
                    continue;
                }
                else if (t.DataFim > dataAtual && dataDif < 1)
                {
                    t.Status = "Pendente";
                }
                else if (t.DataFim > dataAtual)
                {
                    t.Status = "Em andamento";
                }
                else
                {
                    t.Status = "Em atraso";
                }
            }

            await _taskRepository.UpdateStatus(task);
            var tarefasAtraso = task.Where(t => t.Status == "Em atraso").ToList();

            
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            };

           
            var jsonResult = JsonSerializer.Serialize(tarefasAtraso, options);

            return Ok(jsonResult);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao atualizar status das tarefas: {ex.Message}");
            return StatusCode(500, "Erro interno do servidor");
        }
    }
}
