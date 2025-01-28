using BibliotecaApi.Validaciones;
using System.ComponentModel.DataAnnotations;

namespace BibliotecaApi.Entidades
{
    // a un autor le corresponden muchos libros 
    public class Autor : IValidatableObject
    {

        public Autor()
        {
        }
        public Autor(int id, string Nombre)
        {
            Id = id;
            this.Nombre = Nombre;
        }

        public int Id { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")] //{0} es un place holder que sustituye el valor con el nombre del campo
        [StringLength(20, ErrorMessage = "El campo {0} debe tener 20 caracteres o menos")]
        //[PrimeraLetraMayuscula]
        public required string  Nombre { get; set; }

        //[Range(18, 120, ErrorMessage ="La edad debe de esta entre 18 y 120")]
        //public int Edad { get; set; }
        //[CreditCard]
        //public String Tarjeta { get; set; }
        //[Url]
        //public String Url { get; set; }

        public List<Libro> Libros { get; set; } = new List<Libro>();

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!String.IsNullOrEmpty(Nombre)) 
            {
                var PrimeraLetra = Nombre[0].ToString();

                if(PrimeraLetra != PrimeraLetra.ToUpper()) 
                {
                    yield return new ValidationResult("La primera letra debe de ser mayuscula - por modelo", 
                        new string[] {nameof(Nombre)});
                }
            }
        }
    }
}
