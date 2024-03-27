using AutoMapper;
using Gerenciador_de_Tarefas.Domain.Dtos;
using Gerenciador_de_Tarefas.Domain.Models;

namespace Gerenciador_de_Tarefas.Domain.Profiles;

public class UserProfile :Profile
{
    public UserProfile()
    {
        CreateMap<UserRequestDto, User>();
    }

}
