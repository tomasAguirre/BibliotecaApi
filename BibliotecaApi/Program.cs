using BibliotecaApi;
using BibliotecaApi.Datos;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
// Area de servicios 

builder.Services.AddTransient<ServicioTransient>();
builder.Services.AddScoped<ServicioScoped>();
builder.Services.AddSingleton<ServicioSingleton>();

builder.Services.AddControllers().AddJsonOptions(opciones => 
                                    opciones.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles); //agregamos la configuracion de controladores
                               //opciones.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles lo utilizo ...
                               //para evitar en ciclo infinito entre las entidades ya que un autor tiene libros y libros tiene autorres ...

builder.Services.AddDbContext<ApplicationDbContext>(opciones =>     
                    opciones.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddSingleton<IRepositorioValores, RepositorioValoresOracle>();

var app = builder.Build();


// area de middlewares 

app.Use(async (contexto, next) =>
{
    //Viene la peticion 
    var logger = contexto.RequestServices.GetRequiredService<ILogger<Program>>();
    logger.LogInformation($"Peticion {contexto.Request.Method} {contexto.Request.Path}");
    await next.Invoke();
    //Se va la respuesta 
    logger.LogInformation($"Respuesta {contexto.Response.StatusCode}");
});

app.Use(async (contexto, next) =>
{
    if (contexto.Request.Path == "/bloqueado")
    {
        contexto.Response.StatusCode = 403;
        await contexto.Response.WriteAsync("Acceso denegado ");
    }
    else 
    {
        await next.Invoke();
    }
});

//aca indicamos que manejaremos las peticiones con controladores
app.MapControllers();

app.Run();
