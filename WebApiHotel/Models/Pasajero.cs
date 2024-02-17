using System;
using System.Collections.Generic;

namespace WebApiHotel.Models;

public partial class Pasajero
{
    public Guid IdPasajero { get; set; }

    public Guid IdReserva { get; set; }

    public string Nombres { get; set; } = null!;

    public string Apellidos { get; set; } = null!;

    public DateTime FechaNacimiento { get; set; }

    public string Genero { get; set; } = null!;

    public string TipoDocumento { get; set; } = null!;

    public string NumeroDocumento { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Telefono { get; set; } = null!;

    public virtual Reserva IdReservaNavigation { get; set; } = null!;
}
