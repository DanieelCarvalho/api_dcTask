using AutoMapper;
using Gerenciador_de_Tarefas.Domain.Context;
using Gerenciador_de_Tarefas.Domain.Dtos;
using Gerenciador_de_Tarefas.Domain.Models;
using Gerenciador_de_Tarefas.Infra.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace Gerenciador_de_Tarefas.Infra.Services;

public class UserService
{
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly TokenService _tokenService;

    public UserService(IMapper mapper, AppDbContext appDbContext, IUserRepository userRepository, UserManager<User> userManager, SignInManager<User> signInManager, TokenService tokenService)
    {
        _mapper = mapper;
        _userRepository = userRepository;
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
    }

    public async Task CreateAccount(UserRequestDto bodyData)
    {
        var user = _mapper.Map<User>(bodyData);
        var result = await _userManager.CreateAsync(user, bodyData.Password);
        if (!result.Succeeded)
        {
            throw new Exception("Falha ao cadastrar usuario");
        }


    }

    public async Task<UserTokenResponseDto> Login(LoginDto loginDto)
    {
        var signInResult = await _signInManager.PasswordSignInAsync(loginDto.UserName, loginDto.Password, false, false);

        if (!signInResult.Succeeded)
        {
            throw new Exception("Falha ao logar usuário");
        }
        var user = await _userManager.Users.FirstAsync(user => user.NormalizedUserName!.Equals(loginDto.UserName.ToUpper()));
        return new UserTokenResponseDto()
        {
            Token = _tokenService.GenerateToken(user)
        };
    }
}
