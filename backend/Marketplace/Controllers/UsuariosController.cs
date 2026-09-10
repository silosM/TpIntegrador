using Microsoft.AspNetCore.Mvc;
using Marketplace.Dominio;
using Marketplace.Services;

namespace Marketplace.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly MarketplaceService _service;

        public UsuariosController(MarketplaceService service) => _service = service;

        [HttpPost]
        public IActionResult Registrar([FromBody] Usuario usuario)
        {
            _service.RegistrarUsuario(usuario);
            return Ok(usuario);
        }

        [HttpGet]
        public IActionResult Consultar() => Ok(_service.ObtenerUsuarios());
    }
}