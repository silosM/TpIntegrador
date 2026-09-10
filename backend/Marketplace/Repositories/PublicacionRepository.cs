using System.Collections.Generic;
using System.Linq;
using Marketplace.Dominio;
using Marketplace.Enums;

namespace Marketplace.Repositories
{
    public class PublicacionRepository
    {
        private readonly List<Publicacion> _publicaciones = new List<Publicacion>();

        public List<Publicacion> ObtenerTodas() => _publicaciones;
        public List<Publicacion> ObtenerActivas() =>
            _publicaciones.Where(p => p.Estado == EstadoPublicacion.ACTIVA).ToList();
        public List<Publicacion> ObtenerPorVendedor(int idVendedor) =>
            _publicaciones.Where(p => p.Vendedor.IdUsuario == idVendedor).ToList();
        public Publicacion? ObtenerPorId(int id) => _publicaciones.FirstOrDefault(p => p.IdPublicacion == id);
        public void Agregar(Publicacion publicacion) => _publicaciones.Add(publicacion);
    }
}