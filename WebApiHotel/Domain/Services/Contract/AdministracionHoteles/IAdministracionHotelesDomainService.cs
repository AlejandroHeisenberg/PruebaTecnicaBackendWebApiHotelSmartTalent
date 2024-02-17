using WebApiHotel.Models;

namespace WebApiHotel.Domain.Services.Contract.AdministracionHoteles
{
    public interface IAdministracionHotelesDomainService
    {
        /// <summary>
        /// interface que ayuda a traer listado de hoteles
        /// </summary>
        /// <returns>Una lista con la información de los hoteles</returns>
        IQueryable<Hotele> GetListHoteles();

        /// <summary>
        /// metodo que ayuda a traer data de un hotel buscada por id
        /// </summary>
        /// <param name="IdHotel"></param>
        /// <returns>Un objeto con la información del hotel que se busca por id</returns>
        IQueryable<Hotele> GetHotelByID(Guid IdHotel);

        /// <summary>
        /// metodo que sirve para crear un nuevo hotel
        /// </summary>
        /// <param name="Hotel"></param>
        void CreateNewHotel(Hotele Hotel);    

        /// <summary>
        /// metodo que sirve para actualizar la informacion de un hotel en especifico buscado por id.
        /// </summary>
        /// <param name="idHotel"></param>
        /// <param name="dataToUpdate"></param>
        void UpdateHotelById(Hotele dataToUpdate);

       /// <summary>
       /// 
       /// </summary>
       /// <param name="ListRooms"></param>
        void AddListRoomsDTO( List<Habitacione> ListRooms);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="idRoom"></param>
        /// <returns></returns>
        IQueryable<Habitacione> GetRoomByID( Guid idRoom );

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataToUpdate"></param>
        void UpdateRoomById(Habitacione dataToUpdate);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idRoom"></param>
        /// <returns></returns>
        bool EnableDisableRoomPlural( List<Habitacione> idRoom);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idsRooms"></param>
        /// <returns></returns>
        IQueryable<Habitacione> GetListRoomsSearchByIds( List<Guid> idsRooms );
    }
}
