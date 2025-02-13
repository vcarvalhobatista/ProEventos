using System.Threading.Tasks;

namespace ProEventos.Contract.Persistence
{
    public interface IRepository<T> where T : class
    {
        //Persistence
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        void DeleteRange(T[] entity);
        Task<bool> SaveChangesAsync();



    }
}