"use client";

import { Search, Trash2 } from "lucide-react";
import { ReactElement, useEffect, useState } from "react";
import {  buscarEstoqueIdProduto, buscarEstoques, buscarProdutos, buscarProdutosNome, useDebounce } from "./actions";
import { setMaxListeners } from "events";

const ApiUrl = "http://localhost:5290/estoque";

interface produto {
  cod: number;
  nome: string;
  precoCompra: number;
  precoVenda: number;
  setorNome: string;
  fornecedorNome: string;
  categoriaNome: string;
}

export interface estoque {
  id: number;
  produto: produto;
  quantidade: number;
  quantidadeAlerta: number;
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

export default function estoque() {

  return (
    <>
      <section className="w-[1110px] m-auto">
        <TabelaEstoque/>
      </section>
    </>
  );
}
//COMPONENTES
function TabelaEstoque() {
  const [estoques, setEstoques] = useState([]);
  const [loading, setLoading] = useState(true);
  const [inputValue, setInputValue] = useState("");

  const termoDebounced = useDebounce(inputValue, 500);//500 ms de delay para fazer a requisição

  useEffect(() => {

    async function carregarEstoque() {
      try {
        let dados: any = null;
        if(inputValue != "")
          {
            dados = await buscarEstoqueIdProduto(parseInt(inputValue));
          }else{
            dados = await buscarEstoques();
          }
        setLoading(false);
        setEstoques(dados);
      } catch (err) {
        console.log(err instanceof Error ? err.message : "Erro desconhecido");
        setLoading(false);
        setEstoques([]);
      }


    }

    carregarEstoque();
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
      <div className="w-full flex justify-between items-center p-2">
        <h3 className="text-[16px] font-bold">Estoque</h3>
        {/* <AdicionarEstoque /> */}
        <div>
          <Search
            size={20}
            color="#1D1B20"
            className="bg-gray-200 absolute ml-3 mt-[9px]"
          />
          <input
            type="text"
            placeholder="Procurar Estoque"
            value={inputValue}
            onChange={(e) => setInputValue(e.target.value)}
            className="rounded-xl bg-gray-200 text-gray-600 text-center px-5 py-2"
          />
        </div>
      </div>
      {loading == true ? loadingAnimation() : listarEstoques(estoques)}
    </div>
  );
}


//LÓGICA

function listarEstoques(estoques: estoque[] | undefined | null) {
  if (estoques == undefined || estoques == null) {
    return <h1>Nenhum Estoque foi cadastrado</h1>;
  }

  return (
    <table className=" w-full">
      <thead className="rounded-xl">
        <tr className="bg-black/80  text-white font-bold ">
          <th className="p-2">Id do Estoque</th>
          <th className="p-2">Id do produto</th>
          <th>Nome</th>
          <th>Quantidade</th>
          <th>Ações</th>
        </tr>
      </thead>
      <tbody className="text-center">
        {estoques.length === 0 ? (
          <tr>
            <td>Nenhum produto encontrado</td>
          </tr>
        ) : (
          estoques.map((estoque: estoque) => (
            <tr key={estoque.id} className="even:bg-[#E0E0E0] odd:bg-white ">
              <td className="p-2">{estoque.id}</td>
              <td>{estoque.produto.cod}</td>
              <td>{estoque.produto.nome}</td>
              <td className={estoque.quantidade <= estoque.quantidadeAlerta ? "text-red-400" : "text-black"}>{estoque.quantidade}</td>
              <td className="flex justify-center items-center">
                <EditarEstoque estoqueOriginal={estoque} />
              </td>
            </tr>
          ))
        )}
      </tbody>
    </table>
  );
}

function EditarEstoque({ estoqueOriginal }: { estoqueOriginal: estoque }) {
  const [showModal, setShowModal] = useState(false);
  
  const [estoque, setEstoque] = useState({
    id: estoqueOriginal.id,
    produto: estoqueOriginal.produto,
    quantidade: estoqueOriginal.quantidade,
    quantidadeAlerta: estoqueOriginal.quantidadeAlerta
  });

  function putEstoque(estoque: estoque) {
    try {
      fetch(`${ApiUrl}/${estoque.id}`, {
        method: "PUT",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(estoque),
      }).then(() => {
        setShowModal(false);
        setEstoque({
          id: 0,
          produto: {
            cod: 0,
            nome: "",
            precoCompra: 0,
            precoVenda: 0,
            setorNome: "",
            fornecedorNome: "",
            categoriaNome: "",
          },
          quantidade: 0,
          quantidadeAlerta: 0
        });
        window.location.reload();
      });
      console.log("Estoque atualizado com sucesso:", estoque);
    } catch (error) {
      console.error("Erro ao atualizar produto:", error);
    }
  }

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    putEstoque(estoque);
  };

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target;
    setEstoque({ ...estoque, [name]: value });
  };
  return (
    <>
      <button
        className="bg-blue rounded-[10px] shadow px-3 py-1 m-1 font-bold cursor-pointer uppercase text-white text-center"
        title="Editar estoque"
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
            <h2 className="text-xl font-bold text-blue mb-2">Editar Estoque</h2>
            <form onSubmit={handleSubmit} className="flex flex-col gap-2">
              <label htmlFor="quantidade">Quantidade</label>
              <input
                name="quantidade"
                placeholder="Quantidade"
                className="border border-blue rounded p-2"
                maxLength={30}
                value={estoque.quantidade}
                onChange={handleChange}
              />
              <label htmlFor="quantidadeAlerta">Quantidade Alerta</label>
              <input
                name="quantidadeAlerta"
                placeholder="Quantidade Alerta"
                className="border border-blue rounded p-2"
                maxLength={30}
                value={estoque.quantidadeAlerta}
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

