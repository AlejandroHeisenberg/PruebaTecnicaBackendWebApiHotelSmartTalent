using System.Linq.Expressions;
using System.Text.RegularExpressions;
using WebApiHotel.Application.Contract.AdministracionHotel;
using WebApiHotel.Domain.Services.Contract.AdministracionHoteles;
using WebApiHotel.DTOs.AdministracionHoteles;
using WebApiHotel.Models;

namespace WebApiHotel.Application.AdministracionHoteles
{
    public class AdministracionHotelesAppService : IAdministracionHotelesAppService
    {
        #region Field
        private readonly ILogger<AdministracionHotelesAppService> _logger;
        private readonly IAdministracionHotelesDomainService _administracionHotelesDomainService;

        #endregion Field

        #region controller
        public AdministracionHotelesAppService(
            ILogger<AdministracionHotelesAppService> logger,
            IAdministracionHotelesDomainService administracionHotelesDomainService
            )
        {
            _logger = logger;
            _administracionHotelesDomainService = administracionHotelesDomainService;
        }
        #endregion controller

        #region constantes
        #endregion constantes

        #region private methods
        //lista hoteles *EXTRA
        public List<GetListHotelesDTO> GetListHoteles()
        {
            try
            {
                List<GetListHotelesDTO> ListResultHotels = _administracionHotelesDomainService.GetListHoteles( ).Select(MapperHoteleToGetListHotelesDTO).ToList( );
                return ListResultHotels;
            }
            catch (Exception e)
            {
                return new List<GetListHotelesDTO>( )
                {
                    new GetListHotelesDTO
                    {
                        Error = e.Message
                    }
                };
            }
        }
        //obtiene el hotel buscando por id *EXTRA
        public GetHotelByIdDTO GetHotelById( Guid idHotel )
        {
            try
            {
                GetHotelByIdDTO DataHotelById = _administracionHotelesDomainService.GetHotelByID(idHotel).Select(MapperHoteleToGetObjHotelById).FirstOrDefault( );
                return DataHotelById;
            }
            catch (Exception e)
            {
                return new GetHotelByIdDTO( )
                {
                    Error = e.Message
                };
            }
        }
        //crea un nuevo hotel - ok
        public string CreateNewHotel( BaseStructureToCreateNewHotelDTO objHotelDto )
        {
            string mensaje = "";
            if (objHotelDto != null)
            {
                if (objHotelDto.NombreDTO               != null
                    && objHotelDto.DireccionDTO         != null
                    && objHotelDto.CiudadDTO            != null
                    && objHotelDto.TelefonoDTO          != null
                    && objHotelDto.CorreoElectronicoDTO != null
                )
                {
                    try
                    {
                        Hotele hotel = new Hotele
                        {
                            IdHotel           = Guid.NewGuid( ),
                            Nombre            = objHotelDto.NombreDTO,
                            Direccion         = objHotelDto.DireccionDTO,
                            Ciudad            = objHotelDto.CiudadDTO,
                            Telefono          = objHotelDto.TelefonoDTO,
                            CorreoElectronico = objHotelDto.CorreoElectronicoDTO,
                            Activo            = objHotelDto.ActivoDTO,
                            FechaCreacion     = DateTime.Now,
                            FechaModificacion = DateTime.Now
                        };
                        _administracionHotelesDomainService.CreateNewHotel(hotel);
                        mensaje = "Se ha creado correctamente el hotel {objHotelDto.NombreDTO}.";
                    }
                    catch (Exception e)
                    {
                        return e.Message.ToString( );
                    }
                }
                else {
                    mensaje = "Los campos son obligatorios, por favor termine de diligenciar los datos.";
                }
            }
            return mensaje;
        }
        //actualiza la informacion de un hotel - ok
        public string UpdateHotelById( Guid idHotel, DataUpdateHotelDTO objUpdateHotel )
        {
            string mensaje;
            try
            {
                mensaje = ValidateUpdateHotel(idHotel, objUpdateHotel);
                if (mensaje != "ok")
                {
                    return mensaje;
                }

                Hotele? hotel = _administracionHotelesDomainService.GetHotelByID(idHotel).FirstOrDefault( );

                hotel.Nombre            = objUpdateHotel.NombreHotelDTO;
                hotel.Direccion         = objUpdateHotel.DireccionHotelDTO;
                hotel.Ciudad            = objUpdateHotel.CiudadHotelDTO;
                hotel.Telefono          = objUpdateHotel.TelefonoHotelDTO;
                hotel.CorreoElectronico = objUpdateHotel.EmailHotelDTO;
                hotel.Activo            = objUpdateHotel.ActivoHotelDTO;
                hotel.FechaModificacion = DateTime.Now;

                _administracionHotelesDomainService.UpdateHotelById(hotel);
                mensaje = "Se ha actualizado correctamente el hotel.";

                return mensaje;
            }
            catch (Exception e)
            {
                return e.Message.ToString( );
            }
        }

