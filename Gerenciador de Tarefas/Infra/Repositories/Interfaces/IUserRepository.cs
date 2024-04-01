using Gerenciador_de_Tarefas.Domain.Models;

namespace Gerenciador_de_Tarefas.Infra.Repositories.Interfaces;

public interface IUserRepository : IRepository<User>
{
    public Task<User> GetByEmail(string email);

}
