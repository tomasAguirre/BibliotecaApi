﻿using AutoMapper;
using BibliotecaApi.DTOs;
using BibliotecaApi.Entidades;

namespace BibliotecaApi.Utilidades
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Autor, AutorDTO>()
                .ForMember(dto => dto.NombreCompleto, config => config.MapFrom(autor => $"{autor.Nombres} {autor.Apellidos}"));

            CreateMap<AutorCreacionDTO, Autor>();

            CreateMap<Libro, LibroDTO>();

            CreateMap<LibroCreacionDTO, Libro>();
        }
    }
}