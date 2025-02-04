using System.ComponentModel.DataAnnotations;
using System.Security.Principal;

namespace BibliotecaApi.Entidades
{
    //a un libro le corresponde 1 autor
    // a un autor le corresponden muchos libros 
    public class Libro
    {
        public int Id { get; set; }
        [Required]
        [StringLength(150, ErrorMessage ="El titulo de {0} tiene que tener 150 caracteres")]
        public required string Titulo { get; set; }
        public int AutorId { get; set; }
        public Autor? Autor { get; set; } 
    }
}
