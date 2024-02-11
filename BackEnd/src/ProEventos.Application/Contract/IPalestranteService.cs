using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProEventos.Domain;

namespace ProEventos.Application.Contract
{
    public interface IPalestranteService
    {
        Task<Palestrante> AddPalestrante(Palestrante model);
        Task<Palestrante> UpdatePalestrante(int palestranteId, Palestrante model);
        Task<bool> DeletePalestrantes(int palestranteId);
        Task<Palestrante[]> GetAllPalestrantesByNomeAsync(string nome, bool includeEventos);

        Task<Palestrante[]> GetAllPalestrantesAsync(bool includeEventos);

        Task<Palestrante> GetAllPalestrantesByIdAsync(int palestranteId, bool includeEventos);
    }
}