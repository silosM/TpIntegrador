using System;
using System.Collections.Generic;
using System.Linq;
using Marketplace.Enums;

namespace Marketplace.Dominio
{
    public class Compra
    {
        public int IdCompra { get; set; }
        public Usuario Comprador { get; set; }
        public DateTime FechaCompra { get; set; }
        public EstadoCompra Estado { get; set; }
        public List<DetalleCompra> Detalles { get; set; } = new List<DetalleCompra>();
        public Envio? Envio { get; set; }
        public double ValorTotal { get; set; }

        public Compra(int idCompra, Usuario comprador)
        {
            IdCompra = idCompra;
            Comprador = comprador;
            FechaCompra = DateTime.Now;
            Estado = EstadoCompra.PENDIENTE;
        }

        public void Confirmar()
        {
            if (Estado != EstadoCompra.PENDIENTE)
                throw new InvalidOperationException("Solo se puede confirmar una compra pendiente.");

            foreach (var detalle in Detalles)
            {
                if (!detalle.Publicacion.EstaActiva())
                    throw new InvalidOperationException("La publicacion no esta activa.");

                if (Comprador.EsVendedorDe(detalle.Publicacion))
                    throw new InvalidOperationException("No podes comprar tu propia publicacion.");

                if (detalle.Cantidad <= 0)
                    throw new InvalidOperationException("Cantidad invalida.");

                if (!detalle.Publicacion.HayStock(detalle.Cantidad))
                    throw new InvalidOperationException("No hay stock suficiente.");
            }

            foreach (var detalle in Detalles)
            {
                detalle.Publicacion.DescontarStock(detalle.Cantidad);
            }

            ValorTotal = CalcularImporteTotal();
            Estado = EstadoCompra.CONFIRMADA;
        }

        public void CancelarCompra()
        {
            if (Estado == EstadoCompra.CONFIRMADA)
                throw new InvalidOperationException("No se puede cancelar una compra ya confirmada.");

            Estado = EstadoCompra.CANCELADA;
        }

        public Envio GenerarEnvio(int idEnvio)
        {
            if (Estado != EstadoCompra.CONFIRMADA)
                throw new InvalidOperationException("Solo se puede generar un envio para una compra confirmada.");

            Envio = new Envio(idEnvio, this);
            return Envio;
        }
    }
}