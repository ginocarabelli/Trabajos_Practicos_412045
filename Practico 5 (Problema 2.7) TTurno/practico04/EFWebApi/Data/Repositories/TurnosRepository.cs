using EFWebApi.Models;
using Microsoft.IdentityModel.Tokens;

namespace EFWebApi.Data.Repositories
{
    public class TurnosRepository : ITurnosRepository
    {
        private db_TurnosContext _context;
        public TurnosRepository(db_TurnosContext context)
        {
            _context = context;
        }
        public bool Delete(int id)
        {
            var entity = _context.TTurnos.Find(id);
            if(entity != null)
            {
                _context.TTurnos.Remove(entity);
                return _context.SaveChanges() > 0;
            }
            return false;
        }

        public List<TTurno> GetAll()
        {
            return _context.TTurnos
                .ToList();
        }

        public TTurno? GetById(int id)
        {
            return _context.TTurnos.Find(id);
        }

        public bool Save(TTurno turno)
        {
            if(Validate(turno))
            {
                _context.TTurnos.Add(turno);
                return _context.SaveChanges() > 0;
            }
            return false;
        }
        public bool Validate(TTurno turno)
        {
            if (turno != null)
            {
                if(_context.TTurnos.Find(turno.Id) != null)
                {
                    return false;
                }
                else if (turno.Cliente.IsNullOrEmpty())
                {
                    return false;
                }
                else if (turno.Fecha > DateTime.Today.AddDays(45))
                {
                    return false;
                }
                else if (_context.TTurnos.ToList().Where(x => x.Hora == turno.Hora).Any() && _context.TTurnos.ToList().Where(x => x.Fecha == turno.Fecha).Any())
                {
                    return false;
                }
            }
            return true;
        }
        public bool Update(TTurno turno, int id)
        {
            var entity = _context.TTurnos.Find(id);
            if (entity == null) return false;
            entity.Fecha = turno.Fecha;
            entity.Hora = turno.Hora;
            entity.Cliente = turno.Cliente;
            _context.TTurnos.Update(entity);
            return _context.SaveChanges() > 0;
        }
    }
}
