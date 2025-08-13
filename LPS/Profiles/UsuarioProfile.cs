using AutoMapper;
using LPS.DTOs.Usuario;
using LPS.Models;

namespace LPS.Profiles
{
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile()
        {
            // Usuario -> UsuarioReadDTO
            CreateMap<Usuario, UsuarioReadDTO>()
                .ForMember(dest => dest.NomeLoja, opt => opt.MapFrom(src => src.Loja.Nome));

            // UsuarioCreateDTO -> Usuario
            CreateMap<UsuarioCreateDTO, Usuario>();

            // UsuarioUpdateDTO -> Usuario
            CreateMap<UsuarioUpdateDTO, Usuario>();
        }
    }
}
