using Microsoft.AspNetCore.Mvc;
using Marketplace.Services;

namespace Marketplace.Controllers
{
    public class PublicacionCreateDto
    {
        public int IdPublicacion { get; set; }
        public int ProductoId { get; set; }
        public int VendedorId { get; set; }
        public double Precio { get; set; }
        public int Stock { get; set; }
    }

    [ApiController]
    [Route("api/[controller]")]
    public class PublicacionesController : ControllerBase
    {
        private readonly MarketplaceService _service;

        public PublicacionesController(MarketplaceService service) => _service = service;

        [HttpPost]
        public IActionResult Crear([FromBody] PublicacionCreateDto dto)
        {
            try
            {
                var publicacion = _service.CrearPublicacion(dto.IdPublicacion, dto.ProductoId, dto.VendedorId, dto.Precio, dto.Stock);
                return Ok(publicacion);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("activas")]
        public IActionResult ObtenerActivas() => Ok(_service.ObtenerPublicacionesActivas());

        [HttpGet("vendedor/{idVendedor}")]
        public IActionResult ObtenerPorVendedor(int idVendedor) => Ok(_service.ObtenerPublicacionesVendedor(idVendedor));

        [HttpPut("{id}/precio")]
        public IActionResult ModificarPrecio(int id, [FromQuery] int idVendedor, [FromBody] double nuevoPrecio)
        {
            try
            {
                _service.ModificarPublicacion(id, idVendedor, nuevoPrecio);
                return Ok();
            }
            catch (UnauthorizedAccessException ex)
            {
                return StatusCode(403, ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}/pausar")]
        public IActionResult Pausar(int id, [FromQuery] int idVendedor)
        {
            try
            {
                _service.PausarPublicacion(id, idVendedor);
                return Ok();
            }
            catch (UnauthorizedAccessException ex)
            {
                return StatusCode(403, ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}/finalizar")]
        public IActionResult Finalizar(int id, [FromQuery] int idVendedor)
        {
            try
            {
                _service.FinalizarPublicacion(id, idVendedor);
                return Ok();
            }
            catch (UnauthorizedAccessException ex)
            {
                return StatusCode(403, ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}/stock")]
        public IActionResult ObtenerStock(int id) => Ok(_service.ObtenerStockDisponible(id));
    }
}