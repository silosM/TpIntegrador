using System;
using System.Collections.Generic;
using Marketplace.Enums;

namespace Marketplace.Dominio
{
    public class Promocion
    {
        public int IdPromocion { get; set; }
        public Campania Campania { get; set; }
        public double PorcentajeDescuento { get; set; }

        public CategoriaProducto? CategoriaAplicable { get; set; }
        public Usuario VendedorAplicable { get; set; }
        public Producto ProductoAplicable { get; set; }
        public double? PrecioMinimo { get; set; }
        public double? PrecioMaximo { get; set; }

        public Promocion(int idPromocion, Campania campania, double porcentajeDescuento)
        {
            IdPromocion = idPromocion;
            Campania = campania;
            PorcentajeDescuento = porcentajeDescuento;
        }

        public bool AplicaA(Publicacion publicacion)
        {
            if (!Campania.EstaVigente(DateTime.Now))
                return false;

            if (CategoriaAplicable != null && publicacion.Producto.Categoria != CategoriaAplicable)
                return false;

            if (VendedorAplicable != null && publicacion.Vendedor.IdUsuario != VendedorAplicable.IdUsuario)
                return false;

            if (ProductoAplicable != null && publicacion.Producto.IdProducto != ProductoAplicable.IdProducto)
                return false;

            if (PrecioMinimo != null && publicacion.Precio < PrecioMinimo)
                return false;

            if (PrecioMaximo != null && publicacion.Precio > PrecioMaximo)
                return false;

            return true;
        }

        public double CalcularDescuento(double precio)
        {
            return precio * (PorcentajeDescuento / 100.0);
        }

        public static Promocion MejorPromocionPara(Publicacion publicacion, List<Promocion> promociones)
        {
            Promocion mejor = null;
            double mayorDescuento = 0;

            foreach (var promo in promociones)
            {
                if (!promo.AplicaA(publicacion))
                    continue;

                double descuento = promo.CalcularDescuento(publicacion.Precio);

                if (mejor == null || descuento > mayorDescuento)
                {
                    mejor = promo;
                    mayorDescuento = descuento;
                }
            }

            return mejor;
        }
    }
}