using Gerenciador_de_Tarefas.Domain.Models;

namespace Gerenciador_de_Tarefas.Infra.Repositories.Interfaces;

public interface IRepository<T>
{
    Task<IEnumerable<T>> GetAll();

    Task<IEnumerable<Tasks>> GetByUserId(string userId);

    Task Add(T entity);

    Task Update( T Entity);

    Task<bool> Delete(int Id);
}
