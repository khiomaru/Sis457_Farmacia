-- =============================================
-- SCRIPT: CREACIÓN DE BASE DE DATOS FARMACIA
-- BASE DE DATOS: Labsis457Farmacia
-- USUARIO: usrfarmacia
-- FECHA: 2025
-- =============================================

-- Crear base de datos
CREATE DATABASE Labsis457Farmacia;
GO

-- Usar la base de datos master para crear el login
USE [master]
GO

-- Crear login de usuario para la aplicación
CREATE LOGIN [usrfarmacia] WITH 
    PASSWORD = N'123456',  -- CONTRASEÑA SEGURA
    DEFAULT_DATABASE = [Labsis457Farmacia],
    CHECK_EXPIRATION = OFF,
    CHECK_POLICY = ON;
GO

-- Asignar permisos en la base de datos
USE [Labsis457Farmacia]
GO

-- Crear usuario en la base de datos
CREATE USER [usrfarmacia] FOR LOGIN [usrfarmacia];
GO

-- Asignar rol de db_owner para control total sobre la base de datos
ALTER ROLE [db_owner] ADD MEMBER [usrfarmacia];
GO

-- =============================================
-- ELIMINAR TABLAS Y PROCEDIMIENTOS SI EXISTEN (LIMPIEZA)
-- =============================================
DROP TABLE IF EXISTS DetalleVenta;
DROP TABLE IF EXISTS Venta;
DROP TABLE IF EXISTS Usuario;
DROP TABLE IF EXISTS Empleado;
DROP TABLE IF EXISTS Cliente;
DROP TABLE IF EXISTS Medicamento;
DROP TABLE IF EXISTS Categoria;
DROP TABLE IF EXISTS Laboratorio;
GO

DROP PROC IF EXISTS paVentaListar;
DROP PROC IF EXISTS paClienteListar;
DROP PROC IF EXISTS paMedicamentoListar;
DROP PROC IF EXISTS paEmpleadoListar;
DROP PROC IF EXISTS paLaboratorioListar;
GO

-- =============================================
-- CREACIÓN DE TABLAS
-- =============================================

-- Tabla: Laboratorio (Fabricantes de medicamentos)
CREATE TABLE Laboratorio (
    id INT PRIMARY KEY IDENTITY(1,1),
    nombre VARCHAR(100) NOT NULL UNIQUE,
    pais VARCHAR(50),
    estado SMALLINT NOT NULL DEFAULT 1
);

-- Tabla: Categoria (Tipos de medicamentos)
CREATE TABLE Categoria (
    id INT PRIMARY KEY IDENTITY(1,1),
    nombre VARCHAR(50) NOT NULL UNIQUE,
    descripcion VARCHAR(250),
    estado SMALLINT NOT NULL DEFAULT 1
);

-- Tabla: Medicamento (Productos)
CREATE TABLE Medicamento (
    id INT PRIMARY KEY IDENTITY(1,1),
    idCategoria INT NOT NULL,
    idLaboratorio INT NOT NULL,
    codigo VARCHAR(20) NOT NULL UNIQUE,
    nombre VARCHAR(100) NOT NULL,
    descripcion VARCHAR(250),
    composicion VARCHAR(250),
    fechaVencimiento DATE NOT NULL,
    stock INT NOT NULL DEFAULT 0,
    precioVenta DECIMAL(10,2) NOT NULL CHECK (precioVenta > 0),
    requiereReceta BIT NOT NULL DEFAULT 0,
    usuarioRegistro VARCHAR(50) NOT NULL DEFAULT SUSER_NAME(),
    fechaRegistro DATETIME NOT NULL DEFAULT GETDATE(),
    estado SMALLINT NOT NULL DEFAULT 1,
    CONSTRAINT FK_Medicamento_Categoria FOREIGN KEY (idCategoria) REFERENCES Categoria(id),
    CONSTRAINT FK_Medicamento_Laboratorio FOREIGN KEY (idLaboratorio) REFERENCES Laboratorio(id)
);

-- Tabla: Cliente (Pacientes)
CREATE TABLE Cliente (
    id INT PRIMARY KEY IDENTITY(1,1),
    cedulaIdentidad VARCHAR(12) NOT NULL UNIQUE,
    nombres VARCHAR(100) NOT NULL,
    apellidos VARCHAR(100) NOT NULL,
    telefono BIGINT,
    direccion VARCHAR(250),
    usuarioRegistro VARCHAR(50) NOT NULL DEFAULT SUSER_NAME(),
    fechaRegistro DATETIME NOT NULL DEFAULT GETDATE(),
    estado SMALLINT NOT NULL DEFAULT 1
);

