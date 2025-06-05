import { useEffect, useState } from "react";
import { buscarEstoqueAlerta, buscarEstoqueVazio, buscarPedidos } from "./actions";

interface pedido  {
    id: number;
    status: string;
    dataPedido: string;
    valorTotal: number;
}

const Dashboard = () => {

    const [estoquesAlerta, setEstoquesAlerta] = useState([]);
    const [estoquesVazio, setEstoquesVazio] = useState([]);

    const [pedidos, setPedidos] = useState([]);

    const [loadingEstoque, setLoadingEstoque] = useState(true);
    const [loadingPedidos, setLoadingPedidos] = useState(true);

    useEffect(() => {
    
        async function carregarEstoques() {
          try {
            let estoqueAlerta: any = null;
            let estoqueVazio: any = null;
            estoqueAlerta = await buscarEstoqueAlerta();
            estoqueVazio = await buscarEstoqueVazio();
            
            setEstoquesAlerta(estoqueAlerta);
            setEstoquesVazio(estoqueVazio);
            setLoadingEstoque(false)
          } catch (err) {
            console.log(err instanceof Error ? err.message : "Erro desconhecido");
            setLoadingEstoque(false);
            setEstoquesAlerta([]);
            setEstoquesVazio([]);
          }
    
    
        }

        async function carregarPedidos(){
            try {
            let pedidos: any = null;
            pedidos = await buscarPedidos();
            setPedidos(pedidos)

            setLoadingPedidos(false);
          } catch (err) {
            console.log(err instanceof Error ? err.message : "Erro desconhecido");
            setLoadingPedidos(false);
            setPedidos([])
          }

        }

        carregarPedidos()
        carregarEstoques();
      }, []);


    return (
        <>
            <section className="grid gap-2 grid-cols-3">
                <div className="flex flex-col justify-center w-[350px] h-[170px] shadow border-gray rounded px-5">
                    <p className="font-semibold text-[16px]">Produtos em alerta</p>
                    <p className="font-semibold text-[40px] text-orange">{estoquesAlerta.length}</p>
                    {/* <p className="font-medium text-[16px] text-gray">Nenhum pedido feito</p> //TODO se der tempo implementar isso*/}
                </div>
                <div className="flex flex-col justify-center w-[350px] h-[170px] shadow border-gray rounded px-5">
                    <p className="font-semibold text-[16px]">Produtos em falta</p>
                    <p className="font-semibold text-[40px] text-red">{estoquesVazio.length}</p>
                    {/* <span className="text-[24px]">  &#x276f; 2</span> */}
                    {/* <p className="font-medium text-[16px] text-gray"><span className="text-blue">4 </span>produtos com pedidos criados</p> //TODO se der tempo implementar isso*/}
                </div>
                <div className="flex flex-col justify-evenly w-[350px] h-[170px] shadow border-gray rounded px-5">
                    <p className="font-semibold text-[16px]">Ações</p>
                    <button
                    className="bg-orange-alpha text-white font-medium rounded p-2 text-[14px] cursor-pointer transition hover:bg-orange"
                    //TODO implementar a logica de criar pedido com produtos em alerta
                    >Criar pedido com os itens em alerta</button>
                    <button
                    className="bg-red-alpha text-white font-medium rounded p-2 text-[14px] cursor-pointer transition hover:bg-red"
                    //TODO implementar a logica de criar pedido com produtos em falta
                    >Criar pedido com os itens em falta</button>
                </div>
                <div className="flex flex-col col-span-2 w-full h-[450px] shadow border-gray rounded p-5">
                    <p className="font-semibold text-[16px]">Pedidos realizados</p>
                </div>
                <div className="flex flex-col  w-[350px] h-[450px] shadow border-gray rounded p-5">
                    <p className="font-semibold text-[16px]">Pedidos</p>
                    <div className="font-semibold mt-2">

                            {pedidos.map((pedido: pedido) => (
                                <button
                                    key={pedido.id}
                                    className={`px-4 w-full text-justify cursor-pointer py-2 rounded-md mb-2 ${pedido.status.toLowerCase() === "concluido"
                                            ? "bg-green-100 text-green-700"
                                            : pedido.status.toLowerCase() === "pendente"
                                                ? "bg-orange-100 text-orange-500"
                                                : "bg-red-100 text-red-500"
                                        }`}
                                >
                                    {pedido.id} - {pedido.status} - Valor: R${pedido.valorTotal}
                                </button>
                            ))}

                    </div>
                </div>
            </section>
        </>
    )
}

export default Dashboard;