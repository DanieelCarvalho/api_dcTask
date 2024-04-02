using AutoMapper;
using Gerenciador_de_Tarefas.Domain.Context;
using Gerenciador_de_Tarefas.Domain.Dtos;
using Gerenciador_de_Tarefas.Domain.Models;
using Gerenciador_de_Tarefas.Infra.Repositories.Interfaces;
using Gerenciador_de_Tarefas.Infra.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Gerenciador_de_Tarefas.Controllers;
[ApiController]
[Route("/user")]
public class UserController : ControllerBase

{
    private readonly UserService _userService;

    public UserController(UserService userService)
    {
        _userService = userService;
    }

    [HttpPost("resgister")]
    public async Task<IActionResult> CreateAccount(UserRequestDto bodyData)
    {
        await _userService.CreateAccount(bodyData);
        return Created();
    }


    [HttpPost("login")]
    [ProducesResponseType(type: typeof(UserTokenResponseDto), statusCode: StatusCodes.Status200OK)]
    public async Task<IActionResult> login(LoginDto loginDto)
    {
       var rokenResponse= await _userService.Login(loginDto);
        return Ok(rokenResponse);
    }
}
