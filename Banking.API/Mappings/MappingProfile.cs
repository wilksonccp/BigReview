using AutoMapper;
using Banking.API.DTOs;
using Banking.API.Models.entities;
using Banking.API.Models.Entities;

namespace Banking.API.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Cliente
            CreateMap<Cliente, ClienteDTO>().ReverseMap();

            // Conta
            CreateMap<Conta, ContaResponseDTO>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
                .ReverseMap();

            // Movimento
            CreateMap<Movimento, MovimentoDTO>()
                .ForMember(dest => dest.Tipo, opt => opt.MapFrom(src => src.Tipo.ToString()));
        }
    }
}
