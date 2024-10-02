using EFWebApi.Models;

namespace EFWebApi.Services
{
    public interface IServicioServices
    {
        List<TServicio> GetAll();
        TServicio GetById(int id);
        Task<bool> Save(TServicio servicio);
        Task<bool> Update(TServicio servicio, int id);
        Task<bool> Delete(int id);
    }
}
