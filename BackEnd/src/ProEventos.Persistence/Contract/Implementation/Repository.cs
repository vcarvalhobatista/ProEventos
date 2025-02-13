using Microsoft.EntityFrameworkCore;
using ProEventos.Contract.Persistence;
using ProEventos.Persistence.Context;

namespace ProEventos.Implementation.Persistence
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ProEventosContext _context;

        public Repository(ProEventosContext context)
        {
            _context = context;

        }
        public void Add(T entity)
        {
            _context.Add(entity);
        }

        public void Delete(T entity)
        {
            _context.Entry(entity).State = EntityState.Deleted;
            _context.Remove(entity);
        }

        public void DeleteRange(T[] entities)
        {
            _context.RemoveRange(entities);
        }

        public void Update(T entity)
        {            
            _context.Update(entity);
        }
        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

    }
}