using AutoMapper;
using ControlePlus_BackEnd.models;

namespace ControlePlus_BackEnd.Dto.Profiles
{
    public class FornecedorProfile : Profile
    {
        public FornecedorProfile()
        {
            CreateMap<Fornecedor, FornecedorDTO>().ForMember(dest => dest.Produtos, opt => opt.MapFrom(src => src.Produtos));

            CreateMap<FornecedorPostDTO, Fornecedor>();

            CreateMap<FornecedorUpdateDTO, Fornecedor>()
                .ForMember(dest => dest.Nome, opt =>
                    opt.Condition(src => src.Nome != null))
                .ForMember(dest => dest.Contato, opt =>
                    opt.Condition(src => src.Contato != null))
                .ForMember(dest => dest.Endereco, opt =>
                    opt.Condition(src => src.Endereco != null));

        }
    }
}