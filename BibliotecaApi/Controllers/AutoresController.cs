using BibliotecaApi.Datos;
using BibliotecaApi.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaApi.Controllers
{
    [ApiController]
    [Route("api/autores")] // [Route("api/[Controller]")]
    public class AutoresController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ILogger<AutoresController> Logger { get; }

        public AutoresController(ApplicationDbContext context, ILogger<AutoresController> logger)
        {
            _context = context;
            Logger = logger;
        }

        [HttpGet("/listado-de-autores")] // no es necesario usar la ruta del controlador api/autores
        [HttpGet] //pueden existir 2 rutas para una accion 
        public async Task<IEnumerable<Autor>> Get() 
        {
            Logger.LogTrace("Obteniendo el listado de autores : ");
            Logger.LogDebug("Obteniendo el listado de autores : ");
            Logger.LogInformation("Obteniendo el listado de autores : ");
            Logger.LogWarning("Obteniendo el listado de autores :");
            Logger.LogError("Obteniendo el listado de autores :");
            Logger.LogCritical("Obteniendo el listado de autores :");
            return await this._context.Autores.ToListAsync();
        }

        [HttpGet("{id:int}", Name ="ObtenerAutor")] //api/autores/1?incluirLibros  (el fromquery es opcional ), tambien puedo usar [FromHeader]
        public async Task<ActionResult<Autor>> Get(int id,[FromQuery] bool incluirLibros) 
        {
            var autor = await this._context.Autores
                                   .Include(a => a.Libros)
                                   .FirstOrDefaultAsync(x => x.Id == id);

            if (autor is null) {
                return NotFound();
            }

            return autor;
        }

        [HttpGet("{nombre:alpha}")] //api/autores/nombreAutor
        public async Task<ActionResult<Autor>> Get(string nombre)
        {
            var autor = await this._context.Autores
                                   .Include(a => a.Libros)
                                   .FirstOrDefaultAsync(x => x.Nombre.Contains(nombre));

            if (autor is null)
            {
                return NotFound();
            }

            return autor;
        }

        [HttpGet("primero")] //api/autores/primero
        public async Task<Autor> GetPrimerAutor() 
        {
            return await this._context.Autores.FirstAsync();
        }

        //[HttpGet("{parametro1}/{parametro2?}")] //la ? indica que el parametro es opcional 
        //public ActionResult Get(string parametro1, string? parametro2) 
        //{
        //    return Ok(new { parametro1, parametro2});
        //}

        [HttpPost]                //opcional [FromBody] Autor autor 
        public async Task<ActionResult> Post(Autor autor) 
        {
            this._context.Add(autor);
            await this._context.SaveChangesAsync();
            return CreatedAtRoute("ObtenerAutor", new {Id=autor.Id }, autor);
            //return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, Autor autor) 
        {
            if (id != autor.Id) 
            {
                return BadRequest("Los ids deben de coincidir");
            }
            this._context.Update(autor);
            await this._context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id) 
        {
            var registrosBorrados = await this._context.Autores.Where(a => a.Id == id).ExecuteDeleteAsync();

            if (registrosBorrados == 0) 
            {
                return NotFound();
            }
            return Ok();
        }
    }
}
