using AutoMapper;
using Gerenciador_de_Tarefas.Domain.Context;
using Gerenciador_de_Tarefas.Domain.Dtos;
using Gerenciador_de_Tarefas.Domain.Models;
using Gerenciador_de_Tarefas.Infra.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Gerenciador_de_Tarefas.Controllers;
[ApiController]
[Route("/signups")]
public class SignupController : ControllerBase

{
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;

    public SignupController(IMapper mapper, AppDbContext appDbContext, IUserRepository userRepository)
    {
        _mapper = mapper;
        _userRepository = userRepository;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAccount(UserRequestDto bodyData)
    {
        var alunoParaCadastrar = _mapper.Map<User>(bodyData);
        await _userRepository.Add(alunoParaCadastrar);

       // await _appDbContext.users.AddAsync(alunoParaCadastrar);
       // await _appDbContext.SaveChangesAsync();
       //status 201 + corpo vazio + header com redirecionamento 
        return CreatedAtAction(nameof(CreateAccount), new {});
    }   

}
