using Microsoft.AspNetCore.Builder;

namespace BibliotecaApi
{
    public class LogearPeticionMidleware
    {
        private readonly RequestDelegate next;

        public LogearPeticionMidleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext contexto) 
        {
            //Viene la peticion 
            var logger = contexto.RequestServices.GetRequiredService<ILogger<Program>>();
            logger.LogInformation($"Peticion {contexto.Request.Method} {contexto.Request.Path}");
            await next.Invoke(contexto);
            //Se va la respuesta 
            logger.LogInformation($"Respuesta {contexto.Response.StatusCode}");
        }
    }

    //Segun en estandar de microsoft es buena practica usar una 
    //Clase estatica para poder usar mi extension del middleware en prograam 
    public static class logeaPeticionMiddlewareExtensions 
    {                                                    //Uso this para enviar toda mi clase
        public static IApplicationBuilder UseLogueaPeticion(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<LogearPeticionMidleware>();
        }
    }
}
