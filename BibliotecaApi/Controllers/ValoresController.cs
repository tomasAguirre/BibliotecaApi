using BibliotecaApi.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace BibliotecaApi.Controllers
{
    [ApiController]
    [Route("api/valores")]
    public class ValoresController : ControllerBase
    {
        private readonly IRepositorioValores repositorioValores;
        private readonly ServicioTransient transient1;
        private readonly ServicioTransient transient2;
        private readonly ServicioScoped servicioScoped1;
        private readonly ServicioScoped servicioScoped2;
        private readonly ServicioSingleton servicioSingleton;

        public ValoresController(IRepositorioValores repositorioValores,
            ServicioTransient transient1,
            ServicioTransient transient2,
            ServicioScoped servicioScoped1,
            ServicioScoped servicioScoped2, 
            ServicioSingleton servicioSingleton)
        {
            this.repositorioValores = repositorioValores;
            this.transient1 = transient1;
            this.transient2 = transient2;
            this.servicioScoped1 = servicioScoped1;
            this.servicioScoped2 = servicioScoped2;
            this.servicioSingleton = servicioSingleton;
        }

        [HttpGet("servicios")]
        public IActionResult GetServicios() 
        {
            return Ok(new
            {
                Transients = new
                {
                    transient1 = transient1.obtenerGuid,
                    transient2 = transient2.obtenerGuid

                },
                Scopeds = new 
                {
                    scoped1 = servicioScoped1.obtenerGuid, 
                    scoped2 = servicioScoped2.obtenerGuid
                },
                Singleton = new 
                {
                    singleton = servicioSingleton.obtenerGuid
                }
            });
        }

        [HttpGet]
        public IEnumerable<Valor> Get() 
        {
            return repositorioValores.obtenerValores();
        }

        [HttpPost]
        public IActionResult Post(Valor valor) 
        {
            repositorioValores.InsertarValor(valor);
            return Ok();
        } 

    }
}
