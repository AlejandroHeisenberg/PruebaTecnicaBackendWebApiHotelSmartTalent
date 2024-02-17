using Microsoft.IdentityModel.Tokens;
using System.Linq.Expressions;
using System.Net.Mail;
using System.Net;
using System.Text.RegularExpressions;
using WebApiHotel.Application.Contract.ReservaHotel;
using WebApiHotel.Domain.Services.Contract.ReservaHotel;
using WebApiHotel.DTOs.AdministracionHoteles;
using WebApiHotel.DTOs.ReservacionesHotel;
using WebApiHotel.Models;

namespace WebApiHotel.Application.ReservaHotel
{
    public class ReservaHotelAppService : IReservaHotelAppService
    {
        #region Field
        private readonly ILogger<ReservaHotelAppService> _logger;
        private readonly IReservaHotelDomainService _reservaHotelDomainService;
        #endregion Field

        #region controller
        public ReservaHotelAppService(
            ILogger<ReservaHotelAppService> logger,
            IReservaHotelDomainService reservaHotelDomainService
        )
        {
            _logger = logger;
            _reservaHotelDomainService = reservaHotelDomainService;  
        }
       
        #endregion controller

        #region private methods

         //2. Obtener habitaciones de un hotel:
         /// <summary>
         /// 
         /// </summary>
         /// <param name="idHotel"></param>
         /// <returns></returns>
        public List<ResultInfoDetailRoomByHotelDTO> GetListRoomsByHotel( Guid idHotel )
        {
            try
            {
                string mensaje = string.Empty;
                    // Validación de idHotel
                if (!Guid.TryParse(idHotel.ToString(), out Guid parsedGuid))
                {
                    mensaje = "El ID del hotel no es un GUID válido.";//*
                }
                else if (idHotel == Guid.Empty)
                {
                    mensaje = "El ID del hotel no puede ser vacío.";//*
                }

                var DataDetailRoomByHotel = _reservaHotelDomainService.GetRoomsByHotel(idHotel).Select(MapperHoteleToGetListHotelesDTO).ToList();
                return DataDetailRoomByHotel;
            }
            catch (Exception e)
            {

                throw;
            }
            
        }

