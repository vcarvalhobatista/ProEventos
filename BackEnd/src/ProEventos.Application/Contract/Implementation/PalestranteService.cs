using ProEventos.Contract.Persistence;
using ProEventos.Domain;
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

        public async Task<Palestrante> UpdatePalestrante(int palestranteId, Palestrante model)
        {
            var palestrante = _palestranteRepository.GetAllPalestrantesByIdAsync(palestranteId , false)  ?? throw new Exception("Palestrante não encontrado para ser deletado.");
            if (palestrante == null)
                return null;

            _repository.Update(palestrante);
            
            if(await _repository.SaveChangesAsync())
                return await _palestranteRepository.GetAllPalestrantesByIdAsync(palestranteId, false);

            return null;
        }

        public async Task<bool> DeletePalestrantes(int palestranteId)
        {
            var palestrante = _palestranteRepository.GetAllPalestrantesByIdAsync(palestranteId , false)  ?? throw new Exception("Palestrante não encontrado para ser deletado.");
            if (palestrante == null)
                return false;

            _repository.Delete(palestrante);
            return await _repository.SaveChangesAsync();
        }

        public async Task<Palestrante[]> GetAllPalestrantesAsync(bool includeEventos)
        {
            return  await _palestranteRepository.GetAllPalestrantesAsync(includeEventos) 
                                ?? throw new Exception("Não há palestrantes.");            
        }

        public async Task<Palestrante> GetAllPalestrantesByIdAsync(int palestranteId, bool includeEventos)
        {            
            return await _palestranteRepository.GetAllPalestrantesByIdAsync(palestranteId, includeEventos) 
                        ?? throw new Exception("Palestrante não encontrado.");
        }

        public async Task<Palestrante[]> GetAllPalestrantesByNomeAsync(string nome, bool includeEventos)
        {
            return await _palestranteRepository.GetAllPalestrantesByNomeAsync(nome, includeEventos) 
                        ?? throw new Exception("Palestrantes não encontrados.");
        }        
    }
}