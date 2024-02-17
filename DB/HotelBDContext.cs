using Microsoft.EntityFrameworkCore;

namespace DB
{
    public class HotelBDContext : DbContext
    {
        public HotelBDContext(DbContextOptions<HotelBDContext> options) : base(options)
        {
            
        }

        public DbSet<Hoteles>Hoteles { get; set; }
        public DbSet<Pasajeros>Pasajeros { get; set; }
        public DbSet<ContactosEmergencia>ContactosEmergencias { get; set; }
        public DbSet<Reservas>Reservas { get; set; }
    }
}
