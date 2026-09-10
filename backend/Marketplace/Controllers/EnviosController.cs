using Microsoft.AspNetCore.Mvc;
using Marketplace.Enums;
using Marketplace.Services;

namespace Marketplace.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EnviosController : ControllerBase
    {
        private readonly MarketplaceService _service;

        public EnviosController(MarketplaceService service) => _service = service;

        [HttpGet("{idEnvio}/estado")]
        public IActionResult ObtenerEstado(int idEnvio) => Ok(_service.ObtenerEstadoEnvio(idEnvio));

        [HttpPut("{idEnvio}/estado")]
        public IActionResult ActualizarEstado(int idEnvio, [FromBody] EstadoEnvio nuevoEstado)
        {
            _service.ActualizarEstadoEnvio(idEnvio, nuevoEstado);
            return Ok();
        }
    }
}