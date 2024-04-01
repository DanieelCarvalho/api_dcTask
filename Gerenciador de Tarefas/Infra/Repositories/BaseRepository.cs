using Gerenciador_de_Tarefas.Domain.Context;
using Gerenciador_de_Tarefas.Domain.Models;
using Gerenciador_de_Tarefas.Infra.Repositories.Interfaces;

namespace Gerenciador_de_Tarefas.Infra.Repositories
{
    public abstract class BaseRepository<T> : IRepository<T> where T : Entity
    {
        protected readonly AppDbContext _appDbContext;

        public BaseRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task Add(T entity)
        {
            await _appDbContext.Set<T>().AddAsync(entity);
            await _appDbContext.SaveChangesAsync();
        }

        public Task<bool> Delete(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<T> GetById(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<T> Update(int Id, T NewEntity)
        {
            throw new NotImplementedException();
        }
    }
}
