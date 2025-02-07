using System.ComponentModel.DataAnnotations;

namespace BibliotecaApi.DTOs
{
    public class ComentarioCreacionDTO
    {
        [Required]
        public string Cuerpo { get; set; }
    }
}
