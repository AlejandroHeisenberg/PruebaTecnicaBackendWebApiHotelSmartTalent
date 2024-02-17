//using DB;
using Microsoft.EntityFrameworkCore;
using WebApiHotel.Application.AdministracionHoteles;
using WebApiHotel.Application.Contract.AdministracionHotel;
using WebApiHotel.Application.Contract.ReservaHotel;
using WebApiHotel.Application.ReservaHotel;
using WebApiHotel.Domain.Services.AdministracionHoteles;
using WebApiHotel.Domain.Services.Contract.AdministracionHoteles;
using WebApiHotel.Domain.Services.Contract.ReservaHotel;
using WebApiHotel.Domain.Services.ReservaHotel;
using WebApiHotel.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//Agregar variable de coneccion
var connectionString = builder.Configuration.GetConnectionString("Connection");

////registramos nuestro servicio para la conexion 
builder.Services.AddDbContext<HotelBdContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddLogging(logging => logging.ClearProviders( ).AddConsole( ));
builder.Services.AddScoped<IAdministracionHotelesAppService, AdministracionHotelesAppService>( );
builder.Services.AddScoped<IAdministracionHotelesDomainService, AdministracionHotelesDomainService>( );
builder.Services.AddScoped<IReservaHotelAppService, ReservaHotelAppService>( );
builder.Services.AddScoped<IReservaHotelDomainService, ReservaHotelDomainService>( );




builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//using (var scope = app.Services.CreateScope())
//{
//    var context = scope.ServiceProvider.GetRequiredService<HotelBDContext>( );
//    context.Database.Migrate();
//}
    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment( ))
    {
        app.UseSwagger( );
        app.UseSwaggerUI( );
    }

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
