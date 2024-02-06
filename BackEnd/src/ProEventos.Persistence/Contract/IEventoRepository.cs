using ProEventos.Contract.Persistence;
using ProEventos.Domain;

namespace ProEventos.Persistence.Contract
{
    public interface IEventoRepository
    {
        
        Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes);

        Task<Evento[]> GetAllEventosAsync(bool includePalestrantes);

        Task<Evento> GetAllEventosByIdAsync(int eventoId , bool includePalestrantes);
    }
}