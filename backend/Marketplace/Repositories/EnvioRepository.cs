using System.Collections.Generic;
using System.Linq;
using Marketplace.Dominio;

namespace Marketplace.Repositories
{
    public class EnvioRepository
    {
        private readonly List<Envio> _envios = new List<Envio>();

        public List<Envio> ObtenerTodos() => _envios;
        public Envio? ObtenerPorId(int id) => _envios.FirstOrDefault(e => e.IdEnvio == id);
        public void Agregar(Envio envio) => _envios.Add(envio);
    }
}