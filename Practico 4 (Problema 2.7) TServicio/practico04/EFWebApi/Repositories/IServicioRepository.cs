using EFWebApi.Models;

namespace EFWebApi.Repositories
{
    public interface IServicioRepository
    {
        List<TServicio> GetAll();
        TServicio? GetById(int id);
        Task<bool> Save(TServicio servicio);
        Task<bool> Update(TServicio servicio, int id);
        Task<bool> Delete(int id);
    }
}
