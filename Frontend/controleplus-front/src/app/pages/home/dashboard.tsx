import { useEffect, useState } from "react";
import { buscarEstoqueAlerta, buscarEstoqueVazio } from "./actions";


const dashboard = () => {

    const [estoquesAlerta, setEstoquesAlerta] = useState([]);
    const [estoquesVazio, setEstoquesVazio] = useState([]);

    const [loading, setLoading] = useState(true);

    useEffect(() => {
    
        async function carregarEstoques() {
          try {
            let estoqueAlerta: any = null;
            let estoqueVazio: any = null;
            estoqueAlerta = await buscarEstoqueAlerta();
            estoqueVazio = await buscarEstoqueVazio();
            
            setEstoquesAlerta(estoqueAlerta);
            setEstoquesVazio(estoqueVazio);
            setLoading(false);
          } catch (err) {
            console.log(err instanceof Error ? err.message : "Erro desconhecido");
            setLoading(false);
            setEstoquesAlerta([]);
            setEstoquesVazio([]);
          }
    
    
        }
    
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
                </div>
            </section>
        </>
    )
}

export default dashboard;