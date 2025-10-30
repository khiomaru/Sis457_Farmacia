# SIS457_Farmacia
Proyecto de la materia sis457 aplicación de escritorio de gestión de una farmacia

<p align="center">
  <<img src="Imagenes/Logo.png" width="200" alt="Nest Logo" /></a>
</p>
  <p align="center" style="font-family: impact; font-size: 20px">FARMACIA "SALUD"</p>

## Integrantes

```bash
1. Manuel Adolfo Soto
2. Leon Mamani Roly Edson
```
## Descripción

La FARMACIA  Empezo a funcionar apartir del 2025, ubicada en la Av. Marcelo Quiroga de Santa Cruz s/n.

La farmacia cuenta en la actualidad con una basta cantidad de productos entre medicamentos de distintas clases, marcas y precios ,
asi como vitaminas, productos de higiene y diversidad de artículos de salud como ser termómetros, vendas, etc.

En cuanto a su administracion la realiza de forma manual, atraves de cuadernos de actas y muchas veces incluso dejan de realizar el inventario por falta de ayuda.
A pesar de los avances tecnológicos, mantiene un registro detallado y preciso de todas las transacciones y el inventario.

Cada vez que se realiza una venta, los empleados completan un formulario de papel que incluye detalles como el nombre del cliente, los productos comprados, el precio y la fecha de la transacción. Estos formularios se archivan cuidadosamente en carpetas por fecha y cliente para su fácil acceso en caso de necesidad.

Para el inventario, se lleva un registro manual de los productos entrantes y salientes. Los empleados anotan cada nuevo envío de mercancía y registran las ventas diarias para mantener un control preciso de las existencias. Aunque este enfoque puede ser más laborioso que el uso de sistemas informáticos automatizados.

## Entidades con campos tentativos

Empleado(id, cedulaIdentidad, nombres, primerApellido, segundoApellido, direccion ,celular, cargo)
Proveedor(id, documento, razonSocial, email, telefono)
Cliente(id, documento, nombreCompleto, email, telefono)
Usuario(id, id_Empleado, usuario, clave)
Compra(id, id_Usuario, id_Proveedor, tipoDocumento, numeroDocumento, montoTotal)
Categoria(id, descripcion)
Producto(id, id_Categoria, codigo, nombre, descripcion, stock, precioCompra, precioVenta)
DetalleCompra(id, id_Compra, id_Producto, precioCompra, precioVenta, cantidad, total)
Venta(id, id_Usuario, tipoDocumento, numeroDocumento, documentoCliente, nombreCliente, montoPago, montoCambio, montoTotal)
DetalleVenta(id, id_Venta, id_Producto, precioVenta, cantidad, subTotal)

## Entidades con campos consolidados
+++DetalleNegocio(id, nombre, direccion, nit)

Empleado(id, cedulaIdentidad, nombres, primerApellido, segundoApellido, direccion ,celular, cargo)
Proveedor(id, documento, razonSocial, email, telefono)
Cliente(id, documento, nombreCompleto, email, telefono)
Usuario(id, id_Empleado, usuario, clave)
Compra(id, id_Usuario, id_Proveedor, tipoDocumento, numeroDocumento, montoTotal)
Categoria(id, descripcion)
Producto(id, id_Categoria, codigo, nombre, descripcion, stock, precioCompra, precioVenta)
DetalleCompra(id, id_Compra, id_Producto, precioCompra, precioVenta, cantidad, total)
Venta(id, id_Usuario, tipoDocumento, numeroDocumento, documentoCliente, nombreCliente, montoPago, montoCambio, montoTotal)
DetalleVenta(id, id_Venta, id_Producto, precioVenta, cantidad, subTotal)
