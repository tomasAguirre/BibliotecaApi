using System.ComponentModel.DataAnnotations;

namespace BibliotecaApi.DTOs
{
    public class LibroCreacionDTO
    {
        [Required]
        [StringLength(150, ErrorMessage = "El titulo de {0} tiene que tener 150 caracteres")]
        public required string Titulo { get; set; }
        public int AutorId { get; set; }
    }
}
