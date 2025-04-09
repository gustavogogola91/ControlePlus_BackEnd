using AutoMapper;
using ControlePlus_BackEnd.Dto;
using ControlePlus_BackEnd.models;

namespace ControlePlus_BackEnd.Dto
{
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile()
        {
            CreateMap<Usuario, UsuarioDTO>(MemberList.Destination);
        }
    }
}