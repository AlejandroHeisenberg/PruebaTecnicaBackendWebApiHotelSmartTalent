namespace WebApiHotel.DTOs.ReservacionesHotel
{
    public class InfoDetailRoomReserveDTO
    {
        public Guid     IdHotelDTO         { get; set; }
        public Guid     IdHabitacionDTO    { get; set; }
        public DateTime FechaEntradaDTO    { get; set; }
        public DateTime FechaSalidaDTO     { get; set; }
    }
}
