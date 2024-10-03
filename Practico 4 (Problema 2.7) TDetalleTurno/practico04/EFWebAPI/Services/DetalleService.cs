using EFWebAPI.Models;
using EFWebAPI.Repositories;

namespace EFWebAPI.Services
{
    public class DetalleService : IDetalleService
    {
        IDetalleTurnoRepository _repo;
        public DetalleService(IDetalleTurnoRepository repo)
        {
            _repo = repo;
        }
        public async Task<bool> Delete(int idTurno, int idServicio)
        {
            return await _repo.Delete(idTurno, idServicio);
        }

        public List<TDetallesTurno> GetAll()
        {
            return _repo.GetAll();
        }

        public List<TDetallesTurno>? GetById(int id)
        {
            return _repo.GetById(id);
        }

        public async Task<bool> Save(TDetallesTurno detalle)
        {
            return await _repo.Save(detalle);
        }

        public async Task<bool> Update(TDetallesTurno detalle, int idTurno, int idServicio)
        {
            return await _repo.Update(detalle, idTurno, idServicio);
        }
    }
}
