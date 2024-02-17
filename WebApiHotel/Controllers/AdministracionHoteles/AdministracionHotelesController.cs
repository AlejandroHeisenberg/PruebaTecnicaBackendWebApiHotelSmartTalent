using Microsoft.AspNetCore.Mvc;
using WebApiHotel.Application.Contract.AdministracionHotel;
using WebApiHotel.DTOs.AdministracionHoteles;

namespace WebApiHotel.Controllers.AdministracionHoteles
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdministracionHotelesController : Controller
    {
        private readonly IAdministracionHotelesAppService _administracionHotelesAppService;

        #region Constructor
        public AdministracionHotelesController(IAdministracionHotelesAppService administracionHotelesAppService)
        {
            _administracionHotelesAppService = administracionHotelesAppService;
        }
        #endregion Constructor

        #region endPoints

        
        /// <summary>
        /// metodo que devuelve una lista de hoteles
        /// </summary>
        /// <returns></returns>
        // GET: api/Hoteles
        [HttpGet]
        [Route(nameof(GetListHoteles))]
        public List<GetListHotelesDTO> GetListHoteles() => _administracionHotelesAppService.GetListHoteles();

        /// <summary>
        /// metodo que obtiene un hotel buscado por id
        /// </summary>
        /// <param name="idHotel"></param>
        /// <returns></returns>
        // GET: api/Hoteles/{id}
        [HttpGet]
        [Route(nameof(GetHotelByID))]
        public GetHotelByIdDTO GetHotelByID(Guid idHotel) => _administracionHotelesAppService.GetHotelById(idHotel);

        /// <summary>
        /// Metodo que crea un nuevo hotel
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        //POST: api/Hotel
        [HttpPost]
        [Route(nameof(CreateNewHotel))]
        public string CreateNewHotel(BaseStructureToCreateNewHotelDTO data) => _administracionHotelesAppService.CreateNewHotel(data);

        /// <summary>
        /// Actualiza la informacion de un hotel buscando por id
        /// </summary>
        /// <param name="idHotel"></param>
        /// <param name="objData"></param>
        /// <returns>una texto de confirmacion o negacion al momento de actualizar un hotel</returns>
        [HttpPut]
        [Route(nameof(UpdateHotelById))]
        public string UpdateHotelById(Guid idHotel, DataUpdateHotelDTO objData) => _administracionHotelesAppService.UpdateHotelById(idHotel, objData);

        //habitaciones

        /// <summary>
        /// metodo que sirve para agregar una o varias habitaciones al hotel
        /// </summary>
        /// <param name="idHotel"></param>
        /// <param name="onRoomAdding"></param>
        /// <returns>Retorna un mensaje confirmacion o negacion si se realizo exitosamente o no la operacion de agregar habitaciones al hotel</returns>
        [HttpPost]
        [Route(nameof(AddingRoomsToHotel))]
        public string AddingRoomsToHotel(Guid idHotel, List<StructureRoomTOAddingOrUpdateDTO> onRoomAdding) => _administracionHotelesAppService.AddingRoomsToHotel(idHotel, onRoomAdding);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdRoom"></param>
        /// <param name="dataRoomUpdate"></param>
        /// <returns></returns>
        [HttpPut]
        [Route(nameof(UpdateRoomsByID))]
        public string UpdateRoomsByID(Guid IdRoom, StructureRoomTOAddingOrUpdateDTO dataRoomUpdate) => _administracionHotelesAppService.UpdateRoomsByID(IdRoom, dataRoomUpdate);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idRoom"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(nameof(EnableDisableRoomSingular))]
        public string EnableDisableRoomSingular(Guid idRoom, bool valor) => _administracionHotelesAppService.EnableDisableRoomSingular(idRoom, valor);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="listIdsRooms"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(nameof(EnableDisableRoomPlural))]
        public string EnableDisableRoomPlural(List<Guid> listIdsRooms, bool valor) => _administracionHotelesAppService.EnableDisableRoomPlural(listIdsRooms, valor);
        #endregion endPoints
    }
}
