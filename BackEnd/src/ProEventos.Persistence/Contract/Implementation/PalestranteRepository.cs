using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;

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
                        .Include(x => x.Eventos);

            if (includeEventos)
            {
                queryReturned = queryReturned.Include(p => p.PalestrantesEventos)
                                        .ThenInclude(pa => pa.Palestrante);
            }

            queryReturned = queryReturned.OrderBy(p => p.Id);
                                
            return await queryReturned.AsNoTracking()
                                .ToArrayAsync();
        }

        public async Task<Palestrante> GetAllPalestrantesByIdAsync(int palestranteId, bool includeEventos = false)
        {
            IQueryable<Palestrante> queryReturned = _context.Palestrantes
                        .Include(x => x.Eventos);

            if (includeEventos)
            {
                queryReturned = queryReturned.Include(p => p.PalestrantesEventos)
                                            .ThenInclude(pa => pa.Palestrante).AsNoTracking();
            }

            queryReturned = queryReturned.OrderBy(p => p.Id)
                                         .Where(p => p.Id == palestranteId);
                                
            return await queryReturned.AsNoTracking()
                                    .FirstOrDefaultAsync();
        }

        public async Task<Palestrante[]> GetAllPalestrantesByNomeAsync(string nome, bool includeEventos = false)
        {
            IQueryable<Palestrante> queryReturned = _context.Palestrantes
                        .Include(x => x.Eventos);

            if (includeEventos)
            {
                queryReturned = queryReturned.Include(p => p.PalestrantesEventos)
                                        .ThenInclude(pa => pa.Palestrante);
            }

            queryReturned = queryReturned.OrderBy(p => p.Id)
                                         .Where(p => p.Nome.ToLower().Contains(nome.ToLower()));
                                
            return await queryReturned.AsNoTracking()
                                .ToArrayAsync();
        }
    }
}