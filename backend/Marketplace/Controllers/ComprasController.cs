using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Marketplace.Services;

namespace Marketplace.Controllers
{
    public class SolicitudCompraDto
    {
        public int IdCompra { get; set; }
        public int IdComprador { get; set; }
        public List<ItemCompraDto> Items { get; set; } = new List<ItemCompraDto>();
    }

    public class ItemCompraDto
    {
        public int IdPublicacion { get; set; }
        public int Cantidad { get; set; }
    }

    [ApiController]
    [Route("api/[controller]")]
    public class ComprasController : ControllerBase
    {
        private readonly MarketplaceService _service;

        public ComprasController(MarketplaceService service) => _service = service;

        [HttpPost]
        public IActionResult RealizarCompra([FromBody] SolicitudCompraDto dto)
        {
            try
            {
                var items = dto.Items.ConvertAll(i => (i.IdPublicacion, i.Cantidad));
                var compra = _service.RealizarCompra(dto.IdCompra, dto.IdComprador, items);
                return Ok(compra);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult Consultar() => Ok(_service.ObtenerCompras());
    }
}