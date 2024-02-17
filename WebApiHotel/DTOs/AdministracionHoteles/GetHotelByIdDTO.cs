namespace WebApiHotel.DTOs.AdministracionHoteles
{
    public class GetHotelByIdDTO
    {
        public Guid     IdHotelDTO                { get; set; }
        public string   NombreHotelDTO            { get; set; }
        public string   DireccionHotelDTO         { get; set; }
        public string   CiudadHotelDTO            { get; set; }
        public string   TelefonoHotelDTO          { get; set; }
        public string   EmailHotelDTO             { get; set; }
        public bool     ActivoHotelDTO            { get; set; }
        public DateTime FechaCreacionHotelDTO     { get; set; }
        public DateTime FechaModificacionHotelDTO { get; set; }
        public string   Error                     { get; set; } // Se agrega la propiedad Error
    }
}
