using System.Collections.Generic;
using System.Linq;
using Marketplace.Dominio;

namespace Marketplace.Repositories
{
    public class UsuarioRepository
    {
        private readonly List<Usuario> _usuarios = new List<Usuario>();

        public List<Usuario> ObtenerTodos() => _usuarios;
        public Usuario? ObtenerPorId(int id) => _usuarios.FirstOrDefault(u => u.IdUsuario == id);
        public void Agregar(Usuario usuario) => _usuarios.Add(usuario);
    }
}