        //3. Reservar una habitación:
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objectPayloadReserveDTO"></param>
        /// <returns></returns>
        public string ReserveRoomFromHotel(ObjectPayloadReserveDTO objectPayloadReserveDTO) 
        {
            string mensaje;
            mensaje = ValidarDataPayloadReserva(objectPayloadReserveDTO);

            if (string.IsNullOrEmpty(mensaje))
            {            
                try
                {
                    //consultamos disponibilidad de las habitaciones del hotel seleccionado
                    var disponibilidad = _reservaHotelDomainService.SearchDisponibilityRoom(objectPayloadReserveDTO.IdHabitacionDTO)
                        .Select(MapperReservaToInfoDetailRoomReserveDTO).ToList(); 
                   
                    foreach (var item in disponibilidad)
                    {
                        // Comparar el idHabitacion del objeto de entrada con el idHabitacion de la lista
                        if (item.IdHabitacionDTO == objectPayloadReserveDTO.IdHabitacionDTO)
                        {
                            // Validar si las fechas se cruzan
                            if ((objectPayloadReserveDTO.FechaEntradaDTO >= item.FechaEntradaDTO && objectPayloadReserveDTO.FechaEntradaDTO <= item.FechaSalidaDTO) ||
                                (objectPayloadReserveDTO.FechaSalidaDTO >= item.FechaEntradaDTO && objectPayloadReserveDTO.FechaSalidaDTO <= item.FechaSalidaDTO) ||
                                (objectPayloadReserveDTO.FechaEntradaDTO <= item.FechaEntradaDTO && objectPayloadReserveDTO.FechaSalidaDTO >= item.FechaSalidaDTO))
                            {
                                // Las fechas se cruzan y la habitación está ocupada
                                mensaje = "La habitación ya está ocupada para las fechas seleccionadas.";
                            }
                        }
                    }
                    Reserva objDataReserve = new Reserva( );

                    objDataReserve.IdHabitacion      = objectPayloadReserveDTO.IdHabitacionDTO;
                    objDataReserve.FechaEntrada      = objectPayloadReserveDTO.FechaEntradaDTO;
                    objDataReserve.FechaSalida       = objectPayloadReserveDTO.FechaSalidaDTO;
                    objDataReserve.CantidadPersonas  = objectPayloadReserveDTO.CantidadPersonasDTO;
                    objDataReserve.NombreContacto    = objectPayloadReserveDTO.NombreContactoDTO;
                    objDataReserve.TelefonoContacto  = objectPayloadReserveDTO.TelefonoContactoDTO;
                    objDataReserve.EmailContacto     = objectPayloadReserveDTO.CorreoContactoDTO;
                    objDataReserve.Estado            = "RESERVADO";
                    objDataReserve.FechaCreacion     = DateTime.Now;
                    objDataReserve.FechaModificacion = DateTime.Now;    
                    
                    _reservaHotelDomainService.SaveReserve( objDataReserve );

                    //actualizamos el estado de disponible de la habitacion en falso
                    Habitacione habitacionCambioStadoDisponible = _reservaHotelDomainService.GetRoomById(objectPayloadReserveDTO.IdHabitacionDTO).FirstOrDefault();
                    habitacionCambioStadoDisponible.Disponible = false;
                    _reservaHotelDomainService.SaveChangeStatusAvailable( habitacionCambioStadoDisponible );
                    
                    mensaje =  $"Su reserva ha sido confirmada con exito para el dia {objectPayloadReserveDTO.FechaEntradaDTO} hasta el día {objectPayloadReserveDTO.FechaSalidaDTO}";
                 
                }
                catch (Exception e)
                {

                    return e.Message;
                }
                }
            
            return mensaje;
        }
        //4. Obtener detalles de una reserva x id:

        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdReserve"></param>
        /// <returns></returns>
        public ReservesDetailDTO GetReserveDetailById(Guid IdReserve)
        {
            string mensaje = "";
            mensaje = ValidateGuid(IdReserve);
            try
            {
                if (mensaje.IsNullOrEmpty( ))
                {
                    ReservesDetailDTO ?DataReserveDetailById = _reservaHotelDomainService.GetReserveDetailById(IdReserve).Select(MapperReservaToListReservesDetailDTODTO).FirstOrDefault( );
                    if (DataReserveDetailById != null)
                    {
                        return DataReserveDetailById;
                    }
                    else { 
                        return new ReservesDetailDTO( ) 
                        { 
                            ErrorDTO = $"No se encontro información de esta reserva con el Id {IdReserve}",
                        };
                    }
                }
                else
                { 
                    return new ReservesDetailDTO( ) 
                    { 
                        ErrorDTO = mensaje,
                    };
                }
            }
            catch (Exception e )
            {

                return new ReservesDetailDTO( ) 
                { 
                    ErrorDTO = e.Message,
                };
            }
        }
        // obtener listado de reservas 
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<ReservesDetailDTO> GetListAllReserveDetail()
        {
            try
            {
                List<ReservesDetailDTO> ?DataReserveDetailById = _reservaHotelDomainService.GetListAllReserveDetail().Select(MapperReservaToListReservesDetailDTODTO).ToList( );
                if( DataReserveDetailById != null )
                {
                    return DataReserveDetailById; 
                } 
                else 
                { 
                    return new List<ReservesDetailDTO>( )
                    {
                        new ReservesDetailDTO()
                        {
                            ErrorDTO= "No existen reservas por el momento.",
                        }
                };  }
            }
            catch (Exception e)
            {
                return new List<ReservesDetailDTO>( )
                {
                    new ReservesDetailDTO()
                    {
                        ErrorDTO= e.Message,
                    }
                };
                throw;
            }
        }
        
        //consulta de parametros multiples


