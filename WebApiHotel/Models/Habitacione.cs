using System;
using System.Collections.Generic;

namespace WebApiHotel.Models;

public partial class Habitacione
{
    public Guid IdHabitacion { get; set; }

    public Guid IdHotel { get; set; }

    public decimal CostoBase { get; set; }

    public decimal Impuestos { get; set; }

    public string TipoHabitacion { get; set; } = null!;

    public string Ubicacion { get; set; } = null!;

    public bool Activa { get; set; }

    public DateTime FechaCreacion { get; set; }

    public DateTime FechaModificacion { get; set; }

    public bool Disponible { get; set; }

    public virtual Hotele IdHotelNavigation { get; set; } = null!;

    public virtual ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
}
