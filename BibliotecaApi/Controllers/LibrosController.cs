using BibliotecaApi.Datos;
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

        public LibrosController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<Libro>> Get() 
        {
            return await this.context.Libros.ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Libro>> Get(int id) 
        {
            var libro = await this.context.Libros
                                    .Include(x=> x.Autor)
                                    .FirstOrDefaultAsync(l => l.Id == id);
            if (libro is null)
            {
                return NotFound();
            }
            return libro;
        }

        [HttpPost]
        public async Task<ActionResult> Post(Libro libro) 
        {
            var exiteAutor = await this.context.Autores.AnyAsync(x=> x.Id == libro.AutorId);
            if (!exiteAutor) 
            {
                return BadRequest($"el Autor de id {libro.AutorId} no existe");
            }
            this.context.Libros.Add(libro);
            await this.context.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, Libro libro) 
        {
            if (id != libro.Id) 
            {
                return BadRequest("Los ids deben de coincidir");
            }
            var exiteAutor = await this.context.Autores.AnyAsync(x => x.Id == libro.AutorId);
            if (!exiteAutor)
            {
                return BadRequest($"el Autor de id {libro.AutorId} no existe");
            }
            this.context.Update(libro);
            await this.context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id) 
        {
            var registrosBorrados = await this.context.Libros.Where(l=> l.Id == id).ExecuteDeleteAsync();

            if (registrosBorrados == 0) 
            {
                return NotFound();
            }
            return Ok();
        }
    }
}
