-- SCRIPT COMPLETO: Base de datos Farmacia (Labsis457farmacia)
-- Ejecutar en SQL Server (SSMS). Ajusta password si lo deseas.

USE master;
GO

-- Crear base de datos si no existe
IF DB_ID('Labsis457farmacia') IS NULL
BEGIN
    CREATE DATABASE Labsis457farmacia;
END
GO

-- Crear login y usuario (si no existen)
IF NOT EXISTS (SELECT 1 FROM sys.server_principals WHERE name = 'usrfarmacia')
BEGIN
    CREATE LOGIN [usrfarmacia] WITH PASSWORD = N'123456', 
        CHECK_EXPIRATION = OFF, CHECK_POLICY = ON, DEFAULT_DATABASE = Labsis457farmacia;
END
GO

USE Labsis457farmacia;
GO

IF NOT EXISTS (SELECT 1 FROM sys.database_principals WHERE name = 'usrfarmacia')
BEGIN
    CREATE USER [usrfarmacia] FOR LOGIN [usrfarmacia];
    ALTER ROLE [db_owner] ADD MEMBER [usrfarmacia];
END
GO

-- Eliminar tablas en orden seguro si existen (hijas primero)
DROP TABLE IF EXISTS DetalleVenta;
DROP TABLE IF EXISTS Venta;
DROP TABLE IF EXISTS DetalleCompra;
DROP TABLE IF EXISTS Compra;
DROP TABLE IF EXISTS Usuario;
DROP TABLE IF EXISTS Medicamento;
DROP TABLE IF EXISTS Categoria;
DROP TABLE IF EXISTS Paciente;
DROP TABLE IF EXISTS Proveedor;
DROP TABLE IF EXISTS Empleado;
DROP TABLE IF EXISTS DetalleNegocio;
GO

-- Tabla: DetalleNegocio (información del negocio)
CREATE TABLE DetalleNegocio (
    Id INT PRIMARY KEY IDENTITY(1,1),
    nombre NVARCHAR(100) NOT NULL,
    direccion NVARCHAR(200) NULL,
    nit NVARCHAR(50) NULL,
    usuarioRegistro VARCHAR(50) NOT NULL DEFAULT SUSER_NAME(),
    fechaRegistro DATETIME NOT NULL DEFAULT GETDATE(),
    estado SMALLINT NOT NULL DEFAULT 1 -- -1: Eliminado, 0: Inactivo, 1: Activo
);
GO

-- Tabla: Empleado
CREATE TABLE Empleado(
  id INT PRIMARY KEY IDENTITY(1,1),
  cedulaIdentidad VARCHAR(20) NOT NULL,
  nombres VARCHAR(50) NOT NULL,
  primerApellido VARCHAR(50) NULL,
  segundoApellido VARCHAR(50) NULL,
  direccion VARCHAR(250) NULL,
  celular VARCHAR(15) NULL,
  cargo VARCHAR(50) NULL,
  usuarioRegistro VARCHAR(50) NOT NULL DEFAULT SUSER_NAME(),
  fechaRegistro DATETIME NOT NULL DEFAULT GETDATE(),
  estado SMALLINT NOT NULL DEFAULT 1
);
GO

-- Tabla: Proveedor
CREATE TABLE Proveedor (
  id INT PRIMARY KEY IDENTITY(1,1),
  documento VARCHAR(50) NOT NULL,
  razonSocial VARCHAR(150) NOT NULL,
  email VARCHAR(100) NULL,
  telefono VARCHAR(15) NULL,
  direccion VARCHAR(250) NULL,
  usuarioRegistro VARCHAR(50) NOT NULL DEFAULT SUSER_NAME(),
  fechaRegistro DATETIME NOT NULL DEFAULT GETDATE(),
  estado SMALLINT NOT NULL DEFAULT 1
);
GO

