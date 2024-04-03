using Microsoft.OpenApi.Models;

namespace Gerenciador_de_Tarefas.Configuration;

public static class DependencyInjectionSwaggerrCongif
{
    public static IServiceCollection AddInfrastructureSwagger(this IServiceCollection services )
    {
        services.AddSwaggerGen(c => 
        {
            c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme()
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                BearerFormat = "Jwt",
                In = ParameterLocation.Header,
                Description = "Description",
            });
            c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement()
            {
                {
                new OpenApiSecurityScheme()
                {
                    Reference = new OpenApiReference()
                    {
                         Type = ReferenceType.SecurityScheme,
                          Id = "Bearer"
                    }

                },
                new string[] {}
                }
            });

        });
        return services;
    }
}
