using AutoMapper;
using ControlePlus_BackEnd.models;

namespace ControlePlus_BackEnd.Dto.Profiles
{
    public class FornecedorProfile : Profile
    {
        public FornecedorProfile()
        {
            CreateMap<Fornecedor, FornecedorDTO>().ForMember(dest => dest.Produtos, opt => opt.MapFrom(src => src.Produtos));

        }
    }
}