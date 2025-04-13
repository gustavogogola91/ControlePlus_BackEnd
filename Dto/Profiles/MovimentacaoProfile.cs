using AutoMapper;
using ControlePlus_BackEnd.models;

namespace ControlePlus_BackEnd.Dto.Profiles
{
    public class MovimentacaoProfile : Profile
    {
        public MovimentacaoProfile()
        {
            CreateMap<Movimentacao, MovimentacaoDTO>()
            .ForMember(dest => dest.Produto, opt => opt.MapFrom(src => src.Produto));

            CreateMap<MovimentacaoPostDTO, Movimentacao>();
        }
    }
}