-- Tabla: Paciente
CREATE TABLE Paciente (
  id INT PRIMARY KEY IDENTITY(1,1),
  documento VARCHAR(50) NOT NULL,
  nombreCompleto VARCHAR(150) NOT NULL,
  email VARCHAR(100) NULL,
  telefono VARCHAR(15) NULL,
  direccion VARCHAR(250) NULL,
  usuarioRegistro VARCHAR(50) NOT NULL DEFAULT SUSER_NAME(),
  fechaRegistro DATETIME NOT NULL DEFAULT GETDATE(),
  estado SMALLINT NOT NULL DEFAULT 1
);
GO

-- Tabla: Usuario (login de la app, vinculado a Empleado)
CREATE TABLE Usuario (
  id INT PRIMARY KEY IDENTITY(1,1),
  idEmpleado INT NOT NULL,
  usuario VARCHAR(50) NOT NULL,
  clave VARCHAR(250) NOT NULL,
  usuarioRegistro VARCHAR(50) NOT NULL DEFAULT SUSER_NAME(),
  fechaRegistro DATETIME NOT NULL DEFAULT GETDATE(),
  estado SMALLINT NOT NULL DEFAULT 1,
  CONSTRAINT fk_Usuario_Empleado FOREIGN KEY (idEmpleado) REFERENCES Empleado(id)
);
GO

-- Tabla: Compra (encabezado)
CREATE TABLE Compra (
  id INT PRIMARY KEY IDENTITY(1,1),
  idProveedor INT NOT NULL,
  idUsuario INT NULL,
  tipoDocumento VARCHAR(50) NULL,
  numeroDocumento VARCHAR(50) NULL,
  montoTotal DECIMAL(18,2) NOT NULL DEFAULT 0,
  usuarioRegistro VARCHAR(50) NOT NULL DEFAULT SUSER_NAME(),
  fechaRegistro DATETIME NOT NULL DEFAULT GETDATE(),
  estado SMALLINT NOT NULL DEFAULT 1,
  CONSTRAINT fk_Compra_Proveedor FOREIGN KEY (idProveedor) REFERENCES Proveedor(id),
  CONSTRAINT fk_Compra_Usuario FOREIGN KEY (idUsuario) REFERENCES Usuario(id)
);
GO

-- Tabla: Categoria
CREATE TABLE Categoria (
  id INT PRIMARY KEY IDENTITY(1,1),
  descripcion VARCHAR(150) NOT NULL,
  usuarioRegistro VARCHAR(50) NOT NULL DEFAULT SUSER_NAME(),
  fechaRegistro DATETIME NOT NULL DEFAULT GETDATE(),
  estado SMALLINT NOT NULL DEFAULT 1
);
GO

-- Tabla: Medicamento (productos)
CREATE TABLE Medicamento (
  id INT PRIMARY KEY IDENTITY(1,1),
  idCategoria INT NOT NULL,
  codigo VARCHAR(50) NOT NULL,
  nombre VARCHAR(150) NOT NULL,
  descripcion VARCHAR(250) NULL,
  tipoUnidad VARCHAR(70) NULL, -- Ej: Caja, Blister, Frasco, Unidad
  stock INT NOT NULL DEFAULT 0,
  precioCompra DECIMAL(18,2) NOT NULL DEFAULT 0,
  precioVenta DECIMAL(18,2) NOT NULL DEFAULT 0,
  requiereReceta BIT NOT NULL DEFAULT 0,
  usuarioRegistro VARCHAR(50) NOT NULL DEFAULT SUSER_NAME(),
  fechaRegistro DATETIME NOT NULL DEFAULT GETDATE(),
  estado SMALLINT NOT NULL DEFAULT 1,
  CONSTRAINT fk_Medicamento_Categoria FOREIGN KEY (idCategoria) REFERENCES Categoria(id)
);
GO

