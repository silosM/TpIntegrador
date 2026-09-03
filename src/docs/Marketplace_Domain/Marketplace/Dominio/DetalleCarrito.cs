namespace Marketplace.Dominio
{
    public class DetalleCarrito
    {
        public int IdDetalleCarrito { get; set; }
        public Publicacion Publicacion { get; set; }
        public int Cantidad { get; set; }

        public DetalleCarrito(int idDetalleCarrito, Publicacion publicacion, int cantidad)
        {
            IdDetalleCarrito = idDetalleCarrito;
            Publicacion = publicacion;
            Cantidad = cantidad;
        }

        public double CalcularSubtotal()
        {
            return Publicacion.Precio * Cantidad;
        }
    }
}