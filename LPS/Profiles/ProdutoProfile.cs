using AutoMapper;
using LPS.DTOs.Produto;
using LPS.Models;

namespace LPS.Profiles
{
    public class ProdutoProfile : Profile
    {
        public ProdutoProfile()
        {
            CreateMap<Produto, ProdutoDTO>().ReverseMap();
        }
    }
}
