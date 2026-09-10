using Microsoft.AspNetCore.Mvc;
using Marketplace.Dominio;
using Marketplace.Enums;
using Marketplace.Services;

namespace Marketplace.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductosController : ControllerBase
    {
        private readonly MarketplaceService _service;

        public ProductosController(MarketplaceService service) => _service = service;

        [HttpPost]
        public IActionResult Registrar([FromBody] Producto producto)
        {
            _service.RegistrarProducto(producto);
            return Ok(producto);
        }

        [HttpGet("categoria/{categoria}")]
        public IActionResult ConsultarPorCategoria(CategoriaProducto categoria) => 
            Ok(_service.ObtenerProductosPorCategoria(categoria));
    }
}