using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB
{
    public class Habitaciones
    {
        public Guid     IdHabitacion      { get; set; }
        public Guid     IdHotel           { get; set; }
        public decimal  ConstoBase        { get; set; }
        public decimal  Impuestos         { get; set; }
        public string   TipoHabitacion    { get; set; }
        public string   Ubicacion         { get; set; }
        public bool     Activa            { get; set; }
        public DateTime FechaCreacion     { get; set; }
        public DateTime FechaMOdificacion { get; set; }
    }
}
