﻿using BibliotecaApi.Entidades;

namespace BibliotecaApi.DTOs
{
    public class LibroConAutorDTO:LibroDTO
    {
        public int AutorId { get; set; }
        public required string AutorNombre { get; set; }
    }
}
