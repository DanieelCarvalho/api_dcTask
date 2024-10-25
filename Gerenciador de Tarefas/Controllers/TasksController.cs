using AutoMapper;
using Gerenciador_de_Tarefas.Domain.Context;
using Gerenciador_de_Tarefas.Domain.Dtos;
using Gerenciador_de_Tarefas.Domain.Models;
using Gerenciador_de_Tarefas.Infra.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Drawing;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Gerenciador_de_Tarefas.Controllers
{
    /// <summary>
    /// Controller responsável por lidar com operações relacionadas a tarefas.
    /// </summary>
    [ApiController]
    [Route("/tasks")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class TasksController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly ITaskRepository _taskRepository;
        private readonly AppDbContext _appDbContext;

        public TasksController(UserManager<User> userManager, IMapper mapper, ITaskRepository taskRepository, AppDbContext appDbContext)
        {
            _userManager = userManager;
            _mapper = mapper;
            _taskRepository = taskRepository;
            _appDbContext = appDbContext;
        }

        /// <summary>
        /// Cria uma nova tarefa para o usuário logado.
        /// </summary>
        /// <param name="taskDto">Dados da tarefa a ser criada.</param>
        /// <returns>Resposta HTTP indicando o resultado da operação.</returns>
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
                GetTarefasAtraso();
                return CreatedAtAction(nameof(CreateTask), responseText);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao criar tarefa: {ex.Message}");
            }
        }

        /// <summary>
        /// Obtém todas as tarefas do usuário logado.
        /// </summary>
        /// <returns>Resposta HTTP contendo as tarefas do usuário.</returns>
        [HttpGet]
        [Route("getTask")]
        public async Task<IActionResult> GetAll([FromQuery(Name = "page")] int page = 0,
                                                [FromQuery(Name = "size")] int size = 10,
                                                [FromQuery(Name = "TaskSearch")] string taskSearch = "")
        {
            var userID = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var offset = page * size;

            if (userID == null)
            {
                return NotFound("Usuário não encontrado.");
            }

            var tasks = await _taskRepository.GetByUserId(userID);

            if (!string.IsNullOrEmpty(taskSearch))
            {
                tasks = tasks.Where(t => t.tarefa.Contains(taskSearch, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            tasks = tasks.Skip(offset).Take(size).ToList();

            if (tasks == null) return NotFound(new
            {
                Moment = DateTime.Now,
                Message = $"Cannot find user with user= {userID}"
            });

            tasks = tasks.Where(t => !t.EstarDeletado).ToList();
            var mappedTasks = _mapper.Map<List<GetTasksDto>>(tasks);
            GetTarefasAtraso();

            return Ok(mappedTasks);
        }

        /// <summary>
        /// Deleta uma tarefa com o ID especificado.
        /// </summary>
        /// <param name="id">ID da tarefa a ser deletada.</param>
        /// <returns>Booleano indicando se a tarefa foi deletada com sucesso.</returns>
        [HttpDelete]
        [Route("{id}")]
        public async Task<bool> DeleteTaskId(int id)
        {
            var taskDelete = await _taskRepository.Delete(id);
            return taskDelete;

        }

        /// <summary>
        /// Atualiza uma tarefa com o ID especificado.
        /// </summary>
        /// <param name="id">ID da tarefa a ser atualizada.</param>
        /// <param name="updatedTaskDto">Dados atualizados da tarefa.</param>
        /// <returns>Resposta HTTP indicando o resultado da operação.</returns>
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

            existingTask.tarefa = updatedTaskDto.tarefa;
            existingTask.DataInitio = updatedTaskDto.DataInitio;
            existingTask.DataFim = updatedTaskDto.DataFim;
            existingTask.Descricao = updatedTaskDto.Descricao;

            await _taskRepository.Update(existingTask);
            var responseText = new
            {
                Message = "Successful",
                CreatedAt = DateTime.UtcNow
            };
            GetTarefasAtraso();

            return CreatedAtAction(nameof(UpdateTask), responseText);
        }

        /// <summary>
        /// Obtém todas as tarefas do usuário que estão em atraso.
        /// </summary>
        /// <returns>Resposta HTTP contendo as tarefas em atraso.</returns>
        [HttpGet]
        public async Task<IActionResult> GetTarefasAtraso()
        {
            try
            {
                var tasks = await _taskRepository.GetStatus();
                DateTime dataAtual = DateTime.Now;
                int diaEmMilisegundos = 1000 * 60 * 60 * 24;

                foreach (var t in tasks)
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

                await _taskRepository.UpdateStatus(tasks);
                var tarefasAtraso = tasks.Where(t => t.Status == "Em atraso").ToList();

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
}
