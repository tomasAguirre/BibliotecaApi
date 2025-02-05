using AutoMapper;
using BibliotecaApi.Datos;
using BibliotecaApi.DTOs;
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
        private readonly IMapper mapper;

        public ILogger<AutoresController> Logger { get; }

        public AutoresController(ApplicationDbContext context, ILogger<AutoresController> logger, IMapper mapper)
        {
            _context = context;
            Logger = logger;
            this.mapper = mapper;
        }

        [HttpGet("/listado-de-autores")] // no es necesario usar la ruta del controlador api/autores
        [HttpGet] //pueden existir 2 rutas para una accion 
        public async Task<IEnumerable<AutorDTO>> Get() 
        {
            Logger.LogTrace("Obteniendo el listado de autores : ");
            Logger.LogDebug("Obteniendo el listado de autores : ");
            Logger.LogInformation("Obteniendo el listado de autores : ");
            Logger.LogWarning("Obteniendo el listado de autores :");
            Logger.LogError("Obteniendo el listado de autores :");
            Logger.LogCritical("Obteniendo el listado de autores :");
            var autores = await this._context.Autores.ToListAsync();
            //var autoresDTO = autores.Select(autor =>
            //new AutorDTO { Id = autor.Id, NombreCompleto = $"{autor.Nombres} {autor.Apellidos}" });
            var autoresDTO = mapper.Map<IEnumerable<AutorDTO>>(autores);
            return autoresDTO;
        }

        [HttpGet("{id:int}", Name ="ObtenerAutor")] //api/autores/1?incluirLibros  (el fromquery es opcional ), tambien puedo usar [FromHeader]
        public async Task<ActionResult<AutorDTO>> Get(int id,[FromQuery] bool incluirLibros) 
        {
            var autor = await this._context.Autores
                                   .Include(a => a.Libros)
                                   .FirstOrDefaultAsync(x => x.Id == id);

            if (autor is null) {
                return NotFound();
            }

            var AutorDTO = mapper.Map<AutorDTO>(autor);

            return AutorDTO;
        }

        [HttpGet("{nombre:alpha}")] //api/autores/nombreAutor
        public async Task<ActionResult<Autor>> Get(string nombre)
        {
            var autor = await this._context.Autores
                                   .Include(a => a.Libros)
                                   .FirstOrDefaultAsync(x => x.Nombres.Contains(nombre));

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
        public async Task<ActionResult> Post(AutorCreacionDTO autorCreacionDTO) 
        {
            var autor = mapper.Map<Autor>(autorCreacionDTO);
            this._context.Add(autor);
            await this._context.SaveChangesAsync();
            var autorDTO = mapper.Map<Autor>(autor);
            return CreatedAtRoute("ObtenerAutor", new {Id=autor.Id }, autorDTO);
            //return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, AutorCreacionDTO autorCreacionDTO) 
        {
            var autor = mapper.Map<Autor>(autorCreacionDTO);
            autor.Id = id;
            //if (id != autor.Id) 
            //{
            //    return BadRequest("Los ids deben de coincidir");
            //}
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
