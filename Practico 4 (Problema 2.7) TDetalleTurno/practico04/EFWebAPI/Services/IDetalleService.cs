using EFWebAPI.Models;

namespace EFWebAPI.Services
{
    public interface IDetalleService
    {
        List<TDetallesTurno> GetAll();
        List<TDetallesTurno>? GetById(int id);
        Task<bool> Save(TDetallesTurno detalle);
        Task<bool> Update(TDetallesTurno detalle, int idTurno, int idServicio);
        Task<bool> Delete(int idTurno, int idServicio);
    }
}
