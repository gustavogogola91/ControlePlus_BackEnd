import { useEffect, useState } from "react";
import { estoque } from "./Estoque";

const ApiUrlProdutos = "http://localhost:5290/produto";
const ApiUrlEstoques = "http://localhost:5290/estoque";

//APIs PRODUTO

export async function buscarProdutos() {
  const response = await fetch(`${ApiUrlProdutos}`);
  if (!response.ok) throw new Error("Erro ao carregar produtos");
  return response.json();
}

export async function buscarProdutosNome(nome: string) {
  const response = await fetch(`${ApiUrlProdutos}/buscar/${nome}`);
  if (!response.ok) throw new Error("Erro ao buscar produto por nome");
  return response.json();
}

//APIs ESTOQUE

export async function buscarEstoques() {
  const response = await fetch(`${ApiUrlEstoques}`);
  if (!response.ok) throw new Error("Erro ao carregar estoques");
  return response.json();
}

export async function buscarEstoqueIdProduto(id: number) {
  const response = await fetch(`${ApiUrlEstoques}/produto/${id}`);
  if (!response.ok) throw new Error("Erro ao buscar estoque por id produto");
  return response.json();
}

//CUSTOM HOOK

export function useDebounce<T>(value: T, delay: number): T { //XXX pra que o T?
  const [debouncedValue, setDebouncedValue] = useState(value);

  useEffect(() => {
    const timer = setTimeout(() => {
      setDebouncedValue(value);
    }, delay);

    return () => clearTimeout(timer);
  }, [value, delay]);

  return debouncedValue;
}

