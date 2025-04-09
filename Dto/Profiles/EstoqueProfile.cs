using AutoMapper;
using ControlePlus_BackEnd.models;

namespace ControlePlus_BackEnd.Dto.Profiles
{
    public class EstoqueProfile : Profile
    {
        public EstoqueProfile()
        {
            CreateMap<Estoque, EstoqueDTO>()
            .ForMember(dest => dest.Produto, opt => opt.MapFrom(src => src.Produto));

            CreateMap<EstoquePostDTO, Estoque>();
        }
    }
}