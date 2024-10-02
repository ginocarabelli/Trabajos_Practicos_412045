using EFWebApi.Models;
using EFWebApi.Repositories;

namespace EFWebApi.Services
{
    public class ServicioServices : IServicioServices
    {
        private IServicioRepository _repo;
        public ServicioServices(IServicioRepository repo)
        {
            _repo = repo;
        }
        public async Task<bool> Delete(int id)
        {
            return await _repo.Delete(id);
        }

        public List<TServicio> GetAll()
        {
            return _repo.GetAll();
        }

        public TServicio? GetById(int id)
        {
            return _repo.GetById(id);
        }

        public async Task<bool> Save(TServicio servicio)
        {
            return await _repo.Save(servicio);
        }

        public async Task<bool> Update(TServicio servicio, int id)
        {
            return await _repo.Update(servicio, id);
        }
    }
}
