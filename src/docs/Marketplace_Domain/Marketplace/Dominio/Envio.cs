using System;
using Marketplace.Enums;
namespace Marketplace.Dominio
{
    public class Envio
    {
        public int IdEnvio { get; set; }
        public Compra Compra { get; set; }
        public DateTime FechaEnvio { get; set; }
        public DateTime? FechaEntrega { get; set; }
        public EstadoEnvio Estado { get; set; }

        public Envio(int idEnvio, Compra compra)
        {
            IdEnvio = idEnvio;
            Compra = compra;
            FechaEnvio = DateTime.Now;
            Estado = EstadoEnvio.PENDIENTE_PREPARACION;
        }

        public void ActualizarEstado(EstadoEnvio nuevoEstado)
        {
            Estado = nuevoEstado;

            if (nuevoEstado == EstadoEnvio.ENTREGADO)
                FechaEntrega = DateTime.Now;
        }
        public void CancelarEnvio()
        {
            if (Estado == EstadoEnvio.ENTREGADO)
            throw new InvalidOperationException("No se puede cancelar un envío ya entregado.");

            Estado = EstadoEnvio.CANCELADO;
        }
    }
}