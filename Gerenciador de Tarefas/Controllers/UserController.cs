using AutoMapper;
using Gerenciador_de_Tarefas.Domain.Dtos;
using Gerenciador_de_Tarefas.Domain.Models;
using Gerenciador_de_Tarefas.Infra.Repositories.Interfaces;
using Gerenciador_de_Tarefas.Services;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace Gerenciador_de_Tarefas.Controllers;
[ApiController]
[Route("users")]
public class UserController : ControllerBase

{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UserController(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    [HttpGet]
    public IActionResult GetHello()
    {
        return Ok("Hello World!");
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        User user = await _userRepository.GetByEmail(loginDto.Email);

        bool doesPasswordMatch = PasswordVerificationService.CheckPassword(user.Password, loginDto.Password);
        if (!doesPasswordMatch) return StatusCode(403);
        return Ok();
    }

}

