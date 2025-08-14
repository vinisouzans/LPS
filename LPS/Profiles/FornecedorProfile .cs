using AutoMapper;
using LPS.DTOs.Fornecedor;
using LPS.Models;

namespace LPS.Profiles
{
    public class FornecedorProfile : Profile
    {
        public FornecedorProfile()
        {
            CreateMap<Fornecedor, FornecedorDTO>().ReverseMap();
        }
    }
}
