using AutoMapper;
using LPS.DTOs.Estoque;
using LPS.DTOs.Loja;
using LPS.DTOs.Produto;
using LPS.DTOs.Venda;
using LPS.Models;

namespace LPS.Profiles
{
    public class ProdutoProfile : Profile
    {
        public ProdutoProfile()
        {
            CreateMap<Produto, ProdutoReadDTO>()
                .ForMember(dest => dest.QuantidadeTotal, opt => opt.MapFrom(src => src.Estoques.Sum(e => e.QuantidadeTotal)))
                .ForMember(dest => dest.QuantidadeDisponivel, opt => opt.MapFrom(src => src.Estoques.Sum(e => e.QuantidadeDisponivel)));
            CreateMap<ProdutoCreateDTO, Produto>();
            CreateMap<ProdutoUpdateDTO, Produto>();

            CreateMap<EstoqueCreateDTO, Estoque>();
            CreateMap<VendaCreateDTO, Venda>();
        }
    }
}
