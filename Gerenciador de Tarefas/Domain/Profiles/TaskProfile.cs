﻿using AutoMapper;
using Gerenciador_de_Tarefas.Domain.Dtos;
using Gerenciador_de_Tarefas.Domain.Models;

namespace Gerenciador_de_Tarefas.Domain.Profiles;

public class TaskProfile : Profile
{
    public TaskProfile()
    {
    CreateMap<TaskDto, Tasks>();
        CreateMap< Tasks, TaskDto>();
        CreateMap<Tasks, GetTasksDto>();

    }

}
