using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.EntityFrameworkCore.Diagnostics;
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
                
                throw new Exception("Houve falha ao adicionar um novo Evento.",ex.InnerException);
            }
        }

        public async Task<Evento> UpdateEvento(int eventoId, Evento model)
        {
            try
            {                
                _repository.Update(model);
                await _repository.SaveChangesAsync();
                return await _eventoRepository.GetAllEventosByIdAsync(eventoId);
            }
            catch (System.Exception)
            {
                
                throw;
            }
        }

        public async Task<bool> DeleteEventos(int eventoId)
        {
             try
            {
                _repository.Delete(_eventoRepository.GetAllEventosByIdAsync(eventoId, false));                
                
                return await _repository.SaveChangesAsync();
            }
            catch (System.Exception)
            {
                
                throw;
            }
        }

        public async Task<Evento[]> GetAllEventosAsync(bool includePalestrantes = false)
        {
            try
            {                
                return await _eventoRepository.GetAllEventosAsync(includePalestrantes);
            }
            catch (System.Exception)
            {
                
                throw;
            }
        }

        public async Task<Evento> GetAllEventosByIdAsync(int eventoId, bool includePalestrantes = false)
        {
            try
            {                
                return await _eventoRepository.GetAllEventosByIdAsync(eventoId ,includePalestrantes);
            }
            catch (System.Exception)
            {
                
                throw;
            }
        }

        public async Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes = false)
        {
            try
            {                
                return await _eventoRepository.GetAllEventosByTemaAsync(tema ,includePalestrantes);
            }
            catch (System.Exception)
            {
                
                throw;
            }
        }        
    }
}