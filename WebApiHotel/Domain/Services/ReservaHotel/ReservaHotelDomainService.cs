using Microsoft.EntityFrameworkCore;
using WebApiHotel.Domain.Services.Contract.ReservaHotel;
using WebApiHotel.DTOs.ReservacionesHotel;
using WebApiHotel.Models;

namespace WebApiHotel.Domain.Services.ReservaHotel
{
    public class ReservaHotelDomainService : IReservaHotelDomainService
    {
        private readonly HotelBdContext Context;
        public ReservaHotelDomainService( HotelBdContext _Context )
        {
            Context = _Context;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Idhotel"></param>
        /// <returns></returns>
        public IQueryable<Hotele> GetRoomsByHotel( Guid Idhotel )
        {
            IQueryable<Hotele> listRoomsByHotel = Context.Hoteles
                .Include(a => a.Habitaciones)
                .Where(x => x.IdHotel.Equals(Idhotel)).AsNoTracking( );
            return listRoomsByHotel;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objectPayloadReserveDTO"></param>
        public void ReserveRoomFromHotel( Reserva objectPayloadReserveDTO )
        {
            string mensaje;
            Context.Reservas.Add(objectPayloadReserveDTO);
            Context.SaveChanges( );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idHabitacion"></param>
        /// <returns></returns>
        public IQueryable<Reserva> SearchDisponibilityRoom( Guid idHabitacion )
        {
            IQueryable<Reserva> habitacionesDelHotel = Context.Reservas
                .Include(y => y.IdHabitacionNavigation)
                //.Where(x => x.IdHabitacionNavigation.Disponible == true)
                .AsNoTracking( );

            return habitacionesDelHotel;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objDataReserve"></param>
        public void SaveReserve( Reserva objDataReserve )
        {
            Context.Reservas.Add(objDataReserve);
            Context.SaveChanges( );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objSaveChangeStatusAvailableRoom"></param>
        public void SaveChangeStatusAvailable( Habitacione objSaveChangeStatusAvailableRoom )
        {
            Context.Habitaciones.Update(objSaveChangeStatusAvailableRoom);
            Context.SaveChanges( );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdRoom"></param>
        /// <returns></returns>
        public IQueryable<Habitacione> GetRoomById( Guid IdRoom )
        {
            IQueryable<Habitacione> data = Context.Habitaciones.AsNoTracking( );
            return data;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdReserve"></param>
        /// <returns></returns>
        public IQueryable<Reserva> GetReserveDetailById( Guid IdReserve )
        {
            IQueryable<Reserva> dataReserveDetail = Context.Reservas
                .Include(a => a.IdHabitacionNavigation)
                 .ThenInclude(b => b.IdHotelNavigation)
                .Where(x => x.IdReserva.Equals(IdReserve))
                .AsNoTracking( );
            return dataReserveDetail;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IQueryable<Reserva> GetListAllReserveDetail()
        {
            IQueryable<Reserva> dataListReserveDetail =  Context.Reservas
                .Include(a => a.IdHabitacionNavigation)
                 .ThenInclude(b => b.IdHotelNavigation)
                .AsNoTracking( ); 
            return dataListReserveDetail;
        }

        
    }
}
