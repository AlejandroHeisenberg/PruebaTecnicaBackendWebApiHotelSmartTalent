namespace WebApiHotel.DTOs.AdministracionHoteles
{
    public class GetRoomByIdDTO
    {
        public Guid    IdHabitacionDTO        { get; set; }
        public Guid    IdHotelabitacionDTO    { get; set; }
        public decimal CostoBaseHabitacionDTO { get; set; }
        public decimal ImpuestosHabitacionDTO { get; set; }
        public string  TipoHabitacionDTO      { get; set; }
        public string  UbicacionHabitacionDTO { get; set; }
        public bool    ActivaHabitacionDTO    { get; set; }
        public DateTime FechaCreacionHabitacionDTO    { get; set; }
        public string  ErrorHabitacionDTO     { get; set; }
        public bool  DisponibleDTO     { get; set; }
    }
}
