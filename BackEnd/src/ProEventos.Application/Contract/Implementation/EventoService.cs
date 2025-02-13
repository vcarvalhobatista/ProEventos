using System.Reflection.Metadata.Ecma335;

using ProEventos.Contract.Persistence;
using ProEventos.Domain;
using ProEventos.Persistence.Contract;

namespace ProEventos.Application.Contract.Implementation
{
    public class EventoService : IEventoService
    {
        private readonly IRepository<Evento> _repository;
        private readonly IEventoRepository _eventoRepository;
        public EventoService(IRepository<Evento> repository, IEventoRepository eventoRepository)
        {
            _repository = repository;
            _eventoRepository = eventoRepository;

        }
        public async Task<Evento> AddEvento(Evento model)
        {
            try
            {
                _repository.Add(model);
                await _repository.SaveChangesAsync();

                return await _eventoRepository.GetAllEventosByIdAsync(model.EventoId, false);
            }
            catch (System.Exception ex)
            {
                throw new Exception("Houve falha ao adicionar um novo Evento.", ex.InnerException);
            }
        }

        public async Task<Evento> UpdateEvento(int eventoId, Evento model)
        {
            try
            {
                if (model.EventoId != eventoId) return null;

                var evento = await _eventoRepository.GetAllEventosByIdAsync(eventoId, false);
                if (evento == null) return null;

                model.EventoId = evento.EventoId;

                _repository.Update(model);

                if (await _repository.SaveChangesAsync())
                    return await _eventoRepository.GetAllEventosByIdAsync(eventoId);

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteEventos(int eventoId)
        {
            try
            {
                var evento = await _eventoRepository.GetAllEventosByIdAsync(eventoId, false);

                if(evento == null) throw new Exception("O evento não foi encontrado.");
                _repository.Delete(evento);

                return await _repository.SaveChangesAsync();
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Evento[]> GetAllEventosAsync(bool includePalestrantes = false)
        {
            try
            {
                return await _eventoRepository.GetAllEventosAsync(includePalestrantes);
            }
            catch (System.Exception ex)
            {

                throw new Exception("Não foi possível buscar os Eventos.");
            }
        }

        public async Task<Evento> GetAllEventosByIdAsync(int eventoId, bool includePalestrantes = false)
        {
            try
            {                
                var evento = await _eventoRepository.GetAllEventosByIdAsync(eventoId, includePalestrantes);

                if(evento == null) return null;

                return evento;
            }
            catch (System.Exception ex)
            {
                throw new Exception("Houve falha na busca do Evento.", ex.InnerException);
            }
        }

        public async Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes = false)
        {
            try
            {
                return await _eventoRepository.GetAllEventosByTemaAsync(tema, includePalestrantes) ?? throw new Exception("Eventos não encontrados.");
            }
            catch (System.Exception ex)
            {
                throw new Exception("Houve falha na busca dos Eventos.", ex.InnerException);
            }
        }
    }
}