-- Tabla: Empleado (Personal de la farmacia)
CREATE TABLE Empleado (
    id INT PRIMARY KEY IDENTITY(1,1),
    cedulaIdentidad VARCHAR(12) NOT NULL UNIQUE,
    nombres VARCHAR(50) NOT NULL,
    primerApellido VARCHAR(50),
    segundoApellido VARCHAR(50),
    direccion VARCHAR(250) NOT NULL,
    celular BIGINT NOT NULL,
    cargo VARCHAR(50) NOT NULL, -- Farmaceútico, Técnico, Cajero, Administrador
    usuarioRegistro VARCHAR(50) NOT NULL DEFAULT SUSER_NAME(),
    fechaRegistro DATETIME NOT NULL DEFAULT GETDATE(),
    estado SMALLINT NOT NULL DEFAULT 1
);

-- Tabla: Usuario (Credenciales de acceso)
CREATE TABLE Usuario (
    id INT PRIMARY KEY IDENTITY(1,1),
    idEmpleado INT NOT NULL,
    usuario VARCHAR(50) UNIQUE NOT NULL,
    clave VARCHAR(255) NOT NULL,
    usuarioRegistro VARCHAR(50) NOT NULL DEFAULT SUSER_NAME(),
    fechaRegistro DATETIME NOT NULL DEFAULT GETDATE(),
    estado SMALLINT NOT NULL DEFAULT 1,
    CONSTRAINT fk_Usuario_Empleado FOREIGN KEY(idEmpleado) REFERENCES Empleado(id)
);

-- Tabla: Venta (Pedidos/Facturas)
CREATE TABLE Venta (
    id INT PRIMARY KEY IDENTITY(1,1),
    idUsuario INT NOT NULL,
    idCliente INT NOT NULL,
    numeroFactura AS 'FAC-' + CAST(id AS VARCHAR(10)),
    total DECIMAL(10,2) NOT NULL DEFAULT 0,
    fechaVenta DATETIME NOT NULL DEFAULT GETDATE(),
    usuarioRegistro VARCHAR(50) NOT NULL DEFAULT SUSER_NAME(),
    fechaRegistro DATETIME NOT NULL DEFAULT GETDATE(),
    estado SMALLINT NOT NULL DEFAULT 1, -- 1: Activa, 0: Anulada
    CONSTRAINT fk_Venta_Usuario FOREIGN KEY(idUsuario) REFERENCES Usuario(id),
    CONSTRAINT fk_Venta_Cliente FOREIGN KEY(idCliente) REFERENCES Cliente(id)
);

-- Tabla: DetalleVenta (Detalle de la venta)
CREATE TABLE DetalleVenta (
    id INT PRIMARY KEY IDENTITY(1,1),
    idVenta INT NOT NULL,
    idMedicamento INT NOT NULL,
    cantidad INT NOT NULL CHECK (cantidad > 0),
    precioUnitario DECIMAL(10,2) NOT NULL,
    subtotal AS (cantidad * precioUnitario),
    usuarioRegistro VARCHAR(50) NOT NULL DEFAULT SUSER_NAME(),
    fechaRegistro DATETIME NOT NULL DEFAULT GETDATE(),
    estado SMALLINT NOT NULL DEFAULT 1,
    CONSTRAINT fk_DetalleVenta_Venta FOREIGN KEY(idVenta) REFERENCES Venta(id),
    CONSTRAINT fk_DetalleVenta_Medicamento FOREIGN KEY(idMedicamento) REFERENCES Medicamento(id)
);

GO

-- =============================================
-- CREACIÓN DE PROCEDIMIENTOS ALMACENADOS
-- =============================================

-- Procedimiento: Listar Ventas
CREATE PROC paVentaListar @parametro VARCHAR(100) = ''
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        v.id, 
        v.numeroFactura, 
        c.nombres + ' ' + c.apellidos AS Cliente,
        u.usuario AS Usuario, 
        v.total,
        v.fechaVenta, 
        v.estado
    FROM Venta v
    INNER JOIN Cliente c ON c.id = v.idCliente
    INNER JOIN Usuario u ON u.id = v.idUsuario
    WHERE v.estado <> -1
    AND (c.nombres + ' ' + c.apellidos + u.usuario + v.numeroFactura) 
        LIKE '%' + REPLACE(@parametro, ' ', '%') + '%'
    ORDER BY v.fechaVenta DESC;
