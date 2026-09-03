using System;
using Marketplace.Enums;
namespace Marketplace.Dominio
{
    public class Publicacion
    {
        public int IdPublicacion { get; set; }
        public Producto Producto { get; set; }
        public Usuario Vendedor { get; set; }
        public double Precio { get; set; }
        public int Stock { get; set; }
        public EstadoPublicacion Estado { get; set; }
        public DateTime FechaPublicacion { get; set; }

        public Publicacion(int idPublicacion, Producto producto, Usuario vendedor, double precio, int stock)
        {
            IdPublicacion = idPublicacion;
            Producto = producto;
            Vendedor = vendedor;
            Precio = precio;
            Stock = stock;
            Estado = EstadoPublicacion.ACTIVA;
            FechaPublicacion = DateTime.Now;
        }

       
          public bool EstaActiva()
        {
            return Estado == EstadoPublicacion.ACTIVA;
        }
        public bool HayStock(int cantidad)
        {
            return Stock >= cantidad;
        }

        public void DescontarStock(int cantidad)
        {
            if (!HayStock(cantidad))
                throw new InvalidOperationException("No hay stock suficiente para descontar.");

            Stock -= cantidad;

            if (Stock == 0)
                Finalizar();
        }

        public void Pausar()
        {
            if (Estado == EstadoPublicacion.ACTIVA)
                Estado = EstadoPublicacion.PAUSADA;
        }

        public void Reactivar()
        {
            if (Estado == EstadoPublicacion.PAUSADA && Stock > 0)
                Estado = EstadoPublicacion.ACTIVA;
        }

        public void Finalizar()
        {
            Estado = EstadoPublicacion.FINALIZADA;
        }

        public void ModificarPrecio(double nuevoPrecio)
        {
            if (Estado != EstadoPublicacion.ACTIVA)
                throw new InvalidOperationException("Solo se pueden modificar publicaciones activas.");

            Precio = nuevoPrecio;
        }
    }
}