        //habitaciones

        //asignar habitaciones a un hotel - ok
        public string AddingRoomsToHotel( Guid idHotel, List<StructureRoomTOAddingOrUpdateDTO> ListRooms )
        {
            try
            {
                var validationResult = ValidateAddRoom(idHotel, ListRooms);
                if (validationResult != "OK")
                    return validationResult;

                Hotele? hotel = _administracionHotelesDomainService.GetHotelByID(idHotel).FirstOrDefault( );
                if (hotel != null)
                {
                    List<Habitacione> ListHabitacionesAGuardar = ListRooms.Select(item => new Habitacione
                    {
                        IdHabitacion      = Guid.NewGuid( ),
                        IdHotel           = idHotel,
                        CostoBase         = item.CostoBaseHabitacionDTO,
                        Impuestos         = item.ImpuestosHabitacionDTO,
                        TipoHabitacion    = item.TipoHabitacionDTO,
                        Ubicacion         = item.UbicacionHabitacionDTO,
                        Activa            = item.ActivaHabitacionDTO,
                        FechaCreacion     = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        Disponible        = item.DisponibleHabitacionDTO
                    }).ToList( );

                    _administracionHotelesDomainService.AddListRoomsDTO(ListHabitacionesAGuardar);
                    return $"Se ha agregado la(s) habitacion(es) exitosamente al hotel {hotel.Nombre.ToUpper( )}";
                }
                else
                {
                    return $"No fue posible añadir la(s) habitaciones al hotel, el ID de hotel suministrado no existe en nuestra base de datos, por favor valide";
                }
            }
            catch (Exception e)
            {

                return e.Message;
            }

        }

