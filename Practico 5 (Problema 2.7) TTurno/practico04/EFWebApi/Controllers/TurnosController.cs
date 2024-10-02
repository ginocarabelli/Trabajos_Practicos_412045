using EFWebApi.Models;
using EFWebApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EFWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TurnosController : ControllerBase
    {
        private readonly ITurnoService _turnoService;

        public TurnosController(ITurnoService turnoService)
        {
            _turnoService = turnoService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_turnoService.GetAll());
        }
        [HttpGet("turno/{id}")]
        public IActionResult Get(int id)
        {
            var turno = _turnoService.GetById(id);
            if (turno == null)
            {
                return NotFound("No se encontró el turno");
            }
            return Ok(turno);
        }
        [HttpPost]
        public IActionResult Post([FromBody]TTurno turno)
        {
            try
            {
                if (_turnoService.Save(turno))
                {
                    return Ok("Turno creado con exito!");
                }
                else
                {
                    return BadRequest("No se pudo crear el turno");
                }
                
            }
            catch (Exception)
            {
                return StatusCode(500, "ERROR INTERNO");
            }
        }
        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                if(_turnoService.Delete(id))
                    return Ok("Eliminado con éxito");
                else
                    return BadRequest("Error de sintaxis");
            }
            catch (Exception)
            {
                return StatusCode(500, "No se pudo eliminar el turno");
            }
        }
        [HttpPut("update/{id}")]
        public IActionResult Put([FromBody]TTurno turno, int id)
        {
            try
            {
                if (_turnoService.Update(turno, id))
                    return Ok("Actualizado con éxito");
                else
                    return BadRequest("Error de sintaxis");
            }
            catch (Exception)
            {
                return StatusCode(500, "No se pudo editar el turno");
            }
        }
    }
}
