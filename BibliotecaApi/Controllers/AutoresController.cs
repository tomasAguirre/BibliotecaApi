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
        public AutoresController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IEnumerable<Autor>> Get() 
        {
            return await this._context.Autores.ToListAsync();
        }

        [HttpGet("{id:int}")] //api/autores/1
        public async Task<ActionResult<Autor>> Get(int id) 
        {
            var autor = await this._context.Autores
                                   .Include(a => a.Libros)
                                   .FirstOrDefaultAsync(x => x.Id == id);

            if (autor is null) {
                return NotFound();
            }

            return autor;
        }

        [HttpPost]
        public async Task<ActionResult> Post(Autor autor) 
        {
            this._context.Add(autor);
            await this._context.SaveChangesAsync();
            return Ok();
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