        //enviar correo
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reserva"></param>
        /// <param name="emailDestino"></param>
        /// <returns></returns>
        public string SendEmailConfirmationReserve(ReservesDetailDTO reserva, string emailDestino)
        {
            using (var smtpClient = new SmtpClient())
            {
                // Configurar servidor SMTP
                smtpClient.Host = "smtp.gmail.com";
                smtpClient.Port = 587;
                smtpClient.EnableSsl = true;
        
                // **Credenciales de la aplicación:**
                var appCredentials = new NetworkCredential(
                    "alejandro032893reyes@gmail.com", 
                    "oyuf onor ueal umke"
                );
                smtpClient.Credentials = appCredentials;
        
                // Crear mensaje
                var mailMessage = new MailMessage();
                mailMessage.From = new MailAddress("alejandro032893reyes@gmail.com", "Hotel" + reserva.NombreHotelDTO);
                mailMessage.To.Add(emailDestino);
                mailMessage.Subject = "Reserva Habitación - " + reserva.NombreHotelDTO;
        
                // Plantilla HTML
                var body = $@"
                    <h1>Reserva confirmada en {reserva.NombreHotelDTO}</h1>
                    <p>
                        Gracias por reservar en {reserva.NombreHotelDTO}.
                    </p>
                    <ul>
                        <li>Dirección hotel:      {reserva.DireccionHotelDTO}</li>
                        <li>Fecha de entrada:     {reserva.FechaEntradaDTO}</li>
                        <li>Fecha de salida:      {reserva.FechaSalidaDTO}</li>
                        <li>Cantidad de personas: {reserva.CantidadPersonasDTO}</li>
                        <li>Tipo habitación:      {reserva.TipoHabitacionDTO}</li>
                        <li>Ubicación habitación: {reserva.UbicacionHabitacionDTO}</li>
                        <li>Costo total:          {reserva.CostoBaseDTO + reserva.ImpuestoDTO}</li>
                    </ul>
                    <p>
                        Para más información, contacte al hotel al {reserva.TelefonoHotelDTO}.
                        <br>
                        <a href='https://www.youtube.com/channel/UCj8FZA_9bwQPlXuGwXS6eug'>enlace de youtube del hotel</a>
                    </p>
                ";
        
                mailMessage.Body = body;
                mailMessage.IsBodyHtml = true;
        
                // Enviar correo
                try
                {
                    smtpClient.Send(mailMessage);
                    return "Mensaje enviado correctamente";
                }
                catch (Exception ex)
                {
                    return $"Error al enviar correo: {ex.Message}";
                }
            }
        }

        #endregion private methods

