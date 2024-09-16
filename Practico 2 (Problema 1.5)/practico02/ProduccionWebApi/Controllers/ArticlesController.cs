using Microsoft.AspNetCore.Mvc;
using ProduccionBack.Domain;
using ProduccionBack.Repositories.Contracts;
using ProduccionBack.Services;

namespace ProduccionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        private ArticleManager service;

        public ArticlesController()
        {
            service = new ArticleManager();
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(service.GetAll());
        }

        [HttpGet("article")]
        public IActionResult GetArticulo([FromQuery] int id)
        {
            try
            {
                var lst = service.GetArticleById(id);
                if (lst == null)
                    return NotFound("No se encontraron artículos con este código!");
                return Ok(lst);
            }
            catch (Exception)
            {
               return StatusCode(500, "No se pudieron consultar los artículos!");
            }
        }

        [HttpDelete("articles/{id}")]
        public IActionResult DeleteOrden(int id) 
        {
            try
            {
                if (service.DeleteArticle(id))
                {
                    return Ok($"Artículo nro: {id}, eliminado!");
                }
                else
                {
                    return NotFound("Artículo no encontrado");
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Error interno");
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] Article articulo)
        {
            try
            {
                if (articulo == null)
                {
                    return BadRequest("Se esperaba un artículo completo");
                }
                if (service.SaveArticle(articulo))
                    return Ok("Articulo registrado con éxito!");
                else
                    return StatusCode(500, "No se pudo registrar el artículo!");
            }
            catch (Exception)
            {
                return StatusCode(500, "Error interno, intente nuevamente!");
            }
        }
        [HttpPut]
        public IActionResult Put([FromBody] Article articulo)
        {
            try
            {
                if (articulo == null)
                {
                    return BadRequest("Se esperaba un artículo completo");
                }
                if (service.UpdateArticle(articulo))
                    return Ok("Articulo actualizado con éxito!");
                else
                    return StatusCode(500, "No se pudo actualizar el artículo!");
            }
            catch (Exception)
            {
                return StatusCode(500, "Error interno, intente nuevamente!");
            }
        }
    }
}
