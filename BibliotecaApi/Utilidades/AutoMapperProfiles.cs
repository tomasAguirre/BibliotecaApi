using AutoMapper;
using BibliotecaApi.DTOs;
using BibliotecaApi.Entidades;

namespace BibliotecaApi.Utilidades
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Autor, AutorDTO>()
                .ForMember(dto => dto.NombreCompleto, config => config.MapFrom(autor => mapearNombreApellidoAutor(autor)));

            CreateMap<Autor, AutorConLibrosDTO>()
    .ForMember(dto => dto.NombreCompleto, config => config.MapFrom(autor => mapearNombreApellidoAutor(autor)));

            CreateMap<AutorCreacionDTO, Autor>();

            CreateMap<Autor, AutorPatchDTO>().ReverseMap();

            CreateMap<Libro, LibroDTO>();

            CreateMap<LibroCreacionDTO, Libro>();

            CreateMap<Libro, LibroConAutorDTO>().ForMember(dto => dto.AutorNombre, config =>
            config.MapFrom(ent => mapearNombreApellidoAutor(ent.Autor!)));

            CreateMap<ComentarioCreacionDTO, Comentario>();
            CreateMap<Comentario, ComentarioDTO>();
            CreateMap<ComentarioPatchDTO, Comentario>().ReverseMap();

        }

        private string mapearNombreApellidoAutor(Autor autor) => $"{autor.Nombres} {autor.Apellidos}";
    }
}