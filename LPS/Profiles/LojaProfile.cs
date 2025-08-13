using AutoMapper;
using LPS.DTOs.Loja;
using LPS.Models;

namespace LPS.Profiles
{
    public class LojaProfile : Profile
    {
        public LojaProfile()
        {
            CreateMap<Loja, LojaReadDTO>();
            CreateMap<LojaCreateDTO, Loja>();
            CreateMap<LojaUpdateDTO, Loja>();
        }
    }
}
