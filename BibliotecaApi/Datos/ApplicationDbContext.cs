using BibliotecaApi.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BibliotecaApi.Datos
{
    public class ApplicationDbContext : DbContext
    {
        //PM> Add-Migration TablaAutores
        // Update-Database

        // Add-Migration TablaLibros
        // Update-Database
        public ApplicationDbContext(DbContextOptions options) : base(options) 
        { }

        public DbSet<Autor> Autores { get; set; }
        public DbSet<Libro> Libros { get; set; }
    }
}
