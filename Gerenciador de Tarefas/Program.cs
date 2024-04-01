
using Gerenciador_de_Tarefas.Domain.Context;
using Gerenciador_de_Tarefas.Infra.Repositories;
using Gerenciador_de_Tarefas.Infra.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Gerenciador_de_Tarefas
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers(); // Registra e procura controladores na aplicação.

            var defaultConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlite(defaultConnectionString);
            });
            builder.Services.AddScoped<IUserRepository ,UserRepository>();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers(); //  mapeia os controladores quando a aplicação iniciar.

            app.Run();
        }
    }
}
