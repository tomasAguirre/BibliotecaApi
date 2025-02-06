using AutoMapper;
using BibliotecaApi.Datos;
using BibliotecaApi.DTOs;
using BibliotecaApi.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaApi.Controllers
{
    [ApiController]
    [Route("api/libros")]
    public class LibrosController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public IMapper Mapper { get; }

        public LibrosController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            Mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<LibroDTO>> Get() 
        {
            var libros = await this.context.Libros.ToListAsync();
            var librosDTO = Mapper.Map<IEnumerable<LibroDTO>>(libros);
            return librosDTO;
        }

        [HttpGet("{id:int}", Name ="ObtenerLibro")]
        public async Task<ActionResult<LibroConAutorDTO>> Get(int id) 
        {
            var libro = await this.context.Libros
                                    .Include(x=> x.Autor)
                                    .FirstOrDefaultAsync(l => l.Id == id);
            if (libro is null)
            {
                return NotFound();
            }
            var libroDTO = this.Mapper.Map<LibroConAutorDTO>(libro);
            return libroDTO;
        }

        [HttpPost]
        public async Task<ActionResult> Post(LibroCreacionDTO libroCreacionDTO) 
        {
            var libro = Mapper.Map<Libro>(libroCreacionDTO);
            var exiteAutor = await this.context.Autores.AnyAsync(x=> x.Id == libro.AutorId);
            if (!exiteAutor) 
            {
                ModelState.AddModelError(nameof(libro.AutorId), $"El Autor {libro.AutorId}, no existe");
                return ValidationProblem();
                //return BadRequest($"el Autor de id {libro.AutorId} no existe");
            }
            this.context.Libros.Add(libro);
            await this.context.SaveChangesAsync();
            var libroDTO = this.Mapper.Map<LibroDTO>(libro);
            return CreatedAtRoute("ObtenerLibro", new {Id = libro.Id}, libroDTO);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, LibroCreacionDTO libroCreacionDTO) 
        {
            var libro = Mapper.Map<Libro>(libroCreacionDTO);
            libro.Id = id;
            //if (id != libro.Id) 
            //{
            //    return BadRequest("Los ids deben de coincidir");
            //}
            var exiteAutor = await this.context.Autores.AnyAsync(x => x.Id == libro.AutorId);
            if (!exiteAutor)
            {
                return BadRequest($"el Autor de id {libro.AutorId} no existe");
            }
            this.context.Update(libro);
            await this.context.SaveChangesAsync();

            return NoContent(); //actualizar por estandar se retorna esto un 204
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id) 
        {
            var registrosBorrados = await this.context.Libros.Where(l=> l.Id == id).ExecuteDeleteAsync();

            if (registrosBorrados == 0) 
            {
                return NotFound();
            }
            return NoContent(); //eliminar por estandar se retorna esto un 204
        }
    }
}
