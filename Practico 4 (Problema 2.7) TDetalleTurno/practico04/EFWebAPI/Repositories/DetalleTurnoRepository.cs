using EFWebAPI.Models;

namespace EFWebAPI.Repositories
{
    public class DetalleTurnoRepository : IDetalleTurnoRepository
    {
        private db_turnosContext _context;
        public DetalleTurnoRepository(db_turnosContext context)
        {
            _context = context;
        }
        public async Task<bool> Delete(int idTurno, int idServicio)
        {
            var entity = _context.TDetallesTurnos.Where(x => x.IdTurno == idTurno && x.IdServicio == idServicio).ToList().FirstOrDefault(); ;
            if (entity == null) return false;
            _context.TDetallesTurnos.Remove(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public List<TDetallesTurno> GetAll()
        {
            return _context.TDetallesTurnos.ToList();
        }

        public List<TDetallesTurno>? GetById(int idTurno)
        {
            return _context.TDetallesTurnos.Where(x => x.IdTurno == idTurno).ToList();
        }

        public async Task<bool> Save(TDetallesTurno detalle)
        {
            var entity = _context.TDetallesTurnos.Where(x => x.IdTurno == detalle.IdTurno && x.IdServicio == detalle.IdServicio).ToList().FirstOrDefault();
            if (entity == null)
            {
                if (detalle.IdServicio == null || detalle.IdServicio == 0)
                {
                    return false;
                }
                _context.TDetallesTurnos.Add(detalle);
                return await _context.SaveChangesAsync() > 0;
            }
            return false;
        }

        public async Task<bool> Update(TDetallesTurno detalle, int idTurno, int idServicio)
        {
            var entity = _context.TDetallesTurnos.Where(x => x.IdTurno == idTurno && x.IdServicio == idServicio).ToList().FirstOrDefault();
            if (entity != null)
            {
                if (detalle.IdServicio == null || detalle.IdServicio == 0)
                {
                    return false;
                }
                _context.TDetallesTurnos.Update(entity);
                return await _context.SaveChangesAsync() > 0;
            }
            return false;
        }
    }
}
