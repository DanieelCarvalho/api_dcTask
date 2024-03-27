using AutoMapper;
using Gerenciador_de_Tarefas.Domain.Dtos;
using Gerenciador_de_Tarefas.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Gerenciador_de_Tarefas.Controllers;
[ApiController]
[Route("/signups")]
public class SignupController : ControllerBase

{
    private readonly IMapper _mapper;

    public SignupController(IMapper mapper)
    {
        _mapper = mapper;
    }

    [HttpPost]
    public IActionResult CreateAccount(UserRequestDto bodyData)
    {

        User newUser = _mapper.Map<User>(bodyData);
        return Ok();
    }

}
