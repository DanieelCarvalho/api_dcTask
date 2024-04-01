using Gerenciador_de_Tarefas.Domain.Context;
using Gerenciador_de_Tarefas.Domain.Models;
using Gerenciador_de_Tarefas.Infra.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Gerenciador_de_Tarefas.Infra.Repositories;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(AppDbContext appDbContext) : base(appDbContext)
    {
    }

    public async Task<User> GetByEmail(string email)
    {
        return await _appDbContext.users.FirstOrDefaultAsync(user => user.Email.Equals(email));
    }
}
