using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gerenciador_de_Tarefas.Controllers;
[ApiController]
[Route("dados")]
 [Authorize(AuthenticationSchemes = "Bearer")]
public class DadosControles : ControllerBase
{

    [HttpGet("{userName}")]
    public async Task<IActionResult> GetDados(string userName)
    {
        return Ok("Consegui acessar os dados"); 
    }
}
