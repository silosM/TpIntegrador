using System;
using System.Collections.Generic;
using System.Linq;
using Marketplace.Dominio;

namespace Marketplace.Repositories
{
    public class CampaniaRepository
    {
        private readonly List<Campania> _campanias = new List<Campania>();

        public List<Campania> ObtenerTodas() => _campanias;
        public List<Campania> ObtenerActivas() =>
            _campanias.Where(c => c.EstaVigente(DateTime.Now)).ToList();
        public Campania? ObtenerPorId(int id) => _campanias.FirstOrDefault(c => c.IdCampania == id);
        public void Agregar(Campania campania) => _campanias.Add(campania);
    }
}