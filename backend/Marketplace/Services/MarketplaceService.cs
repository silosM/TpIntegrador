using System;
using System.Collections.Generic;
using System.Linq;
using Marketplace.Dominio;
using Marketplace.Enums;
using Marketplace.Repositories;
using Marketplace.Controllers;

namespace Marketplace.Services
{
    public class MarketplaceService
    {
        private readonly UsuarioRepository _usuarioRepo;
        private readonly ProductoRepository _productoRepo;
        private readonly PublicacionRepository _publicacionRepo;
        private readonly CompraRepository _compraRepo;
        private readonly CampaniaRepository _campaniaRepo;
        private readonly EnvioRepository _envioRepo;

        public MarketplaceService(
            UsuarioRepository usuarioRepo,
            ProductoRepository productoRepo,
            PublicacionRepository publicacionRepo,
            CompraRepository compraRepo,
            CampaniaRepository campaniaRepo,
            EnvioRepository envioRepo)
        {
            _usuarioRepo = usuarioRepo;
            _productoRepo = productoRepo;
            _publicacionRepo = publicacionRepo;
            _compraRepo = compraRepo;
            _campaniaRepo = campaniaRepo;
            _envioRepo = envioRepo;
        }

        // --- USUARIOS Y PRODUCTOS ---
        public void RegistrarUsuario(Usuario u) => _usuarioRepo.Agregar(u);
        public List<Usuario> ObtenerUsuarios() => _usuarioRepo.ObtenerTodos();

        public void RegistrarProducto(Producto p) => _productoRepo.Agregar(p);
        public List<Producto> ObtenerProductosPorCategoria(CategoriaProducto cat) => _productoRepo.ObtenerPorCategoria(cat);

        // --- PUBLICACIONES ---
        public Publicacion CrearPublicacion(int idPublicacion, int productoId, int vendedorId, double precio, int stock)
        {
            var producto = _productoRepo.ObtenerPorId(productoId) ?? throw new Exception("Producto no encontrado.");
            var vendedor = _usuarioRepo.ObtenerPorId(vendedorId) ?? throw new Exception("Vendedor no encontrado.");

            var publicacion = new Publicacion(idPublicacion, producto, vendedor, precio, stock);
            _publicacionRepo.Agregar(publicacion);
            return publicacion;
        }

        public List<Publicacion> ObtenerPublicacionesActivas() => _publicacionRepo.ObtenerActivas();
        public List<Publicacion> ObtenerPublicacionesVendedor(int idVendedor) => _publicacionRepo.ObtenerPorVendedor(idVendedor);

        private Publicacion ObtenerPublicacionDeVendedor(int id, int idVendedor)
        {
            var pub = _publicacionRepo.ObtenerPorId(id) ?? throw new Exception("Publicación no encontrada.");
            if (pub.Vendedor.IdUsuario != idVendedor)
                throw new UnauthorizedAccessException("Solo el vendedor dueño de la publicación puede modificarla.");
            return pub;
        }

        public void ModificarPublicacion(int id, int idVendedor, double nuevoPrecio)
        {
            var pub = ObtenerPublicacionDeVendedor(id, idVendedor);
            pub.ModificarPrecio(nuevoPrecio);
        }

        public void PausarPublicacion(int id, int idVendedor)
        {
            var pub = ObtenerPublicacionDeVendedor(id, idVendedor);
            pub.Pausar();
        }

        public void FinalizarPublicacion(int id, int idVendedor)
        {
            var pub = ObtenerPublicacionDeVendedor(id, idVendedor);
            pub.Finalizar();
        }

        public int ObtenerStockDisponible(int idPublicacion)
        {
            var pub = _publicacionRepo.ObtenerPorId(idPublicacion) ?? throw new Exception("Publicación no encontrada.");
            return pub.Stock;
        }

        // --- COMPRAS, VALIDACIONES Y PROMOCIONES ---
        public Compra RealizarCompra(int idCompra, int idComprador, List<(int idPublicacion, int cantidad)> items)
        {
            var comprador = _usuarioRepo.ObtenerPorId(idComprador) ?? throw new Exception("Comprador no existe.");
            var compra = new Compra(idCompra, comprador);

            var campaniasActivas = _campaniaRepo.ObtenerActivas();
            var todasPromos = campaniasActivas.SelectMany(c => c.Promociones).ToList();

            int detalleIdCounter = 1;
            foreach (var item in items)
            {
                var pub = _publicacionRepo.ObtenerPorId(item.idPublicacion) ?? throw new Exception($"Publicación {item.idPublicacion} no existe.");

                if (!pub.EstaActiva()) throw new InvalidOperationException("La publicación no está activa.");
                if (comprador.EsVendedorDe(pub)) throw new InvalidOperationException("Un vendedor no puede comprar su propia publicación.");
                if (!pub.HayStock(item.cantidad)) throw new InvalidOperationException("Stock insuficiente.");

                // Selección de la mejor promoción no acumulable
                var mejorPromo = Promocion.MejorPromocionPara(pub, todasPromos);
                double descuentoUnidad = mejorPromo != null ? mejorPromo.CalcularDescuento(pub.Precio) : 0;

                var detalle = new DetalleCompra(detalleIdCounter++, pub, item.cantidad, pub.Precio, descuentoUnidad * item.cantidad);
                compra.Detalles.Add(detalle);
            }

            // Descuento de stock
            foreach (var d in compra.Detalles)
            {
                d.Publicacion.DescontarStock(d.Cantidad);
            }

            compra.Confirmar();
            _compraRepo.Agregar(compra);

            // Generación de envío
            var envio = compra.GenerarEnvio(_envioRepo.ObtenerTodos().Count + 1);
            _envioRepo.Agregar(envio);

            return compra;
        }

        public List<Compra> ObtenerCompras() => _compraRepo.ObtenerTodas();

        // --- CAMPAÑAS Y ENVÍOS ---
        public void RegistrarCampania(Campania c) => _campaniaRepo.Agregar(c);
        public List<Campania> ObtenerCampaniasActivas() => _campaniaRepo.ObtenerActivas();

        public Promocion AgregarPromocion(int idCampania, PromocionCreateDto dto)
        {
            var campania = _campaniaRepo.ObtenerPorId(idCampania) ?? throw new Exception("Campaña no encontrada.");

            var promocion = new Promocion(dto.IdPromocion, campania, dto.PorcentajeDescuento)
            {
                CategoriaAplicable = dto.CategoriaAplicable,
                PrecioMinimo = dto.PrecioMinimo,
                PrecioMaximo = dto.PrecioMaximo
            };

            if (dto.VendedorAplicableId.HasValue)
                promocion.VendedorAplicable = _usuarioRepo.ObtenerPorId(dto.VendedorAplicableId.Value)
                    ?? throw new Exception("Vendedor no encontrado.");

            if (dto.ProductoAplicableId.HasValue)
                promocion.ProductoAplicable = _productoRepo.ObtenerPorId(dto.ProductoAplicableId.Value)
                    ?? throw new Exception("Producto no encontrado.");

            campania.Promociones.Add(promocion);
            return promocion;
        }

        public EstadoEnvio ObtenerEstadoEnvio(int idEnvio)
        {
            var envio = _envioRepo.ObtenerPorId(idEnvio) ?? throw new Exception("Envío no encontrado.");
            return envio.Estado;
        }

        public void ActualizarEstadoEnvio(int idEnvio, EstadoEnvio nuevoEstado)
        {
            var envio = _envioRepo.ObtenerPorId(idEnvio) ?? throw new Exception("Envío no encontrado.");
            envio.ActualizarEstado(nuevoEstado);
        }
    }
}