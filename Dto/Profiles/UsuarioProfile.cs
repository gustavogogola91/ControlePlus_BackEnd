using AutoMapper;
using ControlePlus_BackEnd.models;

namespace ControlePlus_BackEnd.Dto.Profiles
{
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile()
        {
            CreateMap<Usuario, UsuarioDTO>();

            CreateMap<Usuario, UsuarioDetalhadoDTO>();

            CreateMap<UsuarioPostDTO, Usuario>()
            .ForMember(dest => dest.UsuarioId, opt => opt.Ignore());


            CreateMap<UsuarioUpdateDTO, Usuario>()
            .ForMember(dest => dest.Username, opt => opt.Condition(src => src.Username != null))
            .ForMember(dest => dest.Nome, opt => opt.Condition(src => src.Nome != null))
            .ForMember(dest => dest.Senha, opt => opt.Condition(src => src.Senha != null))
            .ForMember(dest => dest.SetorId, opt => opt.Condition(src => src.SetorId != null))
            .ForMember(dest => dest.Ativo, opt => opt.Condition(src => src.Ativo != null))
            .ForMember(dest => dest.UsuarioId, opt => opt.Condition(src => src.UsuarioId != null));
        }
    }
}