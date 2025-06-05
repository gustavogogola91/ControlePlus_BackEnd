const ApiUrlPedido = "http://localhost:5290/pedido";
const ApiUrlEstoque = "http://localhost:5290/estoque";



//APIs ESTOQUE

export async function buscarEstoqueAlerta() {
    const response = await fetch(`${ApiUrlEstoque}/alerta`);
    if (!response.ok) throw new Error("Erro ao carregar produtos");
    return response.json();
}

export async function buscarEstoqueVazio() {
    const response = await fetch(`${ApiUrlEstoque}/vazio`);
    if (!response.ok) throw new Error("Erro ao carregar produtos");
    return response.json();
}


//APIs PEDIDO
export async function buscarPedidos() {
    const response = await fetch(`${ApiUrlPedido}`);
    if (!response.ok) throw new Error("Erro ao carregar pedidos");
    return response.json();
}