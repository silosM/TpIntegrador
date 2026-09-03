using System;
using System.Collections.Generic;

namespace Marketplace.Dominio
{
    public class Campania
    {
        public int IdCampania { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public List<Promocion> Promociones { get; set; } = new List<Promocion>();

        public Campania(int idCampania, string nombre, DateTime fechaInicio, DateTime fechaFin)
        {
            IdCampania = idCampania;
            Nombre = nombre;
            FechaInicio = fechaInicio;
            FechaFin = fechaFin;
        }

        public bool EstaVigente(DateTime fecha)
        {
            return fecha >= FechaInicio && fecha <= FechaFin;
        }
    }
}