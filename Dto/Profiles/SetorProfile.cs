

using AutoMapper;
using ControlePlus_BackEnd.models;

namespace ControlePlus_BackEnd.Dto.Profiles
{
    public class SetorProfile : Profile
    {
        public SetorProfile()
        {
            // CreateMap<Setor, SetorDTO>()
            //     .ForMember(dest => dest.IdsUsuarios, opt => opt.MapFrom(src =>
            //         src.Usuarios != null ? src.Usuarios.Select(u => u.Nome).ToList() : new List<string>() // se usuarios nao for nulo ele busca o nome, se for joga uma lista vazia
            //     ));
            CreateMap<SetorPostDTO, Setor>()
                .ForMember(dest => dest.UsuarioId, opt => opt.Ignore())
                .ForMember(dest => dest.Responsavel, opt => opt.Ignore())
                .ForMember(dest => dest.Usuarios, opt => opt.Ignore())
                .ForMember(dest => dest.Produtos, opt => opt.Ignore());

            CreateMap<Setor, SetorDetalhadoDTO>()
            .ForMember(dest => dest.Usuarios, opt => opt.MapFrom(src => src.Usuarios));

            CreateMap<Setor, SetorDTO>();

            CreateMap<SetorUpdateDTO, Setor>()
                .ForMember(dest => dest.Responsavel, opt => opt.Ignore())
                .ForMember(dest => dest.Usuarios, opt => opt.Ignore())
                .ForMember(dest => dest.Produtos, opt => opt.Ignore());

        }
    }
}