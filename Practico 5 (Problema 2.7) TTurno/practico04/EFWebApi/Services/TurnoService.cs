using EFWebApi.Models;
using EFWebApi.Data.Repositories;
using EFWebApi.Services;

namespace EFWebApi.Services
{
    public class TurnoService : ITurnoService
    {
        private ITurnosRepository repo;
        public TurnoService(ITurnosRepository repository)
        {
            repo = repository;
        }
        public bool Delete(int id)
        {
            return repo.Delete(id);
        }

        public List<TTurno> GetAll()
        {
            return repo.GetAll();
        }

        public TTurno? GetById(int id)
        {
            return repo.GetById(id);
        }

        public bool Save(TTurno turno)
        {
            return repo.Save(turno);
        }

        public bool Update(TTurno turno, int id)
        {
            return repo.Update(turno, id);
        }
    }
}
