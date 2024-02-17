namespace WebApiHotel.DTOs.ReservacionesHotel
{
    public class ReservesDetailDTO
    {
        public Guid     IdReservaDTO                    { get; set; }
        public Guid     IdHotelDTO                      { get; set; }
        public string   NombreHotelDTO                  { get; set; }
        public string   DireccionHotelDTO               { get; set; }
        public string   CiudadHotelDTO                  { get; set; }
        public string   TelefonoHotelDTO                { get; set; }
        public string   EmailHotelDTO                   { get; set; }
        public Guid     IdHabitacionDTO                 { get; set; }
        public DateTime FechaEntradaDTO                 { get; set; }
        public DateTime FechaSalidaDTO                  { get; set; }
        public int      CantidadPersonasDTO             { get; set; }
        public string   NombreContactoDTO               { get; set; }
        public decimal  CostoBaseDTO                    { get; set; }
        public decimal  ImpuestoDTO                     { get; set; }
        public string   TipoHabitacionDTO               { get; set; }
        public string   UbicacionHabitacionDTO          { get; set; }
        public string   TelefonoContactoDTO             { get; set; }
        public string   EmailContactoDTO                { get; set; }
        public string   EstadoDTO                       { get; set; }
        public DateTime FechaCreacionReservacionDTO     { get; set; }
        public string   ErrorDTO                        { get; set; }
    }
}
