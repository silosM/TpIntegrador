using System.Collections.Generic;
using System.Linq;
using Marketplace.Dominio;
using Marketplace.Enums;

namespace Marketplace.Repositories
{
    public class ProductoRepository
    {
        private readonly List<Producto> _productos = new List<Producto>();

        public List<Producto> ObtenerTodos() => _productos;
        public List<Producto> ObtenerPorCategoria(CategoriaProducto categoria) =>
            _productos.Where(p => p.Categoria == categoria).ToList();
        public Producto? ObtenerPorId(int id) => _productos.FirstOrDefault(p => p.IdProducto == id);
        public void Agregar(Producto producto) => _productos.Add(producto);
    }
}