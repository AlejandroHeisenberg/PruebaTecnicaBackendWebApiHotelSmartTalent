CREATE DATABASE HotelBD

GO

USE HotelBD

GO

CREATE TABLE [Hoteles] (
    [IdHotel] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(),
    [Nombre] VARCHAR(255) NOT NULL,
    [Direccion] VARCHAR(255) NOT NULL,
    [Ciudad] VARCHAR(255) NOT NULL,
    [Telefono] VARCHAR(255) NOT NULL,
    [CorreoElectronico] VARCHAR(255) NOT NULL,
    [Activo] BIT NOT NULL DEFAULT 1,
    [FechaCreacion] DATETIME NOT NULL DEFAULT GETDATE(),
    [FechaModificacion] DATETIME NOT NULL DEFAULT GETDATE()
);

CREATE TABLE [Habitaciones] (
    [IdHabitacion] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(),
    [IdHotel] UNIQUEIDENTIFIER NOT NULL FOREIGN KEY REFERENCES [Hoteles]([IdHotel]),
    [CostoBase] DECIMAL(18,2) NOT NULL,
    [Impuestos] DECIMAL(18,2) NOT NULL,
    [TipoHabitacion] VARCHAR(255) NOT NULL,
    [Ubicacion] VARCHAR(255) NOT NULL,
    [Activa] BIT NOT NULL DEFAULT 1,
    [FechaCreacion] DATETIME NOT NULL DEFAULT GETDATE(),
    [FechaModificacion] DATETIME NOT NULL DEFAULT GETDATE()
);

CREATE TABLE [Reservas] (
    [IdReserva] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(),
    [IdHabitacion] UNIQUEIDENTIFIER NOT NULL FOREIGN KEY REFERENCES [Habitaciones]([IdHabitacion]),
    [FechaEntrada] DATETIME NOT NULL,
    [FechaSalida] DATETIME NOT NULL,
    [CantidadPersonas] INT NOT NULL,
    [NombreContacto] VARCHAR(255) NOT NULL,
    [TelefonoContacto] VARCHAR(255) NOT NULL,
    [EmailContacto] VARCHAR(255) NOT NULL,
    [Estado] VARCHAR(255) NOT NULL DEFAULT 'Pendiente',
    [FechaCreacion] DATETIME NOT NULL DEFAULT GETDATE(),
    [FechaModificacion] DATETIME NOT NULL DEFAULT GETDATE()
);

CREATE TABLE [Pasajeros] (
    [IdPasajero] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(),
    [IdReserva] UNIQUEIDENTIFIER NOT NULL FOREIGN KEY REFERENCES [Reservas]([IdReserva]),
    [Nombres] VARCHAR(255) NOT NULL,
    [Apellidos] VARCHAR(255) NOT NULL,
    [FechaNacimiento] DATETIME NOT NULL,
    [Genero] VARCHAR(255) NOT NULL,
    [TipoDocumento] VARCHAR(255) NOT NULL,
    [NumeroDocumento] VARCHAR(255) NOT NULL,
    [Email] VARCHAR(255) NOT NULL,
    [Telefono] VARCHAR(255) NOT NULL
);

CREATE TABLE [ContactosEmergencia] (
    [IdContactoEmergencia] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(),
    [IdReserva] UNIQUEIDENTIFIER NOT NULL FOREIGN KEY REFERENCES [Reservas]([IdReserva]),
    [Nombres] VARCHAR(255) NOT NULL,
    [Telefono] VARCHAR(255) NOT NULL
);

GO

