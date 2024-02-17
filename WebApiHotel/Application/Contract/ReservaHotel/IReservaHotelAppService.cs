using WebApiHotel.DTOs.ReservacionesHotel;

namespace WebApiHotel.Application.Contract.ReservaHotel
{
    public interface IReservaHotelAppService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="idHotel"></param>
        /// <returns></returns>
        List<ResultInfoDetailRoomByHotelDTO> GetListRoomsByHotel(Guid idHotel);
        string ReserveRoomFromHotel(ObjectPayloadReserveDTO objectPayloadReserveDTO);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdReserve"></param>
        /// <returns></returns>
        ReservesDetailDTO GetReserveDetailById( Guid IdReserve );
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        List<ReservesDetailDTO> GetListAllReserveDetail();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reserva"></param>
        /// <param name="emailDestino"></param>
        /// <returns></returns>
        string SendEmailConfirmationReserve(ReservesDetailDTO reserva, string emailDestino);
    }
}
