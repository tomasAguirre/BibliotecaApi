using System.ComponentModel.DataAnnotations;

namespace BibliotecaApi.DTOs
{
    public class AutorCreacionDTO
    {
        [Required(ErrorMessage = "El campo {0} es requerido")] //{0} es un place holder que sustituye el valor con el nombre del campo
        [StringLength(150, ErrorMessage = "El campo {0} debe tener 20 caracteres o menos")]
        //[PrimeraLetraMayuscula]
        public required string Nombres { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")] //{0} es un place holder que sustituye el valor con el nombre del campo
        [StringLength(150, ErrorMessage = "El campo {0} debe tener 20 caracteres o menos")]
        //[PrimeraLetraMayuscula]
        public required string Apellidos { get; set; }

        [StringLength(30, ErrorMessage = "El campo {0} debe tener 20 caracteres o menos")]
        public string? Identificacion { get; set; }
    }
}
