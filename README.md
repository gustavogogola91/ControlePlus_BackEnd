# 📚 ControlePlus

## 🧾 Descrição
Sistema de gerenciamento de produtos voltado para pequenas empresas de qualquer segmento.
A aplicação permite o cadastro de produtos, fornecedores, estoques vinculados aos produtos, funcionários, setores e categorias.
Também é possível gerar pedidos e controlar movimentações de entrada e saída de produtos, mantendo um histórico de alterações realizadas por usuários.

Atualmente, o front-end oferece funcionalidades básicas para gerenciamento de produtos (criação, listagem, edição e exclusão).

---

## 👥 Integrantes da Dupla

* João Paulo Class - [https://github.com/JoaoPauloClass]
* Gustavo Luiz Gogola - [https://github.com/gustavogogola91]

---

## 🛠️ Tecnologias Utilizadas

* **Back-end:**

  * Linguagem: C# (.NET 8)
  * Framework: ASP.NET Core
  * ORM: Entity Framework Core
  * Banco de Dados: PostgreSQL
  * Segurança: JWT Token + BCrypt
  * Mapeamento de Objetos: AutoMapper

* **Front-end:**

  * Framework: Next.js (React)
  * Estilização: TailwindCSS

* **Versionamento:** Git + GitHub

---

## 🚀 Como Executar o Projeto

### ✅ Pré-requisitos

* [.NET SDK 8.0+](https://dotnet.microsoft.com/en-us/download)
* [Node.js 18+](https://nodejs.org/)
* [PostgreSQL](https://www.postgresql.org/download/)
* Git instalado

---

### 🖥️ Backend (.NET)

```bash
# 1. Clone o repositório
git clone https://github.com/usuario/repositorio

# 2. Acesse a pasta do projeto backend
cd ControlePlus/Backend

# 3. Restaure os pacotes
dotnet restore

# 4. Atualize o banco de dados 
dotnet ef database update

# 5. Rode a aplicação
dotnet run
```

---

### 🌐 Frontend (Next.js)

```bash
# 1. Acesse a pasta do frontend
cd ControlePlus/Frontend/controleplus-front

# 2. Instale as dependências
npm install

# 3. Rode a aplicação
npm run devc
```
---
