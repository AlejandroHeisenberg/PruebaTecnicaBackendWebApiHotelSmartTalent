using System;
using System.Collections.Generic;

namespace WebApiHotel.Models;

public partial class ContactosEmergencium
{
    public Guid IdContactoEmergencia { get; set; }

    public Guid IdReserva { get; set; }

    public string Nombres { get; set; } = null!;

    public string Telefono { get; set; } = null!;

    public virtual Reserva IdReservaNavigation { get; set; } = null!;
}
