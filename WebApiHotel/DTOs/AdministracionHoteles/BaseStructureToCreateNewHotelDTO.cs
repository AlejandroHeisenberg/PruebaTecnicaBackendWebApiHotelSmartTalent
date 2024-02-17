namespace WebApiHotel.DTOs.AdministracionHoteles
{
    public class BaseStructureToCreateNewHotelDTO
    {
        public required string   NombreDTO              { get; set; }
        public required string   DireccionDTO           { get; set; }
        public required string   CiudadDTO              { get; set; }
        public required string   TelefonoDTO            { get; set; }
        public required string   CorreoElectronicoDTO   { get; set; }
        public bool              ActivoDTO              { get; set; }

        public BaseStructureToCreateNewHotelDTO()
        {
            NombreDTO            = "";
            DireccionDTO         = "";
            CiudadDTO            = "";
            TelefonoDTO          = "";
            CorreoElectronicoDTO = "";
        }
    }
}
