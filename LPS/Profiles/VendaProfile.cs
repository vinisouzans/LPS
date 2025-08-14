using AutoMapper;
using LPS.DTOs.Venda;
using LPS.Models;

namespace LPS.Profiles
{
    public class VendaProfile : Profile
    {
        public VendaProfile()
        {
            CreateMap<Venda, VendaDTO>().ReverseMap();
        }
    }
}
