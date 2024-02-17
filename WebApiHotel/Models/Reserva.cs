using System;
using System.Collections.Generic;

namespace WebApiHotel.Models;

public partial class Reserva
{
    public Guid IdReserva { get; set; }

    public Guid IdHabitacion { get; set; }

    public DateTime FechaEntrada { get; set; }

    public DateTime FechaSalida { get; set; }

    public int CantidadPersonas { get; set; }

    public string NombreContacto { get; set; } = null!;

    public string TelefonoContacto { get; set; } = null!;

    public string EmailContacto { get; set; } = null!;

    public string Estado { get; set; } = null!;

    public DateTime FechaCreacion { get; set; }

    public DateTime FechaModificacion { get; set; }

    public virtual ICollection<ContactosEmergencium> ContactosEmergencia { get; set; } = new List<ContactosEmergencium>();

    public virtual Habitacione IdHabitacionNavigation { get; set; } = null!;

    public virtual ICollection<Pasajero> Pasajeros { get; set; } = new List<Pasajero>();
}
