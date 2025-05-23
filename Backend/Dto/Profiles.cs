using AutoMapper;
using ControlePlus_BackEnd.Dto;
using ControlePlus_BackEnd.models;

namespace Backend.Dto
{
    public class Profiles : Profile
    {
        public Profiles()
        {
            CreateMap<Categoria, CategoriaDTO>().ForMember(dest => dest.Produtos, opt => opt.MapFrom(src => src.Produtos));
            CreateMap<CategoriaPostDTO, Categoria>().ForMember(dest => dest.Produtos, opt => opt.Ignore());

            CreateMap<Estoque, EstoqueDTO>()
            .ForMember(dest => dest.Produto, opt => opt.MapFrom(src => src.Produto));
            CreateMap<EstoquePostDTO, Estoque>();

            CreateMap<Fornecedor, FornecedorDTO>().ForMember(dest => dest.Produtos, opt => opt.MapFrom(src => src.Produtos));
            CreateMap<FornecedorPostDTO, Fornecedor>();
            CreateMap<FornecedorUpdateDTO, Fornecedor>()
                .ForMember(dest => dest.Nome, opt =>
                    opt.Condition(src => src.Nome != null))
                .ForMember(dest => dest.Contato, opt =>
                    opt.Condition(src => src.Contato != null))
                .ForMember(dest => dest.Endereco, opt =>
                    opt.Condition(src => src.Endereco != null));

            CreateMap<Movimentacao, MovimentacaoDTO>()
            .ForMember(dest => dest.Produto, opt => opt.MapFrom(src => src.Produto))
            .ForMember(dest => dest.DataCriacao, opt => opt.MapFrom(src => TimeZoneInfo.ConvertTimeFromUtc(src.DataCriacao,
                TimeZoneInfo.FindSystemTimeZoneById("America/Sao_Paulo")).ToString("dd/MM/yyyy HH:mm")));
            CreateMap<MovimentacaoPostDTO, Movimentacao>();

            CreateMap<Produto, ProdutoDTO>()
            .ForMember(dest => dest.FornecedorNome, opt => opt.MapFrom(src => src.Fornecedor != null ? src.Fornecedor.Nome : null))
            .ForMember(dest => dest.SetorNome, opt => opt.MapFrom(src => src.Setor != null ? src.Setor.Nome : null))
            .ForMember(dest => dest.CategoriaNome, opt => opt.MapFrom(src => src.Categoria != null ? src.Categoria.Nome : null));

            CreateMap<ProdutoPostDTO, Produto>();

            CreateMap<SetorPostDTO, Setor>()
            .ForMember(dest => dest.UsuarioId, opt => opt.Ignore())
            .ForMember(dest => dest.Responsavel, opt => opt.Ignore())
            .ForMember(dest => dest.Usuarios, opt => opt.Ignore())
            .ForMember(dest => dest.Produtos, opt => opt.Ignore());

            CreateMap<Setor, SetorDetalhadoDTO>()
            .ForMember(dest => dest.Usuarios, opt => opt.MapFrom(src => src.Usuarios))
            .ForMember(dest => dest.Produtos, opt => opt.MapFrom(src => src.Produtos));

            CreateMap<Setor, SetorDTO>();

            CreateMap<SetorUpdateDTO, Setor>()
                .ForMember(dest => dest.Responsavel, opt => opt.Ignore())
                .ForMember(dest => dest.Usuarios, opt => opt.Ignore())
                .ForMember(dest => dest.Produtos, opt => opt.Ignore());

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

            CreateMap<Usuario, JwtDTO>();

            CreateMap<Pedido, PedidoDTO>();
            CreateMap<PedidoPostDTO, Pedido>();

            CreateMap<ItemPedido, ItemPedido_PedidoDTO>();
            CreateMap<ItemPedidoPostDTO, ItemPedido>();
        }
    }
}