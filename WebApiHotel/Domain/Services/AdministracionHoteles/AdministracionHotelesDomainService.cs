using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using WebApiHotel.Domain.Services.Contract.AdministracionHoteles;
using WebApiHotel.DTOs.AdministracionHoteles;
using WebApiHotel.Models;

namespace WebApiHotel.Domain.Services.AdministracionHoteles
{
    public class AdministracionHotelesDomainService : IAdministracionHotelesDomainService
    {
        private readonly HotelBdContext Context;
        public AdministracionHotelesDomainService( HotelBdContext _Context)
        {
            Context = _Context;
        }
        public IQueryable<Hotele> GetListHoteles() 
        {
            IQueryable<Hotele> dataHoteles = Context.Hoteles
            .AsNoTracking( );
            return dataHoteles;
        }

        public IQueryable<Hotele> GetHotelByID( Guid idHotel )
        { 
            IQueryable<Hotele> dataHotel = Context.Hoteles
                .Where(x => x.IdHotel.Equals(idHotel)).AsNoTracking( ); 
            return dataHotel;   
        }

        public void CreateNewHotel( Hotele objDataNewHotel )
        {
            Context.Hoteles.Add(objDataNewHotel);
            Context.SaveChanges();
        }

        public void UpdateHotelById( Hotele objUpdate )
        { 
            Context.Hoteles.Update(objUpdate);  
            Context.SaveChanges();
        }

        public void AddListRoomsDTO( List<Habitacione> ListRooms )
        {
            Context.Habitaciones.AddRange( ListRooms );
            Context.SaveChanges();  
        }

        public IQueryable<Habitacione> GetRoomByID( Guid idRoom )
        {
            IQueryable<Habitacione> data = Context.Habitaciones
                .Where(b => b.IdHabitacion.Equals(idRoom))
                .AsNoTracking( );

                return data;
        }

        public void UpdateRoomById( Habitacione dataToUpdate )
        {
           Context.Habitaciones.Update(dataToUpdate);
            Context.SaveChanges();
        }

        public IQueryable<Habitacione> GetListRoomsSearchByIds( List<Guid> listIdsRooms )
        {
            IQueryable<Habitacione> data = Context.Habitaciones
                .Include(x => x.IdHotelNavigation)
                .Where(x => listIdsRooms.Contains(x.IdHabitacion));
            return data;
        }

        public bool EnableDisableRoomPlural( List<Habitacione> listIdsRooms  )
        {
            Context.Habitaciones.UpdateRange(listIdsRooms);
            Context.SaveChanges( );
            return true;    
        }
    }
}
