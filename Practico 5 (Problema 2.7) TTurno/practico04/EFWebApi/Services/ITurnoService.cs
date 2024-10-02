using EFWebApi.Models;

namespace EFWebApi.Services
{
    public interface ITurnoService
    {
        List<TTurno> GetAll();
        TTurno? GetById(int id);
        bool Save(TTurno turno);
        bool Update(TTurno turno, int id);
        bool Delete(int id);
    }
}
