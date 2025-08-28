using AutoMapper;
using LPS.DTOs.Venda;
using LPS.Models;

namespace LPS.Profiles
{
    public class VendaProfile : Profile
    {
        public VendaProfile()
        {
            CreateMap<Venda, VendaResponseDTO>()
                .ForMember(dest => dest.ClienteNome, opt => opt.MapFrom(src => src.Cliente != null ? src.Cliente.Nome : null))
                .ForMember(dest => dest.ClienteCPF, opt => opt.MapFrom(src => src.Cliente != null ? src.Cliente.CPF : null));
        }
    }
}
