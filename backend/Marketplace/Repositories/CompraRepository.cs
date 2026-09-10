using System.Collections.Generic;
using System.Linq;
using Marketplace.Dominio;

namespace Marketplace.Repositories
{
    public class CompraRepository
    {
        private readonly List<Compra> _compras = new List<Compra>();

        public List<Compra> ObtenerTodas() => _compras;
        public Compra? ObtenerPorId(int id) => _compras.FirstOrDefault(c => c.IdCompra == id);
        public void Agregar(Compra compra) => _compras.Add(compra);
    }
}