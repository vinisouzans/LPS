using AutoMapper;
using LPS.DTOs.Estoque;
using LPS.Models;

namespace LPS.Profiles
{
    public class EstoqueProfile : Profile
    {
        public EstoqueProfile()
        {
            CreateMap<Estoque, EstoqueDTO>().ReverseMap();
        }
    }
}
