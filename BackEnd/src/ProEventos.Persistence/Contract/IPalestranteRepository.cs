using System.Threading.Tasks;
using ProEventos.Domain;

namespace ProEventos.Persistence.Contract
{
    public interface IPalestranteRepository
    {
        Task<Palestrante[]> GetAllPalestrantesByNomeAsync(string nome, bool includeEventos);

        Task<Palestrante[]> GetAllPalestrantesAsync(bool includeEventos);

        Task<Palestrante> GetAllPalestrantesByIdAsync(int palestranteId, bool includeEventos);
    }
}