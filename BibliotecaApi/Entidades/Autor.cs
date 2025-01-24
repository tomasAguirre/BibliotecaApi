using System.ComponentModel.DataAnnotations;

namespace BibliotecaApi.Entidades
{
    // a un autor le corresponden muchos libros 
    public class Autor
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
        [Required]
        public required string  Nombre { get; set; }

        public List<Libro> Libros { get; set; } = new List<Libro>();
    }
}
