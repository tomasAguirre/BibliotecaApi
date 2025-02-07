using System.ComponentModel.DataAnnotations;

namespace BibliotecaApi.Entidades
{
    //Relacion 1 a muchos, un libro tiene muchos comentarios 
    public class Comentario
    {
        public Guid Id { get; set; }
        [Required]
        public string Cuerpo { get; set; }
        public DateTime FechaPublicacion { get; set; }
        public int LibroId { get; set; } //id del libro al cual le corresponde el comentario 
        public Libro? Libro { get; set; }

    }
}
