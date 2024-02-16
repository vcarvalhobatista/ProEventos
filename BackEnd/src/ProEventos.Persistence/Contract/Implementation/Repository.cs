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
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Entry<T>(entity).State = EntityState.Deleted;
            _context.Remove(entity);
        }

        public void DeleteRange<T>(T[] entities) where T : class
        {
            _context.RemoveRange(entities);
        }

        public void Update<T>(T entity) where T : class
        {            
            _context.Update(entity);
        }
        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

    }
}