-- Tabla: DetalleCompra (lineas de compra)
CREATE TABLE DetalleCompra (
  id INT PRIMARY KEY IDENTITY(1,1),
  idCompra INT NOT NULL,
  idMedicamento INT NOT NULL,
  precioCompra DECIMAL(18,2) NOT NULL DEFAULT 0,
  precioVenta DECIMAL(18,2) NOT NULL DEFAULT 0,
  cantidad INT NOT NULL DEFAULT 0,
  total DECIMAL(18,2) NOT NULL DEFAULT 0,
  usuarioRegistro VARCHAR(50) NOT NULL DEFAULT SUSER_NAME(),
  fechaRegistro DATETIME NOT NULL DEFAULT GETDATE(),
  estado SMALLINT NOT NULL DEFAULT 1,
  CONSTRAINT fk_DetalleCompra_Compra FOREIGN KEY (idCompra) REFERENCES Compra(id),
  CONSTRAINT fk_DetalleCompra_Medicamento FOREIGN KEY (idMedicamento) REFERENCES Medicamento(id)
);
GO

-- Tabla: Venta (encabezado)
CREATE TABLE Venta (
  id INT PRIMARY KEY IDENTITY(1,1),
  tipoDocumento VARCHAR(50) NULL,
  numeroDocumento VARCHAR(50) NULL,
  documentoCliente VARCHAR(50) NULL,
  nombreCliente VARCHAR(150) NULL,
  idUsuario INT NULL,
  montoPago DECIMAL(18,2) NOT NULL DEFAULT 0,
  montoCambio DECIMAL(18,2) NOT NULL DEFAULT 0,
  montoTotal DECIMAL(18,2) NOT NULL DEFAULT 0,
  usuarioRegistro VARCHAR(50) NOT NULL DEFAULT SUSER_NAME(),
  fechaRegistro DATETIME NOT NULL DEFAULT GETDATE(),
  estado SMALLINT NOT NULL DEFAULT 1,
  CONSTRAINT fk_Venta_Usuario FOREIGN KEY (idUsuario) REFERENCES Usuario(id)
);
GO

-- Tabla: DetalleVenta (lineas de venta)
CREATE TABLE DetalleVenta (
  id INT PRIMARY KEY IDENTITY(1,1),
  idVenta INT NOT NULL,
  idMedicamento INT NOT NULL,
  precioVenta DECIMAL(18,2) NOT NULL DEFAULT 0,
  cantidad INT NOT NULL DEFAULT 1,
  subTotal DECIMAL(18,2) NOT NULL DEFAULT 0,
  usuarioRegistro VARCHAR(50) NOT NULL DEFAULT SUSER_NAME(),
  fechaRegistro DATETIME NOT NULL DEFAULT GETDATE(),
  estado SMALLINT NOT NULL DEFAULT 1,
  CONSTRAINT fk_DetalleVenta_Venta FOREIGN KEY (idVenta) REFERENCES Venta(id),
  CONSTRAINT fk_DetalleVenta_Medicamento FOREIGN KEY (idMedicamento) REFERENCES Medicamento(id)
);
GO

-- PROCEDIMIENTOS ALMACENADOS (listas / para CRUD y búsqueda)
-- Listar Proveedores
IF OBJECT_ID('paProveedorListar','P') IS NOT NULL DROP PROCEDURE paProveedorListar;
GO
CREATE PROCEDURE paProveedorListar @parametro VARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT * FROM Proveedor
    WHERE estado <> -1 AND razonSocial LIKE '%' + REPLACE(@parametro,' ','%') + '%'
    ORDER BY razonSocial;
END
GO

-- Listar Pacientes
IF OBJECT_ID('paPacienteListar','P') IS NOT NULL DROP PROCEDURE paPacienteListar;
GO
CREATE PROCEDURE paPacienteListar @parametro VARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT * FROM Paciente
    WHERE estado <> -1 AND (
        documento LIKE '%' + REPLACE(@parametro,' ','%') + '%' OR
        nombreCompleto LIKE '%' + REPLACE(@parametro,' ','%') + '%' OR
        telefono LIKE '%' + REPLACE(@parametro,' ','%') + '%' OR
        email LIKE '%' + REPLACE(@parametro,' ','%') + '%'
    )
    ORDER BY nombreCompleto;
