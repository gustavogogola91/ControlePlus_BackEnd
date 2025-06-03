import { useEffect, useState } from "react";
import { produto } from "./produto";

const ApiUrl = "http://localhost:5290/produto";

export async function buscarProdutos() {
  const response = await fetch(`${ApiUrl}`);
  if (!response.ok) throw new Error("Erro ao carregar produtos");
  return response.json();
}

export async function buscarProdutosNome(nome: string) {
  const response = await fetch(`${ApiUrl}/buscar/${nome}`);
  if (!response.ok) throw new Error("Erro ao buscar produto por nome");
  return response.json();
}

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
