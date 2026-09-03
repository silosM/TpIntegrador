using System;
using System.Collections.Generic;

namespace Marketplace.Dominio
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string CorreoElectronico { get; set; }
        public string Contrasenia { get; set; }
        public DateTime FechaRegistro { get; set; }

        public List<Publicacion> Publicaciones { get; set; } = new List<Publicacion>();
        public List<Compra> Compras { get; set; } = new List<Compra>();

        public Usuario(int idUsuario, string nombre, string correoElectronico, string contrasenia)
        {
            IdUsuario = idUsuario;
            Nombre = nombre;
            CorreoElectronico = correoElectronico;
            Contrasenia = contrasenia;
            FechaRegistro = DateTime.Now;
        }

        public bool EsVendedorDe(Publicacion publicacion)
        {
            return publicacion.Vendedor.IdUsuario == this.IdUsuario;
        }
    }
}