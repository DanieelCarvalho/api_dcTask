namespace Gerenciador_de_Tarefas.Infra.Repositories.Interfaces;

public interface IRepository<T>
{
    Task<IEnumerable<T>> GetAll();

    Task<T> GetById(int Id);

    Task Add(T entity);

    Task Update( T Entity);

    Task<bool> Delete(int Id);
}
