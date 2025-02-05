﻿namespace BibliotecaApi.DTOs
{
    public class AutorDTO
    {
        public int Id { get; set; }
        public required string NombreCompleto { get; set; }

        public List<LibroDTO> Libros { get; set; } = [];
    }
}