END
GO

-- Procedimiento: Listar Clientes
CREATE PROC paClienteListar @parametro VARCHAR(100) = ''
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        c.id, 
        c.cedulaIdentidad, 
        c.nombres, 
        c.apellidos,
        c.telefono,
        c.direccion,
        c.usuarioRegistro, 
        c.fechaRegistro, 
        c.estado
    FROM Cliente c
    WHERE c.estado <> -1
    AND (c.cedulaIdentidad + c.nombres + c.apellidos) 
        LIKE '%' + REPLACE(@parametro, ' ', '%') + '%'
    ORDER BY c.nombres, c.apellidos;
END
GO

-- Procedimiento: Listar Medicamentos
CREATE PROC paMedicamentoListar @parametro VARCHAR(100) = ''
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        m.id, 
        m.codigo,
        m.nombre, 
        m.descripcion, 
        m.composicion,
        ca.nombre AS Categoria, 
        l.nombre AS Laboratorio,
        m.fechaVencimiento,
        m.stock, 
        m.precioVenta,
        m.requiereReceta,
        m.usuarioRegistro, 
        m.fechaRegistro, 
        m.estado, 
        m.idCategoria,
        m.idLaboratorio
    FROM Medicamento m
    INNER JOIN Categoria ca ON ca.id = m.idCategoria
    INNER JOIN Laboratorio l ON l.id = m.idLaboratorio
    WHERE m.estado <> -1 
    AND (m.nombre + m.codigo + m.descripcion + m.composicion + ca.nombre + l.nombre) 
        LIKE '%' + REPLACE(@parametro, ' ', '%') + '%'
    ORDER BY m.estado DESC, m.nombre ASC;
END
GO

-- Procedimiento: Listar Empleados
CREATE PROC paEmpleadoListar @parametro VARCHAR(50) = ''
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        e.id, 
        e.cedulaIdentidad, 
        e.nombres, 
        ISNULL(e.primerApellido, '') AS primerApellido,
        ISNULL(e.segundoApellido, '') AS segundoApellido, 
        e.direccion, 
        e.celular, 
        e.cargo,
        e.usuarioRegistro, 
        e.fechaRegistro,
        ISNULL(u.id, 0) as idUsuario, 
        ISNULL(u.usuario, '') as usuario,
        e.estado
    FROM Empleado e
    LEFT JOIN Usuario u ON e.id = u.idEmpleado
    WHERE e.estado <> -1
    AND (e.cedulaIdentidad + e.nombres + e.primerApellido + e.segundoApellido + e.cargo) 
        LIKE '%' + REPLACE(@parametro, ' ', '%') + '%'
    ORDER BY e.nombres, e.primerApellido;
END
GO

-- Procedimiento: Listar Laboratorios
CREATE PROC paLaboratorioListar @parametro VARCHAR(100) = ''
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        id, 
        nombre, 
        pais,
        estado
    FROM Laboratorio
    WHERE estado <> -1
    AND (nombre + ISNULL(pais, '')) LIKE '%' + REPLACE(@parametro, ' ', '%') + '%'
    ORDER BY nombre;
END
GO

-- =============================================
-- INSERTAR DATOS DE EJEMPLO (DML)
-- =============================================

-- Laboratorios
INSERT INTO Laboratorio(nombre, pais) VALUES ('Bayer', 'Alemania');
INSERT INTO Laboratorio(nombre, pais) VALUES ('Pfizer', 'Estados Unidos');
INSERT INTO Laboratorio(nombre, pais) VALUES ('Roche', 'Suiza');
INSERT INTO Laboratorio(nombre, pais) VALUES ('GSK', 'Reino Unido');

-- Categorías
INSERT INTO Categoria(nombre, descripcion) VALUES ('Antibióticos', 'Medicamentos para combatir infecciones bacterianas');
INSERT INTO Categoria(nombre, descripcion) VALUES ('Analgésicos', 'Medicamentos para el dolor');
INSERT INTO Categoria(nombre, descripcion) VALUES ('Antiinflamatorios', 'Medicamentos para reducir inflamación');
INSERT INTO Categoria(nombre, descripcion) VALUES ('Antipiréticos', 'Medicamentos para bajar la fiebre');
INSERT INTO Categoria(nombre, descripcion) VALUES ('Vitaminas', 'Suplementos vitamínicos');