        #region private Fuction
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objectPayloadReserveDTO"></param>
        /// <returns></returns>
        private string ValidarDataPayloadReserva( ObjectPayloadReserveDTO objectPayloadReserveDTO )
        {
            string mensaje = "";

            // Validar ID del hotel
            mensaje = ValidateGuid( objectPayloadReserveDTO.IdHotelDTO ); 
            // Validar ID de la habitación
            mensaje = ValidateGuid(objectPayloadReserveDTO.IdHabitacionDTO);

            // Validar fechas
            if (objectPayloadReserveDTO.FechaEntradaDTO >= objectPayloadReserveDTO.FechaSalidaDTO)
            {
                mensaje = "La fecha de entrada debe ser menor a la fecha de salida.";
            }

            // Validar cantidad de personas
            if (objectPayloadReserveDTO.CantidadPersonasDTO <= 0)
            {
                mensaje = "La cantidad de personas debe ser mayor a 0.";
            }

            // Validar nombre
            if (string.IsNullOrEmpty(objectPayloadReserveDTO.NombreContactoDTO))
            {
                mensaje = "El nombre del contacto es obligatorio.";            }

            // Validar telefono
            if (!string.IsNullOrEmpty(objectPayloadReserveDTO.TelefonoContactoDTO) && !IsValidPhoneNumber(objectPayloadReserveDTO.TelefonoContactoDTO))
            {
                mensaje = "El número de teléfono no es válido.";
            }

            // Validar correo
            if (!string.IsNullOrEmpty(objectPayloadReserveDTO.CorreoContactoDTO) && !IsValidEmail(objectPayloadReserveDTO.CorreoContactoDTO))
            {
                mensaje = "El correo electrónico no es válido.";
                return mensaje;
            }

            return mensaje;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private string ValidateGuid( Guid id)
        {
            string mensaje  = "";
            if (id == Guid.Empty)
            {
                mensaje = "El parámetro Id no puede ser nulo o vacío.";
            }

            if (!Guid.TryParse(id.ToString(), out Guid parsedGuid))
            {
                mensaje = "El parámetro Id no es un Guid válido.";
            }

            return mensaje;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        private bool IsValidPhoneNumber( string phoneNumber )
        {
            Regex regex = new Regex(@"^\d{10}$");
            return regex.IsMatch(phoneNumber);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        private bool IsValidEmail(string email)
        {            
            Regex regex = new Regex(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4})$");
            return regex.IsMatch(email);               
        }
        #endregion private function

        #region Expressions
        private static Expression<Func<Hotele, ResultInfoDetailRoomByHotelDTO>> MapperHoteleToGetListHotelesDTO = data => new ResultInfoDetailRoomByHotelDTO
        {
            IdHotelDTO = data.IdHotel,
            NombreHotelDTO = data.Nombre,
            DireccionHotelDTO = data.Direccion,
            CiudadHotelDTO = data.Ciudad,
            TelefonoHotelDTO = data.Telefono,
            EmailHotelDTO = data.CorreoElectronico,
            ActivoHotelDTO = data.Activo,
            FechaCreacionHotelDTO = data.FechaCreacion,
            FechaModificacionHotelDTO = data.FechaModificacion,
            ListHabitacionesDTO = data.Habitaciones.Select(x => new GetRoomByIdDTO( )
            {
                IdHabitacionDTO = x.IdHabitacion,
                IdHotelabitacionDTO = x.IdHotel,
                CostoBaseHabitacionDTO = x.CostoBase,
                ImpuestosHabitacionDTO = x.Impuestos,
                TipoHabitacionDTO = x.TipoHabitacion,
                UbicacionHabitacionDTO = x.Ubicacion,
                ActivaHabitacionDTO = x.Activa,
                DisponibleDTO = x.Disponible


            }).ToList( )
        };

        private static Expression<Func<Reserva, InfoDetailRoomReserveDTO>> MapperReservaToInfoDetailRoomReserveDTO = data => new InfoDetailRoomReserveDTO
        {
            IdHotelDTO        = data.IdHabitacionNavigation.IdHotel,
            IdHabitacionDTO   = data.IdHabitacion,
            FechaEntradaDTO   = data.FechaEntrada,
            FechaSalidaDTO    = data.FechaSalida,
        };

        private static Expression<Func<Reserva, ReservesDetailDTO>> MapperReservaToListReservesDetailDTODTO = data => new ReservesDetailDTO
        {
            IdReservaDTO                =  data.IdReserva,
            IdHotelDTO                  =  data.IdHabitacionNavigation.IdHotel,
            NombreHotelDTO              =  data.IdHabitacionNavigation.IdHotelNavigation.Nombre,
            DireccionHotelDTO           =  data.IdHabitacionNavigation.IdHotelNavigation.Direccion,
            CiudadHotelDTO              =  data.IdHabitacionNavigation.IdHotelNavigation.Ciudad,
            TelefonoHotelDTO            =  data.IdHabitacionNavigation.IdHotelNavigation.Telefono,
            EmailHotelDTO               =  data.IdHabitacionNavigation.IdHotelNavigation.CorreoElectronico,
            IdHabitacionDTO             =  data.IdHabitacionNavigation.IdHabitacion,
            FechaEntradaDTO             =  data.FechaEntrada,
            FechaSalidaDTO              =  data.FechaSalida,
            CantidadPersonasDTO         =  data.CantidadPersonas,
            NombreContactoDTO           =  data.NombreContacto,
            TelefonoContactoDTO         =  data.TelefonoContacto,
            CostoBaseDTO                =  data.IdHabitacionNavigation.CostoBase,
            ImpuestoDTO                 =  data.IdHabitacionNavigation.Impuestos,
            TipoHabitacionDTO           =  data.IdHabitacionNavigation.TipoHabitacion,
            UbicacionHabitacionDTO      =  data.IdHabitacionNavigation.Ubicacion,
            EmailContactoDTO            =  data.EmailContacto,
            EstadoDTO                   =  data.Estado,
            FechaCreacionReservacionDTO =  data.FechaCreacion,
        };
        #endregion Expressions

    }
}
