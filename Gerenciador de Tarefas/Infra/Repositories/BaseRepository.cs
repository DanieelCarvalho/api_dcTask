using Gerenciador_de_Tarefas.Domain.Context;
using Gerenciador_de_Tarefas.Domain.Models;
using Gerenciador_de_Tarefas.Infra.Repositories.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

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

        public async Task<bool> Delete(int Id)
        {
          //  var entity = await GetById(Id);
          //  _appDbContext.Set<T>().Remove(entity);
            await _appDbContext.SaveChangesAsync();
           return true;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
              return await _appDbContext.Set<T>().ToListAsync();
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Tasks>> GetByUserId(string userId)
        {
            return await _appDbContext.Tasks.Where(t => t.UserId == userId).ToListAsync();
        }

        public async Task Update( T Entity)
        {
            // _appDbContext.Set<T>().Update( Entity);
             await _appDbContext.SaveChangesAsync();
        }

       
    }
}
