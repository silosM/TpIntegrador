namespace Marketplace.Dominio
{
    public class DetalleCompra
    {
        public int IdDetalleCompra { get; set; }
        public Publicacion Publicacion { get; set; }
        public int Cantidad { get; set; }
        public double PrecioUnitario { get; set; }
        public double DescuentoAplicado { get; set; }

        public DetalleCompra(int idDetalleCompra, Publicacion publicacion, int cantidad, double precioUnitario, double descuentoAplicado = 0)
        {
            IdDetalleCompra = idDetalleCompra;
            Publicacion = publicacion;
            Cantidad = cantidad;
            PrecioUnitario = precioUnitario;
            DescuentoAplicado = descuentoAplicado;
        }

        public double CalcularSubtotal()
        {
            double subtotalSinDescuento = PrecioUnitario * Cantidad;
            return subtotalSinDescuento - DescuentoAplicado;
        }
    }
}