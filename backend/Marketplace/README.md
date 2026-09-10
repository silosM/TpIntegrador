# Marketplace - Plataforma de Compra y Venta

## Integrantes del Grupo
* Johanna Marca 
* Mischel Ibarra
* Nayeli Medrano 
* Maycol Solis
* Albert Huchani
* Cristopher Cussi  

## Estructura por Capas
* **Enums**: Definiciones enumeradas del sistema (`CategoriaProducto`, `EstadoCompra`, `EstadoEnvio`, `EstadoPublicacion`).
* **Dominio**: Clases de entidad (`Usuario`, `Producto`, `Publicacion`, `Compra`, `DetalleCompra`, `Envio`, `Campania`, `Promocion`).
* **Repositories**: Capa de persistencia temporal en memoria.
* **Services**: Lógica de negocio, validaciones de stock, compras cruzadas y aplicación de promociones no acumulables.
* **Controllers**: Capa API REST con Swagger.

## Diagrama UML
Ubicado dentro de la carpeta `docs/`.