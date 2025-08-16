using AutoMapper;
using LPS.Models;
using LPS.DTOs.Cliente;

namespace LPS.Profiles
{
    public class ClienteProfile : Profile
    {
        public ClienteProfile()
        {
            CreateMap<Cliente, ClienteReadDTO>();
            CreateMap<ClienteCreateDTO, Cliente>();
            CreateMap<ClienteUpdateDTO, Cliente>();
        }
    }
}
