using System.Collections.Generic;
using System.Linq;

namespace Marketplace.Dominio
{
    public class Carrito
    {
        public int IdCarrito { get; set; }
        public Usuario Comprador { get; set; }
        public List<DetalleCarrito> Detalles { get; set; } = new List<DetalleCarrito>();

        public Carrito(int idCarrito, Usuario comprador)
        {
            IdCarrito = idCarrito;
            Comprador = comprador;
        }

        public void AgregarDetalle(DetalleCarrito detalle)
        {
            Detalles.Add(detalle);
        }

        public double CalcularTotal()
        {
            return Detalles.Sum(d => d.CalcularSubtotal());
        }
    }
}