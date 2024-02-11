using ProEventos.Contract.Persistence;
using ProEventos.Domain;

namespace ProEventos.Persistence.Contract
{
    public interface IEventoRepository
    {

        Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes = false);

        Task<Evento[]> GetAllEventosAsync(bool includePalestrantes = false);

        Task<Evento> GetAllEventosByIdAsync(int eventoId, bool includePalestrantes = false);
    }
}