END
GO

-- Listar Categorias
IF OBJECT_ID('paCategoriaListar','P') IS NOT NULL DROP PROCEDURE paCategoriaListar;
GO
CREATE PROCEDURE paCategoriaListar @parametro VARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT * FROM Categoria
    WHERE estado <> -1 AND descripcion LIKE '%' + REPLACE(@parametro,' ','%') + '%'
    ORDER BY descripcion;
END
GO

-- Listar Medicamentos (básico)
IF OBJECT_ID('paMedicamentoListar','P') IS NOT NULL DROP PROCEDURE paMedicamentoListar;
GO
CREATE PROCEDURE paMedicamentoListar @parametro VARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT m.*, c.descripcion AS Categoria
    FROM Medicamento m
    JOIN Categoria c ON m.idCategoria = c.id
    WHERE m.estado <> -1 AND (
        m.nombre LIKE '%' + REPLACE(@parametro,' ','%') + '%' OR
        m.codigo LIKE '%' + REPLACE(@parametro,' ','%') + '%' OR
        c.descripcion LIKE '%' + REPLACE(@parametro,' ','%') + '%'
    )
    ORDER BY m.nombre;
END
GO

-- Listar Empleados (con info de usuario)
IF OBJECT_ID('paEmpleadoListar','P') IS NOT NULL DROP PROCEDURE paEmpleadoListar;
GO
CREATE PROCEDURE paEmpleadoListar @parametro VARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT e.id, e.cedulaIdentidad, e.nombres, ISNULL(e.primerApellido,'') AS primerApellido,
           ISNULL(e.segundoApellido,'') AS segundoApellido, e.direccion, e.celular, e.cargo,
           ISNULL(e.usuarioRegistro,'') AS usuarioRegistro, ISNULL(e.fechaRegistro,GETDATE()) AS fechaRegistro,
           ISNULL(u.id,0) AS idUsuario, ISNULL(u.usuario,'') AS usuario
    FROM Empleado e
    LEFT JOIN Usuario u ON e.id = u.idEmpleado
    WHERE e.estado <> -1
      AND (e.cedulaIdentidad + ' ' + e.nombres + ' ' + ISNULL(e.primerApellido,'') + ' ' + ISNULL(e.segundoApellido,'') LIKE '%' + REPLACE(@parametro,' ','%') + '%')
    ORDER BY e.nombres, e.primerApellido;
END
GO

-- Pequeño listado para controles (Medicamento)
IF OBJECT_ID('paMedicamentoPequenoListar','P') IS NOT NULL DROP PROCEDURE paMedicamentoPequenoListar;
GO
CREATE PROCEDURE paMedicamentoPequenoListar @parametro VARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT m.id, m.codigo, m.nombre, c.descripcion AS Categoria, m.stock, m.precioVenta
    FROM Medicamento m
    JOIN Categoria c ON m.idCategoria = c.id
    WHERE m.estado <> -1
      AND (@parametro IS NULL OR m.nombre LIKE '%' + REPLACE(@parametro,' ','%') + '%')
    ORDER BY m.nombre;
END
GO

