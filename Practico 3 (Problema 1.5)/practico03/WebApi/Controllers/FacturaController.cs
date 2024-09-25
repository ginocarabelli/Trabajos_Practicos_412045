using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProduccionBack.Domain;
using ProduccionBack.Repositories.Implementations;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacturaController : ControllerBase
    {
        private BillsRepository service;

        public FacturaController()
        {
            service = new BillsRepository();
        }

        [HttpGet("facturas")]
        public IActionResult Get()
        {
            return Ok(service.GetAll());
        }

        [HttpGet("factura")]
        public IActionResult Get([FromQuery]int id)
        {
            try
            {
                return Ok(service.GetInvoiceById(id));
            }
            catch (Exception)
            {
                return BadRequest("No existe esta factura");
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] Invoice invoice)
        {
            try
            {
                if (service.Save(invoice))
                {
                    return Ok($"Factura nro: {invoice.InvoiceId}, creada con éxito");
                }
                return BadRequest("No se pudo ingresar la factura");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "No se ha podido crear la factura");
            }
        }

        [HttpDelete("{nro}")]
        public IActionResult Delete(int id)
        {
            try
            {
                if (service.Delete(id))
                {
                    return Ok($"Factura nro: {id}, eliminada con éxito");
                }
                return BadRequest("No se pudo eliminar la factura");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "No se ha podido eliminar la factura");
            }
        }
    }
}
