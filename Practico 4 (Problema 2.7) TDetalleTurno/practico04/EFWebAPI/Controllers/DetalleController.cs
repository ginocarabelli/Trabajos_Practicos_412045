using EFWebAPI.Models;
using EFWebAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EFWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetalleController : ControllerBase
    {
        private IDetalleService _service;
        public DetalleController(IDetalleService service)
        {
            _service = service;
        }
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_service.GetAll());
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(_service.GetById(id));
        }
        [HttpPost]
        public async Task<IActionResult> Post(TDetallesTurno detalle)
        {
            try
            {
                if (await _service.Save(detalle))
                    return Ok("Creado con éxito");
                return BadRequest("No se pudo crear");
            }
            catch (Exception)
            {
                return StatusCode(500, "ERROR INTERNO");
            }
        }
        [HttpPut("{idTurno}/{idServicio}")]
        public async Task<IActionResult> Put(int idTurno, int idServicio, TDetallesTurno detalle)
        {
            try
            {
                if (await _service.Update(detalle, idTurno, idServicio))
                    return Ok("Editado con éxito");
                return BadRequest("No se pudo editar");
            }
            catch (Exception)
            {
                return StatusCode(500, "ERROR INTERNO");
            }
        }
        [HttpDelete("delete/{idTurno}/{idServicio}")]
        public async Task<IActionResult> Delete(int idTurno, int idServicio)
        {
            try
            {
                if (await _service.Delete(idTurno, idServicio))
                    return Ok("Eliminado con éxito");
                return BadRequest("No se pudo eliminar");
            }
            catch (Exception)
            {
                return StatusCode(500, "ERROR INTERNO");
            }
        }
    }
}