        //obtiene la informacion de una habitacion buscando por el id de la habitacion *EXTRA
        public GetRoomByIdDTO GetRoomById( Guid IdRoom )
        {
            try
            {
                GetRoomByIdDTO ?dataRoom = _administracionHotelesDomainService.GetRoomByID(IdRoom).Select(MapperHabitacioneToGetRoomByIdDTO).FirstOrDefault();
                if (dataRoom != null)
                {
                    return dataRoom;
                }
                else 
                {
                    return new GetRoomByIdDTO
                    {
                        ErrorHabitacionDTO = "No se Encontro informacion de la habitacion con ese Id suministrado, por favor valide."
                    };
                }
            }
            catch (Exception e)
            {
                return new GetRoomByIdDTO
                {
                    ErrorHabitacionDTO = e.Message
                };
            }
            
        }
        //actualiza la informacion de habitacion - ok
        public string UpdateRoomsByID( Guid idRoom, StructureRoomTOAddingOrUpdateDTO dataObjInfRoomUpdate )
        {
            string mensaje ;
            try
            {
                mensaje = ValidateUpdateRoomById(idRoom, dataObjInfRoomUpdate);
                if (mensaje != "OK")
                {
                    return mensaje;
                }

                 Habitacione? roomData = _administracionHotelesDomainService.GetRoomByID(idRoom).FirstOrDefault( );
                 
                 roomData.CostoBase         = dataObjInfRoomUpdate.CostoBaseHabitacionDTO;
                 roomData.Impuestos         = dataObjInfRoomUpdate.ImpuestosHabitacionDTO;
                 roomData.TipoHabitacion    = dataObjInfRoomUpdate.TipoHabitacionDTO;
                 roomData.Ubicacion         = dataObjInfRoomUpdate.UbicacionHabitacionDTO;
                 roomData.Activa            = dataObjInfRoomUpdate.ActivaHabitacionDTO;
                 roomData.FechaModificacion = DateTime.Now;
                 roomData.Disponible        = dataObjInfRoomUpdate.DisponibleHabitacionDTO;

                 _administracionHotelesDomainService.UpdateRoomById(roomData);
                 mensaje = "Se ha actualizado correctamente La Habitación.";

                return mensaje;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        //habilitar/deshabilitar c/u hoteles individualmente - ok
         public string EnableDisableHotelSingular( Guid IdHotel, bool valor )
         {
            try
            {
                string mensaje = "";
                Hotele ?hotel = _administracionHotelesDomainService.GetHotelByID ( IdHotel ).FirstOrDefault();

                if (hotel.Activo == true && valor == true)
                {
                    mensaje = $"El Hotel {hotel.Nombre} ya se encuentra activo";
                }
                else if (hotel.Activo == false && valor == false)
                {
                    mensaje = $"El hotel {hotel.Nombre} ya se encuentra desactivado";
                }
                else 
                {
                    if(valor != hotel.Activo )
                        hotel.Activo = valor;

                    _administracionHotelesDomainService.UpdateHotelById ( hotel );

                    if (valor == true)
                    {
                        mensaje = $"Se ha activado el hotel {hotel.Nombre} con exito.";
                    }
                    else 
                    {
                        mensaje = $"Se ha desactivado el hotel {hotel.Nombre} con exito.";
                    }
                }
                return mensaje;
            }
            catch (Exception e)
            {

                return e.Message;
            }
            
         }
        //habilitar/deshabilitar c/u Hoteles y sus habitaciones que la incluyen, masivo *EXTRA pendiente


        //habilitar/deshabilitar c/u Habitaciones individualemente - ok
        public string EnableDisableRoomSingular( Guid IdRoom, bool valor )
        {
            try
            {
                string mensaje = "";
                Habitacione ?room = _administracionHotelesDomainService.GetRoomByID ( IdRoom ).FirstOrDefault();

                if (room.Activa == true && valor == true)
                {
                    mensaje = "La habitacion ya se encuentra activa";
                }
                else if (room.Activa == false && valor == false)
                {
                    mensaje = "La habitacion ya se encuentra desactivada";
                }
                else 
                {
                    if(valor != room.Activa )
                        room.Activa = valor;

                    _administracionHotelesDomainService.UpdateRoomById ( room );

                    if (valor == true)
                    {
                        mensaje = "Se ha activado la habitacion con exito.";
                    }
                    else 
                    {
                        mensaje = "Se ha desactivado la habitacion con exito.";
                    }
                }
                return mensaje;
            }
            catch (Exception e)
            {

                return e.Message;
            }
            
        }

        //habilitar/deshabilitar c/u Habitaciones masivo *EXTRA
        public string EnableDisableRoomPlural( List<Guid> listIdsRooms, bool valor )
        {
            string mensaje = "";
            try
            {
                // Validar la lista de IDs de habitaciones.
                if (listIdsRooms == null || !listIdsRooms.Any())
                {
                    throw new ArgumentException("La lista de IDs de habitaciones no puede estar vacía.");
                }

                foreach (var id in listIdsRooms)
                {
                    if (!Guid.TryParse(id.ToString(), out _))
                    {
                        throw new ArgumentException("El ID de la habitación no es válido: " + id);
                    }

                    if (!_administracionHotelesDomainService.GetRoomByID(id).Any())
                    {
                        throw new ArgumentException("La habitación con el ID " + id + " no existe en la base de datos.");
                    }
                }

                List<GetRoomByIdDTO> listRooms = _administracionHotelesDomainService.GetListRoomsSearchByIds( listIdsRooms ).Select(MapperHabitacioneToGetRoomByIdDTO ).ToList( );
                
                listRooms.ForEach(x => x.ActivaHabitacionDTO = valor);

                List<Habitacione> listHabitacionesFinal = new( );
                foreach (var item in listRooms)
                {
                    Habitacione habitacionFinal = new( );
                    habitacionFinal.IdHabitacion      = item.IdHabitacionDTO;
                    habitacionFinal.IdHotel           = item.IdHotelabitacionDTO;
                    habitacionFinal.CostoBase         = item.CostoBaseHabitacionDTO;
                    habitacionFinal.Impuestos         = item.ImpuestosHabitacionDTO;
                    habitacionFinal.TipoHabitacion    = item.TipoHabitacionDTO;
                    habitacionFinal.Ubicacion         = item.UbicacionHabitacionDTO;
                    habitacionFinal.Activa            = item.ActivaHabitacionDTO;
                    habitacionFinal.FechaCreacion = item.FechaCreacionHabitacionDTO;
                    habitacionFinal.FechaModificacion = DateTime.Now;
                    listHabitacionesFinal.Add( habitacionFinal );
                }
                
                _administracionHotelesDomainService.EnableDisableRoomPlural(listHabitacionesFinal);
                if (valor == false)
                {
                    mensaje = "Las Habitaciones han sido Desactivadas correctamente.";
                }
                else 
                {
                    mensaje = "las habitaciones han sido activadas correctamente.";                
                }
                return mensaje;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        #endregion private methods

        #region Function private
        private string ValidateUpdateHotel( Guid idHotel, DataUpdateHotelDTO objUpdateHotel )
    {
            string mensaje = string.Empty;
        
            // Validación de idHotel
            if (!Guid.TryParse(idHotel.ToString(), out Guid parsedGuid))
            {
                mensaje = "El ID del hotel no es un GUID válido.";
            }
            else if (idHotel == Guid.Empty)
            {
                mensaje = "El ID del hotel no puede ser vacío.";
            }
            else if (_administracionHotelesDomainService.GetHotelByID(idHotel).Count() == 0)
            {
                mensaje = "No se encontró un hotel con el ID especificado, por favor rectifique.";
            }
        
            // Validación de objUpdateHotel
            if (objUpdateHotel == null)
            {
                mensaje = "El objeto de datos del hotel es nulo.";
            }

            if (string.IsNullOrEmpty(objUpdateHotel.NombreHotelDTO))
            {
                mensaje = "El nombre del hotel no puede ser vacío.";
            }
            else if (objUpdateHotel.NombreHotelDTO.Length > 50)
            {
                mensaje = "El nombre del hotel no puede superar los 50 caracteres.";
            }

             // Validación de correo electrónico
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            Regex regex = new Regex(pattern);
            if (!regex.IsMatch(objUpdateHotel.EmailHotelDTO))
            {
               mensaje = "El correo electrónico del hotel no es válido.";
            }

            if (string.IsNullOrEmpty(mensaje))
            {
                return "ok";
            }
        
                return mensaje;
        }

        private string ValidateAddRoom(Guid idHotel, List<StructureRoomTOAddingOrUpdateDTO> ListRooms)
        {
            if (idHotel == Guid.Empty)
                return "El ID del hotel no puede ser vacío.";

            if (ListRooms == null || ListRooms.Count == 0)
                return "No se han enviado habitaciones para agregar.";

            foreach (var habitacion in ListRooms)
            {
                if (habitacion.CostoBaseHabitacionDTO <= 0)
                    return "El costo base de la habitación debe ser mayor a 0.";

                if (habitacion.ImpuestosHabitacionDTO < 0 || habitacion.ImpuestosHabitacionDTO > 100)
                    return "El porcentaje de impuestos debe estar entre 0 y 100.";

                if (string.IsNullOrEmpty(habitacion.TipoHabitacionDTO))
                    return "El tipo de habitación no puede ser vacío.";

                if (string.IsNullOrEmpty(habitacion.UbicacionHabitacionDTO))
                    return "La ubicación de la habitación no puede ser vacía.";
            }
            return "OK";
        }

        private string ValidateUpdateRoomById( Guid idRoom, StructureRoomTOAddingOrUpdateDTO dataObjInfRoomUpdate )
        {
            if (idRoom == Guid.Empty)
                return "El ID de la habitación no puede ser vacío.";

            if (dataObjInfRoomUpdate == null)
                return "No se han enviado datos para actualizar la habitación.";

            if (dataObjInfRoomUpdate.CostoBaseHabitacionDTO <= 0)
                return "El costo base de la habitación debe ser mayor a 0.";

            if (dataObjInfRoomUpdate.ImpuestosHabitacionDTO < 0 || dataObjInfRoomUpdate.ImpuestosHabitacionDTO > 100)
                return "El porcentaje de impuestos debe estar entre 0 y 100.";

            if (string.IsNullOrEmpty(dataObjInfRoomUpdate.TipoHabitacionDTO))
                return "El tipo de habitación no puede ser vacío.";

            if (string.IsNullOrEmpty(dataObjInfRoomUpdate.UbicacionHabitacionDTO))
                return "La ubicación de la habitación no puede ser vacía.";

            return "OK";
        }

        #endregion Function private

        #region expressions
        private static Expression<Func<Hotele, GetListHotelesDTO>> MapperHoteleToGetListHotelesDTO = data => new GetListHotelesDTO
        {
            IdHotelDTO                = data.IdHotel,
            NombreHotelDTO            = data.Nombre,
            DireccionHotelDTO         = data.Direccion,
            CiudadHotelDTO            = data.Ciudad,
            TelefonoHotelDTO          = data.Telefono,
            EmailHotelDTO             = data.CorreoElectronico,
            ActivoHotelDTO            = data.Activo,
            FechaCreacionHotelDTO     = data.FechaCreacion,
            FechaModificacionHotelDTO = data.FechaModificacion
        };

        private static Expression<Func<Hotele, GetHotelByIdDTO>> MapperHoteleToGetObjHotelById = data => new GetHotelByIdDTO
        {
            IdHotelDTO                = data.IdHotel,
            NombreHotelDTO            = data.Nombre,
            DireccionHotelDTO         = data.Direccion,
            CiudadHotelDTO            = data.Ciudad,
            TelefonoHotelDTO          = data.Telefono,
            EmailHotelDTO             = data.CorreoElectronico,
            ActivoHotelDTO            = data.Activo,
            FechaCreacionHotelDTO     = data.FechaCreacion,
            FechaModificacionHotelDTO = data.FechaModificacion
        };

        private static Expression<Func<Habitacione, GetRoomByIdDTO>> MapperHabitacioneToGetRoomByIdDTO = data => new GetRoomByIdDTO
        {
            IdHabitacionDTO        = data.IdHabitacion,
            IdHotelabitacionDTO    = data.IdHotel,           
            CostoBaseHabitacionDTO = data.CostoBase,
            ImpuestosHabitacionDTO = data.Impuestos,
            UbicacionHabitacionDTO = data.Ubicacion,
            ActivaHabitacionDTO    = data.Activa,
            TipoHabitacionDTO      = data.TipoHabitacion,
            FechaCreacionHabitacionDTO = data.FechaCreacion
            
        };
        
        #endregion expressions
    }
}
