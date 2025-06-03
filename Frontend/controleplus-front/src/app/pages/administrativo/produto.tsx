"use client";

import { Search } from "lucide-react";
import { ReactElement, useEffect, useState } from "react";
import { buscarProdutos, buscarProdutosNome, useDebounce } from "./actions";
import { setMaxListeners } from "events";

const ApiUrl = "http://localhost:5290/produto";

export interface produto {
  cod: number;
  nome: string;
  precoCompra: number;
  precoVenda: number;
  setorNome: string;
  fornecedorNome: string;
  categoriaNome: string;
}

interface produtoPost {
  cod: number;
  nome: string;
  precoCompra: string;
  precoVenda: string;
  setorId: number;
  fornecedorId: number;
  categoriaId: number;
}

interface setor {
  id: number;
  nome: string;
}

interface fornecedor {
  id: number;
  nome: string;
}

interface categoria {
  id: number;
  nome: string;
}

export default function produto() {
  //  const showModal = () => {

  // }

  return (
    <>
      <section className="w-[1110px] m-auto">
        <TabelaProdutos/>
      </section>
    </>
  );
}
//COMPONENTES
function TabelaProdutos() {
  const [produtos, setProdutos] = useState([]);
  const [loading, setLoading] = useState(true);
  const [inputValue, setInputValue] = useState("");

  const termoDebounced = useDebounce(inputValue, 500);

  useEffect(() => {
    // if (termoDebounced.trim() === "") {
    //   setProdutos([]);
    //   return;
    // }

    async function carregarProdutos() {
      try {
        let dados: any = null;
        if(inputValue != "")
          {
            dados = await buscarProdutosNome(inputValue);
          }else{
            dados = await buscarProdutos();
          }
        setLoading(false);
        setProdutos(dados);
      } catch (err) {
        console.log(err instanceof Error ? err.message : "Erro desconhecido");
        setLoading(false);
        setProdutos([]);
      }


    }

    carregarProdutos();
  }, [termoDebounced]);

  function loadingAnimation() {
    return (
      <>
        <div className="h-8 mt-10 w-8 animate-spin rounded-full border-4 border-solid border-blue-500 border-t-transparent"></div>
        <span className="text-gray-600">Carregando...</span>
      </>
    );
  }

  return (
    <div className="flex flex-col justify-between items-center p-2">
      <div className="w-full flex justify-between">
        <h3 className="text-[16px] font-bold">Produtos</h3>
        <AdicionarProduto />
        {/* TODO implementar lógica de busca */}
        <div>
          <Search
            size={20}
            color="#1D1B20"
            className="bg-gray-200 absolute ml-3 mt-[9px]"
          />
          <input
            type="text"
            placeholder="Procurar Produto"
            value={inputValue}
            onChange={(e) => setInputValue(e.target.value)}
            className="rounded-xl bg-gray-200 text-gray-600 text-center px-5 py-2"
          />
        </div>
      </div>
      {loading == true ? loadingAnimation() : listarProdutos(produtos)}
    </div>
  );
}


//LÓGICA

function listarProdutos(produtos: produto[] | undefined | null) {
  if (produtos == undefined || produtos == null) {
    return <h1>Nenhum produto encontrado</h1>;
  }

  return (
    <table className=" w-full">
      <thead className="rounded-xl">
        <tr className="bg-black/80  text-white font-bold ">
          <th className="p-2">Id do Produto</th>
          <th>Nome</th>
          <th>Setor</th>
          <th>Preço Venda</th>
          <th>Preço</th>
          <th>Categoria</th>
          <th>Ações</th>
        </tr>
      </thead>
      <tbody className="text-center">
        {produtos.length === 0 ? (
          <tr>
            <td>Nenhum produto encontrado</td>
          </tr>
        ) : (
          produtos.map((produto: produto) => (
            <tr key={produto.cod} className="even:bg-[#E0E0E0] odd:bg-white ">
              <td className="p-2">{produto.cod}</td>
              <td>{produto.nome}</td>
              <td>{produto.setorNome}</td>
              <td>R${produto.precoVenda}</td>
              <td>R${produto.precoCompra}</td>
              <td>{produto.categoriaNome}</td>
              <td>
                <EditarProduto produtoOriginal={produto} />
                <ExcluirProduto
                  cod={produto.cod}
                  buscarProdutos={buscarProdutos}
                />
              </td>
            </tr>
          ))
        )}
      </tbody>
      {/* <th><button onClick={showModal()}>Editar</button></th> */}
    </table>
  );
}

