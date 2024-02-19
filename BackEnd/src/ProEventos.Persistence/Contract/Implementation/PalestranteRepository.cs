using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;
using ProEventos.Persistence.Context;

namespace ProEventos.Persistence.Contract.Implementation
{
    public class PalestranteRepository : IPalestranteRepository
    {
        private readonly ProEventosContext _context;
        public PalestranteRepository(ProEventosContext context)
        {
            _context = context;
        }

        public async Task<Palestrante[]> GetAllPalestrantesAsync(bool includeEventos = false)
        {
            IQueryable<Palestrante> queryReturned = _context.Palestrantes
                                                    .Include(r => r.RedesSociais);

            if (includeEventos)
            {
                queryReturned = queryReturned.Include(p => p.PalestrantesEventos)
                                        .ThenInclude(pe => pe.Evento);
            }

            queryReturned = queryReturned.OrderBy(p => p.Id);

            return await queryReturned.AsNoTracking()
                                        .ToArrayAsync();
        }

        public async Task<Palestrante> GetAllPalestrantesByIdAsync(int palestranteId, bool includeEventos = false)
        {
            IQueryable<Palestrante> queryReturned = _context.Palestrantes
                                                    .Include(x => x.RedesSociais);

            if (includeEventos)
            {
                queryReturned = queryReturned.Include(p => p.PalestrantesEventos)
                                            .ThenInclude(pe => pe.Evento).AsNoTracking();
            }

            queryReturned = queryReturned.OrderBy(p => p.Id)
                                         .Where(p => p.Id == palestranteId);

            return await queryReturned.AsNoTracking()
                                      .FirstOrDefaultAsync();
        }

        public async Task<Palestrante[]> GetAllPalestrantesByNomeAsync(string nome, bool includeEventos = false)
        {
            IQueryable<Palestrante> queryReturned = _context.Palestrantes
                        .Include(x => x.RedesSociais);

            if (includeEventos)
            {
                queryReturned = queryReturned.Include(p => p.PalestrantesEventos)
                                              .ThenInclude(pa => pa.Evento);
            }

            queryReturned = queryReturned.OrderBy(p => p.Id)
                                         .Where(p => p.Nome.ToLower().Contains(nome.ToLower()));

            return await queryReturned.AsNoTracking()
                                .ToArrayAsync();
        }
    }
}