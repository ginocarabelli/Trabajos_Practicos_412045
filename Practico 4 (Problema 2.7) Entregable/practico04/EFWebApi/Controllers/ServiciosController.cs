using EFWebApi.Models;
using EFWebApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EFWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiciosController : ControllerBase
    {
        private IServicioServices _service;
        public ServiciosController(IServicioServices service)
        {
            _service = service;
        }
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_service.GetAll());
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var entity = _service.GetById(id);
                if (entity != null)
                    return Ok(entity);
                return NotFound("No se encontró el servicio");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "ERROR INTERNO");
            }
        }
        [HttpPost]
        public async Task<IActionResult> Save([FromBody]TServicio servicio)
        {
            try
            {
                if(await _service.Save(servicio))
                    return Ok("Creado con éxito");
                return BadRequest("Error al crear");
            }
            catch(Exception ex)
            {
                return StatusCode(500, "ERROR INTERNO");
            }
        }
        [HttpDelete("servicio/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (await _service.Delete(id))
                    return Ok("Eliminado con éxito");
                return BadRequest("Error al eliminar");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "ERROR INTERNO");
            }
        }
        [HttpPut("servicio/{id}")]
        public async Task<IActionResult> Put([FromBody] TServicio servicio, int id)
        {
            try
            {
                if (await _service.Update(servicio, id))
                    return Ok("Editado con éxito");
                return BadRequest("Error al editar");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "ERROR INTERNO");
            }
        }
    }
}