function EditarProduto({ produtoOriginal }: { produtoOriginal: produto }) {
  const [showModal, setShowModal] = useState(false);
  const [produto, setProduto] = useState({
    cod: produtoOriginal.cod,
    nome: produtoOriginal.nome,
    precoCompra: produtoOriginal.precoCompra,
    precoVenda: produtoOriginal.precoVenda,
    setorNome: produtoOriginal.setorNome,
    fornecedorNome: produtoOriginal.fornecedorNome,
    categoriaNome: produtoOriginal.categoriaNome,
  });

  function putProduto(produto: produto) {
    try {
      fetch(`${ApiUrl}/${produtoOriginal.cod}`, {
        method: "PUT",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(produto),
      }).then(() => {
        setShowModal(false);
        setProduto({
          cod: 0,
          nome: "",
          precoCompra: 0,
          precoVenda: 0,
          setorNome: "",
          fornecedorNome: "",
          categoriaNome: "",
        });
        window.location.reload();
      });
      console.log("Produto atualizado com sucesso:", produto);
    } catch (error) {
      console.error("Erro ao atualizar produto:", error);
    }
  }

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    putProduto(produto);
  };

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target;
    setProduto({ ...produto, [name]: value });
  };
  return (
    <>
      <button
        className="bg-blue rounded-[10px] shadow px-3 py-1 m-1 font-bold cursor-pointer uppercase text-white text-center"
        title="Editar produto"
        onClick={() => setShowModal(true)}
      >
        Editar
      </button>
      {showModal && (
        <div
          className="fixed inset-0 flex items-center justify-center z-50"
          style={{ background: "rgba(0,0,0,0.3)" }}
        >
          <div className="bg-white p-8 rounded-lg flex flex-col gap-4 min-w-[350px]">
            <h2 className="text-xl font-bold text-blue mb-2">Editar Produto</h2>
            <form onSubmit={handleSubmit} className="flex flex-col gap-2">
              <input
                name="nome"
                placeholder="Nome"
                className="border border-blue rounded p-2"
                required
                maxLength={30}
                value={produto.nome}
                onChange={handleChange}
              />
              <input
                name="precoCompra"
                placeholder="Preço de Compra"
                type="number"
                className="border border-blue rounded p-2"
                required
                maxLength={20}
                value={produto.precoCompra}
                onChange={handleChange}
              />
              <input
                name="precoVenda"
                placeholder="Preço de Venda"
                type="number"
                className="border border-blue rounded p-2"
                required
                maxLength={20}
                value={produto.precoVenda}
                onChange={handleChange}
              />
              <input
                name="setorNome"
                placeholder="Setor"
                type="text"
                className="border border-blue rounded p-2"
                required
                maxLength={20}
                value={produto.setorNome}
                onChange={handleChange}
              />
              <input
                name="fornecedorNome"
                placeholder="Fornecedor"
                type="text"
                className="border border-blue rounded p-2"
                required
                maxLength={20}
                value={produto.fornecedorNome}
                onChange={handleChange}
              />
              <input
                name="categoriaNome"
                placeholder="Categoria"
                type="text"
                className="border border-blue rounded p-2"
                required
                maxLength={20}
                value={produto.categoriaNome}
                onChange={handleChange}
              />

              <div className="flex justify-around mt-2">
                <button
                  type="button"
                  className="bg-red-500 text-white px-4 py-2 rounded cursor-pointer"
                  onClick={() => setShowModal(false)}
                >
                  Cancelar
                </button>
                <button
                  type="submit"
                  className="bg-blue text-white px-4 py-2 rounded cursor-pointer"
                >
                  Salvar
                </button>
              </div>
            </form>
          </div>
        </div>
      )}
    </>
  );
}