-- Procedimiento para obtener detalles de compras
IF OBJECT_ID('ObtenerDetallesCompras','P') IS NOT NULL DROP PROCEDURE ObtenerDetallesCompras;
GO
CREATE PROCEDURE ObtenerDetallesCompras @Fecha DATE = NULL
AS
BEGIN
    SET NOCOUNT ON;
    SELECT
        c.fechaRegistro,
        c.tipoDocumento,
        c.numeroDocumento,
        c.montoTotal,
        c.usuarioRegistro,
        p.documento AS documentoProveedor,
        p.razonSocial AS razonSocialProveedor,
        pr.codigo AS codigoMedicamento,
        pr.nombre AS nombreMedicamento,
        ca.descripcion AS Categoria,
        d.precioCompra,
        d.precioVenta,
        d.cantidad,
        d.total
    FROM Compra c
    JOIN DetalleCompra d ON c.id = d.idCompra
    JOIN Proveedor p ON c.idProveedor = p.id
    JOIN Medicamento pr ON d.idMedicamento = pr.id
    JOIN Categoria ca ON pr.idCategoria = ca.id
    WHERE (@Fecha IS NULL OR CAST(c.fechaRegistro AS DATE) = @Fecha)
    ORDER BY c.fechaRegistro DESC;
END
GO

-- Datos de prueba: DetalleNegocio, Empleados, Usuarios, Proveedor, Paciente, Categoria, Medicamento
-- Informacion del negocio
INSERT INTO DetalleNegocio(nombre, direccion, nit)
VALUES ('Farmacia SALUD', 'Av. Marcelo Quiroga de Santa Cruz s/n', '123456789012');
GO

-- Empleados de prueba
INSERT INTO Empleado(cedulaIdentidad, nombres, primerApellido, segundoApellido, direccion, celular, cargo)
VALUES
('7246545','Manuel','Adolfo','Soto','Calle Junin N°54','77199626','Propietario'),
('7777777','Brayan','Serrudo','Lopez','San Juanillo','54656565','Encargado'),
('11111111','Sis457','----','----','----','33333333','Propietario');
GO

-- Usuarios de prueba (las claves presentadas aquí son placeholders encriptadas o hash simulados; en producción manejar hash)
INSERT INTO Usuario(idEmpleado, usuario, clave)
VALUES
(1, 'Adolfo', 'i0hcoO/nssY6WOs9pOp5Xw=='),
(2, 'BrayanS', 'i0hcoO/nssY6WOs9pOp5Xw=='),
(3, 'Sis457', 'i0hcoO/nssY6WOs9pOp5Xw==');
GO

-- Proveedor y Paciente de prueba
INSERT INTO Proveedor(documento, razonSocial, email, telefono, direccion)
VALUES ('987654321', 'Distribuidora FarmaPlus', 'contacto@farmaplus.com', '67676767', 'Zona Industrial');
GO

INSERT INTO Paciente(documento, nombreCompleto, email, telefono, direccion)
VALUES ('1234567', 'Juan Perez', 'juan.perez@gmail.com', '70000001', 'Calle Falsa 123');
GO

-- Categorias iniciales
INSERT INTO Categoria(descripcion) VALUES
('Medicamentos'),
('Vitaminas'),
('Higiene'),
('Accesorios'),
('Suplementos'),
('Cosmeticos'),
('Bebidas Saludables'),
('Equipos Medicos');
GO

-- Medicamento de ejemplo
INSERT INTO Medicamento(idCategoria, codigo, nombre, descripcion, tipoUnidad, stock, precioCompra, precioVenta)
VALUES (1, '001', 'Paracetamol 500mg', 'Analgésico y antipirético', 'Caja', 100, 10.00, 15.00);
GO

-- Nota: no se insertan DetalleCompra/DetalleVenta de prueba por seguridad (realizalo desde la app o con scripts específicos)

-- SELECTS de comprobación rápida (opcional)
SELECT TOP 10 * FROM DetalleNegocio;
SELECT TOP 10 id, cedulaIdentidad, nombres, primerApellido, celular FROM Empleado;
SELECT TOP 10 * FROM Usuario;
SELECT TOP 10 * FROM Proveedor;
SELECT TOP 10 * FROM Paciente;
SELECT TOP 20 * FROM Categoria;
SELECT TOP 50 id, codigo, nombre, stock, precioVenta FROM Medicamento;
GO

-- FIN DEL SCRIPT
