using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;
using ProEventos.Persistence.Context;

namespace ProEventos.Persistence.Contract.Implementation
{
    public class EventoRepository : IEventoRepository
    {
        private readonly ProEventosContext _context;

        public EventoRepository(ProEventosContext context)
        {
            _context = context;

        }
        public async Task<Evento[]> GetAllEventosAsync(bool includePalestrantes = false)
        {
            IQueryable<Evento> queryReturned = _context.Eventos
                                                        .Include(l => l.Lotes)
                                                        .Include(r => r.RedesSociais);

            if (includePalestrantes)
            {
                queryReturned = queryReturned.Include(p => p.PalestrantesEventos)
                                        .ThenInclude(pa => pa.Palestrante);
            }

            queryReturned = queryReturned.AsNoTracking().OrderBy(x => x.EventoId);

            return await queryReturned.ToArrayAsync();
        }

        public async Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes = false)
        {
            IQueryable<Evento> queryReturned = _context.Eventos
                                    .Include(l => l.Lotes)
                                    .Include(r => r.RedesSociais);

            if (includePalestrantes)
            {
                queryReturned = queryReturned.Include(p => p.PalestrantesEventos)
                                        .ThenInclude(pa => pa.Palestrante);
            }

            queryReturned = queryReturned.AsNoTracking().OrderBy(x => x.EventoId)
                                     .Where(e => e.Tema.ToLower().Contains(tema.ToLower()));

            return await queryReturned.ToArrayAsync();
        }
        public async Task<Evento> GetAllEventosByIdAsync(int eventoId, bool includePalestrantes = false)
        {
            IQueryable<Evento> queryReturned = _context.Eventos
                                              .Include(l => l.Lotes)
                                              .Include(r => r.RedesSociais);

            if (includePalestrantes)
            {
                queryReturned = queryReturned.Include(p => p.PalestrantesEventos)
                                            .ThenInclude(pa => pa.Palestrante);
            }

            queryReturned = queryReturned.AsNoTracking().OrderBy(x => x.EventoId)
                                        .Where(e => e.EventoId == eventoId);

            return await queryReturned.FirstOrDefaultAsync();
        }
    }
}