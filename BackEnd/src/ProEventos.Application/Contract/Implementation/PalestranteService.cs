using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using ProEventos.Contract.Persistence;
using ProEventos.Domain;
using ProEventos.Implementation.Persistence;
using ProEventos.Persistence.Contract;

namespace ProEventos.Application.Contract.Implementation
{
    public class PalestranteService : IPalestranteService
    {
        public IRepository<Palestrante> _repository { get; }
        public IPalestranteRepository _palestranteRepository { get; }
        public PalestranteService(IRepository<Palestrante> repository, IPalestranteRepository palestranteRepository)
        {
            _palestranteRepository = palestranteRepository;
            _repository = repository;
        }
        public async Task<Palestrante> AddPalestrante(Palestrante model)
        {
            _repository.Add(model);
            await _repository.SaveChangesAsync();

            return await _palestranteRepository.GetAllPalestrantesByIdAsync(model.Id, false);
        }

        public async Task<bool> DeletePalestrantes(int palestranteId)
        {
            var palestrante = _palestranteRepository.GetAllPalestrantesByIdAsync(palestranteId , false)  ?? throw new Exception("Palestrante não encontrado para ser deletado.");
            if (palestrante == null)
                return false;

            _repository.Delete(palestrante);
            return await _repository.SaveChangesAsync();
        }

        public Task<Palestrante[]> GetAllPalestrantesAsync(bool includeEventos)
        {
            throw new NotImplementedException();
        }

        public Task<Palestrante> GetAllPalestrantesByIdAsync(int palestranteId, bool includeEventos)
        {
            throw new NotImplementedException();
        }

        public Task<Palestrante[]> GetAllPalestrantesByNomeAsync(string nome, bool includeEventos)
        {
            throw new NotImplementedException();
        }

        public Task<Palestrante> UpdatePalestrante(int palestranteId, Palestrante model)
        {
            throw new NotImplementedException();
        }
    }
}