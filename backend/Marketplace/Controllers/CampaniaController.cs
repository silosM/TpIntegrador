using Microsoft.AspNetCore.Mvc;
using Marketplace.Dominio;
using Marketplace.Enums;
using Marketplace.Services;

namespace Marketplace.Controllers
{
    public class PromocionCreateDto
    {
        public int IdPromocion { get; set; }
        public double PorcentajeDescuento { get; set; }
        public CategoriaProducto? CategoriaAplicable { get; set; }
        public int? VendedorAplicableId { get; set; }
        public int? ProductoAplicableId { get; set; }
        public double? PrecioMinimo { get; set; }
        public double? PrecioMaximo { get; set; }
    }

    [ApiController]
    [Route("api/[controller]")]
    public class CampaniasController : ControllerBase
    {
        private readonly MarketplaceService _service;

        public CampaniasController(MarketplaceService service) => _service = service;

        [HttpPost]
        public IActionResult Registrar([FromBody] Campania campania)
        {
            _service.RegistrarCampania(campania);
            return Ok(campania);
        }

        [HttpGet("activas")]
        public IActionResult ObtenerActivas() => Ok(_service.ObtenerCampaniasActivas());

        [HttpPost("{idCampania}/promociones")]
        public IActionResult AgregarPromocion(int idCampania, [FromBody] PromocionCreateDto dto)
        {
            try
            {
                var promocion = _service.AgregarPromocion(idCampania, dto);
                return Ok(promocion);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}