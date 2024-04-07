using AutoMapper;
using Gerenciador_de_Tarefas.Domain.Dtos;
using Gerenciador_de_Tarefas.Infra.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Gerenciador_de_Tarefas.Controllers;
[ApiController]
[Route("users")]
public class exemploController : ControllerBase

{
    
    private readonly IMapper _mapper;

    public exemploController( IMapper mapper)
    {
        
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
       // var users = await _userRepository.GetAll();

        return Ok();
    }
    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetAccountData(int id)
    {
      //  var user = await _userRepository.GetById(id);
        //var userProfile = _mapper.Map<UserProfileDto>(user);

        return Ok();
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
       
        return Ok();
    }

}

