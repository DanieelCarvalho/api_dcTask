using Gerenciador_de_Tarefas.Infra.Repositories.Interfaces;
using Gerenciador_de_Tarefas.Infra.Repositories;
using System.Runtime.CompilerServices;
using Gerenciador_de_Tarefas.Infra.Services;

namespace Gerenciador_de_Tarefas.Configuration;
public static class DependecyInjectionConfig
 {

    public static IServiceCollection AddRepositoriesLayer(this IServiceCollection services)
    {


        return services;
    }
    public static IServiceCollection AddServicesLayer(this IServiceCollection services )
    {
        services.AddScoped<ITaskRepository, TaskRepository>();
        services.AddScoped<UserService>();
        services.AddScoped<TokenService>();
        return services;
    }

 }

