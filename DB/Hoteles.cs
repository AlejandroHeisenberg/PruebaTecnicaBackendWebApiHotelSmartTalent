using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB
{
    public class Hoteles
    {
        public Guid     IdHotel           { get; set; }   
        public string   Nombre            { get; set; }
        public string   Direccion         { get; set; }
        public string   Ciudad            { get; set; }
        public double   telefono          { get; set; }
        public string   CorreoElectronico { get; set; }   
        public bool     Activo            { get; set; }  
        public DateTime FechaCreacion     { get; set; } 
        public DateTime FechaModificacion { get; set; } 

    }
}
