using Microsoft.AspNetCore.Mvc;
using WebApiHotel.Application.Contract.ReservaHotel;
using WebApiHotel.DTOs.ReservacionesHotel;

namespace WebApiHotel.Controllers.ReservacionesHotel
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservaHotelController: Controller
    {
        private readonly IReservaHotelAppService _reservaHotelAppService;

        #region Constructor
        public ReservaHotelController(IReservaHotelAppService reservaHotelAppService)
        {
                _reservaHotelAppService = reservaHotelAppService;   
         
        }
        #endregion Constructor

        #region endPoints

        //2. Obtener habitaciones de un hotel:
        /// <summary>
        /// 
        /// </summary>
        /// <param name="idHotel"></param>
        /// <returns></returns>
        [HttpGet]
        [Route(nameof( GetListRoomsByHotel ))]
        public  List<ResultInfoDetailRoomByHotelDTO> GetListRoomsByHotel(Guid idHotel) => _reservaHotelAppService.GetListRoomsByHotel(idHotel);

        //3. Reservar una habitación de un hotel:
        [HttpPost]
        [Route(nameof(ReserveRoomFromHotel))]
        public string ReserveRoomFromHotel( ObjectPayloadReserveDTO objectPayloadReserveDTO ) => _reservaHotelAppService.ReserveRoomFromHotel( objectPayloadReserveDTO );

        //obitiene el detalle de la reserva buscado por id
        /// <summary>
        /// 
        /// </summary>
        /// <param name="idReserve"></param>
        /// <returns></returns>
        [HttpGet]
        [Route(nameof(GetReserveDetailById))]
        public ReservesDetailDTO GetReserveDetailById( Guid idReserve ) => _reservaHotelAppService.GetReserveDetailById( idReserve );
        
        //obitiene el detalle de la reserva de todas las reservas existentes
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route(nameof(GetListAllReserveDetail))]
        public List<ReservesDetailDTO> GetListAllReserveDetail(  ) => _reservaHotelAppService.GetListAllReserveDetail(  );

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reserva"></param>
        /// <param name="emailDestino"></param>
        /// <returns></returns>
         [HttpPost]
         [Route(nameof(EnviarCorreoConfirmacionReserva))]
         public string EnviarCorreoConfirmacionReserva([FromBody] ReservesDetailDTO reserva, string emailDestino) =>  _reservaHotelAppService.SendEmailConfirmationReserve(reserva, emailDestino);

         
        #endregion endPoints
    }
}
