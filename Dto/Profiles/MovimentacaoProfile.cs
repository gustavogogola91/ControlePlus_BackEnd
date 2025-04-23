using AutoMapper;
using ControlePlus_BackEnd.models;

namespace ControlePlus_BackEnd.Dto.Profiles
{
    public class MovimentacaoProfile : Profile
    {
        public MovimentacaoProfile()
        {
            CreateMap<Movimentacao, MovimentacaoDTO>()
            .ForMember(dest => dest.Produto, opt => opt.MapFrom(src => src.Produto))
            .ForMember(dest => dest.DataCriacao, opt => opt.MapFrom(src => TimeZoneInfo.ConvertTimeFromUtc(src.DataCriacao,
                TimeZoneInfo.FindSystemTimeZoneById("America/Sao_Paulo")).ToString("dd/MM/yyyy HH:mm")));

            CreateMap<MovimentacaoPostDTO, Movimentacao>();
        }
    }
}