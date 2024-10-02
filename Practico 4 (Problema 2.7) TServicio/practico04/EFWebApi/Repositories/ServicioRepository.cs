using EFWebApi.Models;
using Microsoft.IdentityModel.Tokens;

namespace EFWebApi.Repositories
{
    public class ServicioRepository : IServicioRepository
    {
        private db_turnosContext _context; 
        public ServicioRepository(db_turnosContext context)
        {
            _context = context;
        }
        public async Task<bool> Delete(int id)
        {
            var entity = _context.TServicios.Find(id);
            if (entity != null)
            {
                _context.TServicios.Remove(entity);
                return await _context.SaveChangesAsync() > 0;
            }
            return false;
        }

        public List<TServicio> GetAll()
        {
            return _context.TServicios.ToList();
        }

        public TServicio? GetById(int id)
        {
            var entity = _context.TServicios.Find(id);
            if(entity != null)
                return entity;
            return null;
        }

        public async Task<bool> Save(TServicio servicio)
        {
            if (Validate(servicio))
            {
                _context.Add(servicio);
                return await _context.SaveChangesAsync() > 0;
            }
            return false;
        }

        public async Task<bool> Update(TServicio servicio, int id)
        {
            if (Validate(servicio))
            {
                _context.Update(servicio);
                return await _context.SaveChangesAsync() > 0;
            }
            return false;
        }

        public bool Validate(TServicio servicio)
        {
            if(servicio != null)
            {
                if (servicio.Nombre.IsNullOrEmpty())
                {
                    return false;
                }
                else if (servicio.EnPromocion.IsNullOrEmpty())
                {
                    return false;
                }
                else if(servicio.Costo == 0)
                {
                    return false;
                }
                return true;
            }
            return false;
        }
    }
}
