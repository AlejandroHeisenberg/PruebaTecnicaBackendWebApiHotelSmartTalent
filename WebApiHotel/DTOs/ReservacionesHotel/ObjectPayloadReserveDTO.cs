namespace WebApiHotel.DTOs.ReservacionesHotel
{
    public class ObjectPayloadReserveDTO
    {
        public Guid     IdHotelDTO          { get; set; }
        public Guid     IdHabitacionDTO     { get; set; }
        public DateTime FechaEntradaDTO     { get; set; }
        public int      CantidadPersonasDTO { get; set; }
        public string   NombreContactoDTO   { get; set; }
        public string   TelefonoContactoDTO { get; set; }
        public DateTime FechaSalidaDTO      { get; set; }
        public string   CorreoContactoDTO   { get; set; }
        public bool     EstadoReserva       { get; set; }
    }
}
