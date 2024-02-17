namespace WebApiHotel.DTOs.AdministracionHoteles
{
    public class StructureRoomTOAddingOrUpdateDTO
    {
        public decimal CostoBaseHabitacionDTO { get; set; }
        public decimal ImpuestosHabitacionDTO { get; set; }
        public string  TipoHabitacionDTO      { get; set; }
        public string  UbicacionHabitacionDTO { get; set; }
        public bool    ActivaHabitacionDTO    { get; set; }
        public bool    DisponibleHabitacionDTO{ get; set; }
    }
}
