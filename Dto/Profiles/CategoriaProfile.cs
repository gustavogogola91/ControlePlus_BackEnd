using AutoMapper;
using ControlePlus_BackEnd.models;

namespace ControlePlus_BackEnd.Dto.Profiles
{
    public class CategoriaProfile : Profile
    {
        public CategoriaProfile()
        {
            CreateMap<Categoria, CategoriaDTO>().ForMember(dest => dest.Produtos, opt => opt.MapFrom(src => src.Produtos));
            CreateMap<CategoriaPostDTO, Categoria>().ForMember(dest => dest.Produtos, opt => opt.Ignore());
        }
    }
}