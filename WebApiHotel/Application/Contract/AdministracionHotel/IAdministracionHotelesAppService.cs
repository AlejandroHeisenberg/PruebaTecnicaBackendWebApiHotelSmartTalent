using WebApiHotel.DTOs.AdministracionHoteles;
using WebApiHotel.Models;

namespace WebApiHotel.Application.Contract.AdministracionHotel
{
    public interface IAdministracionHotelesAppService
    {
        /// <summary>
        /// metodo encargado de obtener el listado de los hoteles creados
        /// </summary>
        /// <returns>Una lista con la información de los hoteles</returns>
        List<GetListHotelesDTO> GetListHoteles();

        /// <summary>
        /// obtiene los datos del hotel buscado por el id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Un objeto con la información del hotel que se busca por i</returns>
        GetHotelByIdDTO GetHotelById(Guid idHotel);

        /// <summary>
        /// metodo que se usa para crear un nuevo hotel
        /// </summary>
        /// <param name="ObjHotelDto"></param>
        /// <returns>un texto de confirmacion de creacion del hotel o un mensaje de error si no tuvo exito en la creacion</returns>
        string CreateNewHotel(BaseStructureToCreateNewHotelDTO ObjHotelDto);

        /// <summary>
        /// Metodo que sirve para actualizar los datos de un hotel buscado por id 
        /// </summary>
        /// <param name="idHotel"></param>
        /// <param name="dataHotel"></param>
        /// <returns>un mensaje de confirmacion o negacion si se actilo o no correctamente el hotel</returns>
        string UpdateHotelById(Guid idHotel, DataUpdateHotelDTO dataHotel);

        /// <summary>
        /// metodo para agregar uno o varias habitaciones a un hotel
        /// </summary>
        /// <param name="idHotel"></param>
        /// <param name="onRoomAdding"></param>
        /// <returns>mensaje de confirmacion si se pudo agregar correctamente la habitacion al hotel</returns>
        string AddingRoomsToHotel(Guid idHotel, List<StructureRoomTOAddingOrUpdateDTO> ListRoomsAdding);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdRoom"></param>
        /// <param name="dataRoomUpdate"></param>
        /// <returns></returns>
        string UpdateRoomsByID(Guid IdRoom, StructureRoomTOAddingOrUpdateDTO dataRoomUpdate);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idRoom"></param>
        /// <returns></returns>
        string EnableDisableRoomSingular(Guid idRoom, bool valor);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="listIdsRooms"></param>
        /// <param name="valor"></param>
        /// <returns></returns>
        string EnableDisableRoomPlural(List<Guid> listIdsRooms, bool valor);




    }
}
