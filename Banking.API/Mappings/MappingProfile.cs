using AutoMapper;
using Banking.API.Models;
using Banking.API.DTOs;
namespace Banking.API.Mappings;

public class MappingProfile : Profile // Inherit from AutoMapper.Profile
{
    public MappingProfile()
    {
        // Cliente
        CreateMap<Cliente, ClienteDTO>().ReverseMap();

        // Conta
        CreateMap<Conta, ContaDTO>().ReverseMap();
    }
}
