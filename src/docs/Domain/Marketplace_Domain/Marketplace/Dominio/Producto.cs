using Marketplace.Enums; 
namespace Marketplace.Dominio

{
    public class Producto
    {
        public int IdProducto { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public CategoriaProducto Categoria { get; set; }

        public Producto(int idProducto, string nombre, string descripcion, CategoriaProducto categoria)
        {
            IdProducto = idProducto;
            Nombre = nombre;
            Descripcion = descripcion;
            Categoria = categoria;
        }
    }
}