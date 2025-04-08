using AutoMapper;
using ControlePlus_BackEnd.Dto;
using ControlePlus_BackEnd.models;

namespace ControlePlus_BackEnd.Profiles
{
    public class CategoriaProfile : Profile
    {
        public CategoriaProfile()
        {
            CreateMap<Categoria, CategoriaDTO>().ForMember(dest => dest.Produtos, opt => opt.MapFrom(src => src.Produtos.Select(p => p.Nome).ToList()));
        }
    }
}