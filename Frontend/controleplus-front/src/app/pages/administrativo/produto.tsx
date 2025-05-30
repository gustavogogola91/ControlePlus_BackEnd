"use client";

import { Search } from "lucide-react";
import { useEffect, useState } from "react";

const ApiUrl = "http://localhost:5290/produto";

interface produto {
  cod: number;
  nome: string;
  precoCompra: number;
  precoVenda: number;
  setorNome: string;
  fornecedorNome: string;
  categoriaNome: string;
}

export default function produto() {
  //  const showModal = () => {

  // }

  return (
    <>
      <section className="w-[1110px]">
        <div className="flex justify-between items-center p-2">
          <h3 className="text-[16px] font-bold">Produtos</h3>
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
              className="rounded-xl bg-gray-200 text-gray-600 text-center px-5 py-2"
            />
          </div>
        </div>
        {listarProdutos()}
      </section>
    </>
  );
}

//LÓGICA

function listarProdutos() {
  const [produtos, setProdutos] = useState([]);

  async function buscarProdutos() {
    await fetch(`${ApiUrl}`)
      .then((response) => response.json())
      .then((data) => setProdutos(data))
      .catch((error) => console.error("erro ao buscar produtos", error));
  }

  useEffect(() => {
    buscarProdutos();
  }, []);

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
              <td>{produto.precoVenda}</td>
              <td>{produto.precoCompra}</td>
              <td>{produto.categoriaNome}</td>
              <td>
                <EditarProduto produtoOriginal={produto} />
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
      console.log("Paciente atualizado com sucesso:", produto);
    } catch (error) {
      console.error("Erro ao atualizar paciente:", error);
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
