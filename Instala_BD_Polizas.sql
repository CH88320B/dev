
/*Script de Instalacion ambiente de Mant de Poliza SQL Server*/

-- Creación de la base de datos
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'DBPolizas')
    CREATE DATABASE DBPolizas;
GO

USE DBPolizas;
GO

-- Verificar y eliminar tablas si existen antes de crearlas
IF OBJECT_ID('Clientes', 'U') IS NOT NULL DROP TABLE Clientes;
IF OBJECT_ID('Aseguradoras', 'U') IS NOT NULL DROP TABLE Aseguradoras;
IF OBJECT_ID('TiposPoliza', 'U') IS NOT NULL DROP TABLE TiposPoliza;
IF OBJECT_ID('EstadosPoliza', 'U') IS NOT NULL DROP TABLE EstadosPoliza;
IF OBJECT_ID('Coberturas', 'U') IS NOT NULL DROP TABLE Coberturas;
IF OBJECT_ID('Polizas', 'U') IS NOT NULL DROP TABLE Polizas;
IF OBJECT_ID('Usuarios', 'U') IS NOT NULL DROP TABLE Usuarios;
IF OBJECT_ID('Configuracion', 'U') IS NOT NULL DROP TABLE Configuracion;
GO

-- Creación de tablas
CREATE TABLE Clientes (
    CedulaAsegurado VARCHAR(20) PRIMARY KEY,
    Nombre VARCHAR(100),
    PrimerApellido VARCHAR(100),
    SegundoApellido VARCHAR(100),
    TipoPersona VARCHAR(50),
    FechaNacimiento DATE
);

CREATE TABLE Aseguradoras (
    AseguradoraId INT PRIMARY KEY IDENTITY,
    Nombre VARCHAR(100)
);

CREATE TABLE TiposPoliza (
    TipoPolizaId INT PRIMARY KEY IDENTITY,
    Nombre VARCHAR(100)
);

CREATE TABLE EstadosPoliza (
    EstadoPolizaId INT PRIMARY KEY IDENTITY,
    Nombre VARCHAR(50)
);

CREATE TABLE Coberturas (
    CoberturaId INT PRIMARY KEY IDENTITY,
    Nombre VARCHAR(100)
);

CREATE TABLE Polizas (
    NumeroPoliza VARCHAR(50) PRIMARY KEY,
    TipoPolizaId INT FOREIGN KEY REFERENCES TiposPoliza(TipoPolizaId),
    CedulaAsegurado VARCHAR(20) FOREIGN KEY REFERENCES Clientes(CedulaAsegurado),
    MontoAsegurado DECIMAL(18,2),
    FechaVencimiento DATE,
    FechaEmision DATE,
    CoberturaId INT FOREIGN KEY REFERENCES Coberturas(CoberturaId),
    EstadoPolizaId INT FOREIGN KEY REFERENCES EstadosPoliza(EstadoPolizaId),
    Prima DECIMAL(18,2),
    Periodo DATE,
    FechaInclusion DATE,
    AseguradoraId INT FOREIGN KEY REFERENCES Aseguradoras(AseguradoraId)
);

CREATE TABLE Usuarios (
    Id INT PRIMARY KEY IDENTITY,
    Login VARCHAR(50) NOT NULL UNIQUE,
    Contrasena VARCHAR(100) NOT NULL,
    NombreCompleto VARCHAR(100),
    Rol VARCHAR(50),
    Activo BIT DEFAULT 1
);

CREATE TABLE Configuracion (
    Clave VARCHAR(50) PRIMARY KEY,
    Valor VARCHAR(256)
);

-- Verifica y elimina índices si existen antes de crearlos
IF EXISTS (SELECT name FROM sys.indexes WHERE name = 'IDX_Clientes_CedulaAsegurado')
    DROP INDEX IDX_Clientes_CedulaAsegurado ON Clientes;
CREATE NONCLUSTERED INDEX IDX_Clientes_CedulaAsegurado ON Clientes (CedulaAsegurado);
GO

IF EXISTS (SELECT name FROM sys.indexes WHERE name = 'IDX_Polizas_TipoPolizaId')
    DROP INDEX IDX_Polizas_TipoPolizaId ON Polizas;
CREATE NONCLUSTERED INDEX IDX_Polizas_TipoPolizaId ON Polizas (TipoPolizaId);
GO

IF EXISTS (SELECT name FROM sys.indexes WHERE name = 'IDX_Polizas_CedulaAsegurado')
    DROP INDEX IDX_Polizas_CedulaAsegurado ON Polizas;
CREATE NONCLUSTERED INDEX IDX_Polizas_CedulaAsegurado ON Polizas (CedulaAsegurado);
GO

IF EXISTS (SELECT name FROM sys.indexes WHERE name = 'IDX_Polizas_AseguradoraId')
    DROP INDEX IDX_Polizas_AseguradoraId ON Polizas;
CREATE NONCLUSTERED INDEX IDX_Polizas_AseguradoraId ON Polizas (AseguradoraId);
GO

IF EXISTS (SELECT name FROM sys.indexes WHERE name = 'IDX_Polizas_EstadoPolizaId')
    DROP INDEX IDX_Polizas_EstadoPolizaId ON Polizas;
CREATE NONCLUSTERED INDEX IDX_Polizas_EstadoPolizaId ON Polizas (EstadoPolizaId);
GO

IF EXISTS (SELECT name FROM sys.indexes WHERE name = 'IDX_Usuarios_Login')
    DROP INDEX IDX_Usuarios_Login ON Usuarios;
CREATE NONCLUSTERED INDEX IDX_Usuarios_Login ON Usuarios (Login);
GO

-- Inserción de datos iniciales
INSERT INTO Aseguradoras VALUES ('INS Seguros');
INSERT INTO TiposPoliza VALUES ('Vehiculo');
INSERT INTO TiposPoliza VALUES ('Hogar');
INSERT INTO EstadosPoliza VALUES ('Vigente');
INSERT INTO Coberturas VALUES ('Total');
INSERT INTO Clientes VALUES ('01-8526-4930', 'Henderson', 'Castañeda', 'Silva', 'Nacional', '1981-09-08');

INSERT INTO Polizas (
    NumeroPoliza, TipoPolizaId, CedulaAsegurado, MontoAsegurado, FechaVencimiento,
    FechaEmision, CoberturaId, EstadoPolizaId, Prima, Periodo, FechaInclusion, AseguradoraId
) VALUES (
    'ABSHY14578', 1, '01-8526-4930', 257840.00, '2025-01-01',
    '2024-01-01', 1, 1, 253625, '2025-01-01', '2025-01-01', 1
);

INSERT INTO Usuarios (Login, Contrasena, NombreCompleto, Rol)
VALUES ('admin', 'Xk39PlqA7fZpLr8qVu12NcTYoQ3BmWg6', 'Administrador General', 'Admin');

INSERT INTO Configuracion (Clave, Valor)
VALUES ('JWT_SECRET', 'Xk39PlqA7fZpLr8qVu12NcTYoQ3BmWg6');
GO

