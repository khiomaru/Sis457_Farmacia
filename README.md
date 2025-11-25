# Proyecto Farmacia
Sis457_Farmacia
Aplicación de escritorio (WinForms, C#) para administrar una farmacia: login, gestión de clientes y productos (CRUD con eliminación lógica) y registro de ventas con detalle y actualización de stock.

Estructura de 3 capas:

CadFarmacia (acceso a datos, ADO.NET parametrizado)
ClnFarmacia (lógica y validaciones)
CpFarmacia (presentación WinForms)
Entidades principales (tentativas):

Cliente(Id, Nombre, CI, Telefono, Direccion, Activo)
Producto(Id, Nombre, Descripcion, Precio, Stock, Activo)
Usuario(Id, Usuario, ClaveHash, Salt, Rol, Activo)
Venta(Id, Fecha, IdCliente, Total, Activo)
VentaDetalle(Id, IdVenta, IdProducto, Cantidad, PrecioUnitario)
Cómo ejecutar (cuando esté listo):

Ejecutar SQL/DDL_Farmacia.sql en SQL Server para crear LabFarmacia (incluye datos de prueba).
Configurar la connection string en CpFarmacia/App.config.
Compilar y ejecutar Sis457_Farmacia.sln en Visual Studio (Framework 4.8).
Convenciones:

Commits frecuentes y descriptivos.
Queries parametrizadas.
Transacciones en ventas.
Validaciones en UI y capa lógica.
Commit y push
git add .gitignore README.md SQL/DDL_Farmacia.sql
git commit -m "chore(repo): nombre y propósito definidos; .gitignore completo; carpeta SQL con DDL_Farmacia.sql"
git push