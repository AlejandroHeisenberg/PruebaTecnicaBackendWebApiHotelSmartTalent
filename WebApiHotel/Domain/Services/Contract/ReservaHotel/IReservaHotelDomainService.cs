using WebApiHotel.DTOs.ReservacionesHotel;
using WebApiHotel.Models;

namespace WebApiHotel.Domain.Services.Contract.ReservaHotel
{
    public interface IReservaHotelDomainService
    {
        IQueryable<Hotele> GetRoomsByHotel(Guid Idhotel);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objectPayloadReserveDTO"></param>
        void ReserveRoomFromHotel( Reserva objectPayloadReserveDTO );

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idHabitacion"></param>
        /// <returns></returns>
        IQueryable<Reserva> SearchDisponibilityRoom( Guid idHabitacion );

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objDataReserve"></param>
        void SaveReserve( Reserva objDataReserve );

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objSaveChangeStatusAvailableRoom"></param>
        void SaveChangeStatusAvailable( Habitacione objSaveChangeStatusAvailableRoom );

        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdRoom"></param>
        /// <returns></returns>
        IQueryable<Habitacione> GetRoomById( Guid IdRoom );

        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdReserve"></param>
        /// <returns></returns>
        IQueryable<Reserva> GetReserveDetailById( Guid IdReserve );

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IQueryable<Reserva> GetListAllReserveDetail( );


    }
}