-- Medicamentos
INSERT INTO Medicamento(idCategoria, idLaboratorio, codigo, nombre, descripcion, composicion, fechaVencimiento, stock, precioVenta, requiereReceta)
VALUES (1, 1, 'MED001', 'Amoxicilina 500mg', 'Antibiótico de amplio espectro', 'Amoxicilina trihidrato', '2025-12-31', 100, 25.50, 1);

INSERT INTO Medicamento(idCategoria, idLaboratorio, codigo, nombre, descripcion, composicion, fechaVencimiento, stock, precioVenta, requiereReceta)
VALUES (2, 2, 'MED002', 'Paracetamol 500mg', 'Analgésico y antipirético', 'Paracetamol', '2025-06-30', 200, 8.75, 0);

INSERT INTO Medicamento(idCategoria, idLaboratorio, codigo, nombre, descripcion, composicion, fechaVencimiento, stock, precioVenta, requiereReceta)
VALUES (3, 3, 'MED003', 'Ibuprofeno 400mg', 'Antiinflamatorio no esteroideo', 'Ibuprofeno', '2025-08-15', 150, 12.30, 0);

INSERT INTO Medicamento(idCategoria, idLaboratorio, codigo, nombre, descripcion, composicion, fechaVencimiento, stock, precioVenta, requiereReceta)
VALUES (5, 4, 'MED004', 'Vitamina C 1000mg', 'Suplemento de vitamina C', 'Ácido ascórbico', '2026-03-20', 300, 15.00, 0);

-- Clientes
INSERT INTO Cliente(cedulaIdentidad, nombres, apellidos, telefono, direccion)
VALUES ('1234567', 'Juan', 'Pérez Gonzáles', 71234567, 'Av. Central #123');

INSERT INTO Cliente(cedulaIdentidad, nombres, apellidos, telefono, direccion)
VALUES ('7654321', 'María', 'López García', 79876543, 'Calle 5 #456');

INSERT INTO Cliente(cedulaIdentidad, nombres, apellidos, telefono, direccion)
VALUES ('11223344', 'Carlos', 'Rodríguez Martínez', 70987654, 'Av. Principal #789');

-- Empleados
INSERT INTO Empleado(cedulaIdentidad, nombres, primerApellido, segundoApellido, direccion, celular, cargo)
VALUES ('9876543', 'Ana', 'Torres', 'Luna', 'Av. Los Leones #321', 69876543, 'Farmaceútico');

INSERT INTO Empleado(cedulaIdentidad, nombres, primerApellido, segundoApellido, direccion, celular, cargo)
VALUES ('55667788', 'Luis', 'Ramírez', 'Flores', 'Calle 10 #654', 71122334, 'Técnico Farmacéutico');

INSERT INTO Empleado(cedulaIdentidad, nombres, primerApellido, segundoApellido, direccion, celular, cargo)
VALUES ('99887766', 'Sofía', 'Mendoza', 'Vargas', 'Av. Circunvalación #987', 69988776, 'Cajero');

-- Clave encriptada: i0hcoO/nssY6WOs9pOp5Xw== (para "hola123")
-- Usaré la misma clave encriptada para el ejemplo
INSERT INTO Usuario(idEmpleado, usuario, clave)
VALUES (1, 'adolfo', 'i0hcoO/nssY6WOs9pOp5Xw==');

INSERT INTO Usuario(idEmpleado, usuario, clave)
VALUES (2, 'luisramirez', 'i0hcoO/nssY6WOs9pOp5Xw==');

INSERT INTO Usuario(idEmpleado, usuario, clave)
VALUES (3, 'sofiamendoza', 'i0hcoO/nssY6WOs9pOp5Xw==');

-- Ventas de ejemplo
INSERT INTO Venta(idUsuario, idCliente, total, fechaVenta)
VALUES (1, 1, 34.25, GETDATE());

INSERT INTO DetalleVenta(idVenta, idMedicamento, cantidad, precioUnitario)
VALUES (1, 1, 1, 25.50);

INSERT INTO DetalleVenta(idVenta, idMedicamento, cantidad, precioUnitario)
VALUES (1, 2, 1, 8.75);

-- =============================================
-- FIN DEL SCRIPT
-- =============================================
GO

-- Verificar creación de objetos
SELECT 'Base de datos creada exitosamente' AS Mensaje;
SELECT name AS TablaCreada FROM sys.tables WHERE type = 'U';
SELECT name AS ProcedimientoCreado FROM sys.procedures;
GO