using Gerenciador_de_Tarefas.Domain.Dtos;
using Gerenciador_de_Tarefas.Infra.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Gerenciador_de_Tarefas.Controllers
{
    /// <summary>
    /// Controller responsável por lidar com operações relacionadas a usuários.
    /// </summary>
    [ApiController]
    [Route("/user")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Cria uma nova conta de usuário.
        /// </summary>
        /// <remarks>Cadastra usuário na base de dados</remarks>
        /// <param name="bodyData">Dados para a nova conta de usuário.</param>
        /// <returns></returns>
        /// <response code="200">Usuário criado com sucesso</response>
        /// <response code="400">Retorna erros de validação</response>
        /// <response code="500">Retorna erros de caso que ocorreram</response>
        [ProducesResponseType(typeof(successCreatDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpPost("register")]
        public async Task<IActionResult> CreateAccount(UserRequestDto bodyData)
        {
            try
            {
                if (bodyData == null)
                {
                    return BadRequest("Dados inválidos para criar uma conta de usuário");
                }


                await _userService.CreateAccount(bodyData);



                return Ok();
            }
            catch (Exception ex)
            {
                //return StatusCode(500, $"Erro interno dos servidor: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        /// <summary>
        /// Realiza o login de um usuário.
        /// </summary>
        /// <remarks>Faz login na conta com base nos dados.</remarks>
        /// <param name="loginDto">Dados de login do usuário.</param>
        /// <returns>Resposta HTTP contendo o token de autenticação do usuário.</returns>
        /// <response code="200">Retorna o token de autenticação do usuário.</response>
        /// <response code="400">Retorna um erro se os dados de login forem inválidos.</response>
        /// /// <response code="500">Retorna erros de caso que ocorreram</response>
        [HttpPost("login")]
        [ProducesResponseType(typeof(UserTokenResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            try
            {
                if (loginDto == null)
                {
                    return BadRequest();
                }
                var tokenResponse = await _userService.Login(loginDto);
                return Ok(tokenResponse);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
           
        }

    }
}
