using AutoMapper;
using BibliotecaApi.Datos;
using BibliotecaApi.DTOs;
using BibliotecaApi.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaApi.Controllers
{
    [ApiController]
    [Route("api/libros/{libroId:int}/comentarios")] //
    public class ComentariosController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public ComentariosController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<ComentarioDTO>>> Get(int LibroId)
        {
            var existeLibros = await this.context.Libros.AnyAsync(l => l.Id == LibroId);
            if (existeLibros)
            {
                return NotFound();
            }
            var comentarios = await this.context.Comentarios.Where(c => c.LibroId == LibroId)
                                            .OrderByDescending(c => c.FechaPublicacion)
                                            .ToListAsync();
            return this.mapper.Map<List<ComentarioDTO>>(comentarios);
        }

        [HttpGet("{id}", Name = "ObtenerComentario")]
        public async Task<ActionResult<ComentarioDTO>> Get(Guid id)
        {
            var comentario = await this.context.Comentarios.FirstOrDefaultAsync(c => c.Id == id);

            if (comentario is null)
            {
                return NotFound();
            }
            return mapper.Map<ComentarioDTO>(comentario);
        }

        [HttpPost]
        public async Task<ActionResult> Post(int libroId, ComentarioCreacionDTO comentarioCreacionDTO) 
        {
            var existeLibros = await this.context.Libros.AnyAsync(l => l.Id == libroId);
            if (existeLibros)
            {
                return NotFound();
            }
            var comentario = this.mapper.Map<Comentario>(comentarioCreacionDTO);
            comentario.LibroId = libroId;
            comentario.FechaPublicacion = DateTime.UtcNow;
            context.Add(comentario);
            await this.context.SaveChangesAsync();
            var comentarioDTO = mapper.Map<ComentarioDTO>(comentario);
            return CreatedAtRoute("ObtenerComentario", new { id = comentario.Id, libroId}, comentario);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id, int libroId) 
        {
            var existeLibros = await this.context.Libros.AnyAsync(l => l.Id == libroId);
            if (existeLibros)
            {
                return NotFound();
            }
            var registrosBorrados = await this.context.Comentarios.Where(x => x.Id == id).ExecuteDeleteAsync();
            if (registrosBorrados == 0) 
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}