function AdicionarProduto() {
  const [showModal, setShowModal] = useState(false);
  const [produto, setProduto] = useState<produtoPost>({
    cod: 0,
    nome: "",
    precoCompra: "R$ 0,00",
    precoVenda: "R$ 0,00",
    setorId: 0,
    fornecedorId: 0,
    categoriaId: 0,
  });

  const [setores, setSetor] = useState<setor[]>([]);
  const [fornecedores, setFornecedor] = useState<fornecedor[]>([]);
  const [categorias, setCategoria] = useState<categoria[]>([]);

  async function buscarSetor() {
    await fetch("http://localhost:5290/setor")
      .then((response) => response.json())
      .then((data) => setSetor(data))
      .catch((error) => console.error("erro ao buscar produtos", error));
  }

  async function buscarFornecedor() {
    await fetch("http://localhost:5290/fornecedor")
      .then((response) => response.json())
      .then((data) => setFornecedor(data))
      .catch((error) => console.error("erro ao buscar produtos", error));
  }

  async function buscarCategoria() {
    await fetch("http://localhost:5290/categoria")
      .then((response) => response.json())
      .then((data) => setCategoria(data))
      .catch((error) => console.error("erro ao buscar produtos", error));
  }

  useEffect(() => {
    buscarCategoria();
    buscarFornecedor();
    buscarSetor();
  }, []);

  function postProduto(produto: {
    cod: number;
    nome: string;
    descricao: string;
    precoCompra: number;
    precoVenda: number;
    setorId: number;
    fornecedorId: number;
    categoriaId: number;
  }) {
    try {
      fetch(`${ApiUrl}/`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(produto),
      }).then(() => {
        setShowModal(false);
        setProduto({
          cod: 0,
          nome: "",
          precoCompra: "R$ 0,00",
          precoVenda: "R$ 0,00",
          setorId: 0,
          fornecedorId: 0,
          categoriaId: 0,
        });
        console.log(produto);
      });
      console.log("Produto adicionado com sucesso:", produto);
    } catch (error) {
      console.error("Erro ao adicionar produto:", error);
    }
  }

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    try {
      const precoCompraNumerico =
        Number(produto.precoCompra.replace(/\D/g, "")) / 100;
      const precoVendaNumerico =
        Number(produto.precoVenda.replace(/\D/g, "")) / 100;

      postProduto({
        cod: produto.cod,
        nome: produto.nome,
        descricao: "a",
        precoCompra: precoCompraNumerico,
        precoVenda: precoVendaNumerico,
        setorId: produto.setorId,
        fornecedorId: produto.fornecedorId,
        categoriaId: produto.categoriaId,
      });
    } catch (error) {
      console.error(error);
    }
  };

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target;
    setProduto({ ...produto, [name]: value });
  };

  const handleSelectChange = (e: React.ChangeEvent<HTMLSelectElement>) => {
    const { name, value } = e.target;
    setProduto({ ...produto, [name]: value });
  };

  const formatBRL = (value: string) => {
    const cleanValue = value.replace(/\D/g, "");
    const numberValue = parseInt(cleanValue || "0", 10);

    return new Intl.NumberFormat("pt-BR", {
      style: "currency",
      currency: "BRL",
    }).format(numberValue / 100);
  };

  const handleDinheiroChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const input = e.target.value;
    const { name } = e.target;

    setProduto({ ...produto, [name]: formatBRL(input) });
  };

  return (
    <>
      <button
        className="bg-blue rounded-[10px] shadow px-3 py-1 m-1 font-bold cursor-pointer uppercase text-white text-center"
        title="Adicionar produto"
        onClick={() => setShowModal(true)}
      >
        Novo Produto
      </button>
      {showModal && (
        <div
          className="fixed inset-0 flex items-center justify-center z-50"
          style={{ background: "rgba(0,0,0,0.3)" }}
        >
          <div className="bg-white p-8 rounded-lg flex flex-col gap-4 min-w-[350px]">
            <h2 className="text-xl font-bold text-blue mb-2">Novo Produto</h2>
            <form onSubmit={handleSubmit} className="flex flex-col gap-2">
              <input
                name="cod"
                type="number"
                placeholder="Codigo"
                className="border border-blue rounded p-2"
                required
                value={produto.cod}
                onChange={handleChange}
              />
              <input
                name="nome"
                placeholder="Nome"
                className="border border-blue rounded p-2"
                required
                maxLength={30}
                value={produto.nome}
                onChange={handleChange}
              />
              <input
                name="precoCompra"
                placeholder="Preço de Compra"
                type="text"
                className="border border-blue rounded p-2"
                required
                maxLength={20}
                value={produto.precoCompra}
                onChange={handleDinheiroChange}
              />
              <input
                name="precoVenda"
                placeholder="Preço de Venda"
                type="text"
                className="border border-blue rounded p-2"
                required
                maxLength={20}
                value={produto.precoVenda}
                onChange={handleDinheiroChange}
              />
              <select
                name="setorId"
                className="border border-blue rounded p-2"
                onChange={handleSelectChange}
              >
                <option value="">Selecione um setor</option>
                {setores.map((setor) => {
                  return <option value={setor.id}>{setor.nome}</option>;
                })}
              </select>
              <select
                name="fornecedorId"
                className="border border-blue rounded p-2"
                onChange={handleSelectChange}
              >
                <option value="">Selecione um fornecedor</option>
                {fornecedores.map((fornecedor) => {
                  return (
                    <option value={fornecedor.id}>{fornecedor.nome}</option>
                  );
                })}
              </select>
              <select
                name="categoriaId"
                className="border border-blue rounded p-2"
                onChange={handleSelectChange}
              >
                <option value="">Selecione uma categoria</option>
                {categorias.map((categoria) => {
                  return <option value={categoria.id}>{categoria.nome}</option>;
                })}
              </select>

              <div className="flex justify-around mt-2">
                <button
                  type="button"
                  className="bg-red-500 text-white px-4 py-2 rounded cursor-pointer"
                  onClick={() => setShowModal(false)}
                >
                  Cancelar
                </button>
                <button
                  type="submit"
                  className="bg-blue text-white px-4 py-2 rounded cursor-pointer"
                >
                  Salvar
                </button>
              </div>
            </form>
          </div>
        </div>
      )}
    </>
  );
}

function ExcluirProduto({ cod, buscarProdutos }: { cod: number, buscarProdutos:any }) {
  const deletarProduto = (cod: number) => {
    fetch(`${ApiUrl}/${cod}`, {
            method: 'DELETE',
        })
        .then(() => {
            window.location.reload();
        })
        .catch((error) => console.error('Erro ao deletar paciente:', error));
  };
  return (
    <>
      <button
        className="ml-2 bg-transparent hover:bg-red-100 rounded p-1"
        title="Deletar Produto"
        onClick={() => deletarProduto(cod)}
      >
        <img src="/trashicon.png" alt="Trash icon" className="w-6 h-6" />
      </button>
    </>
  );
}
