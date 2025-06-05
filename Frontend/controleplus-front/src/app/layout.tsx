//TODO: trocar icone do site

import type { Metadata } from "next";
import { Inter } from 'next/font/google'
import "./globals.css";

import { Header } from "./components/Header";

const inter = Inter({ subsets: ['latin'] })


export const metadata: Metadata = {
  title: "ControlePlus",
  description: "Gerencimante de estoques para pequenas empresas",
};

export default function RootLayout({
  children,
}: Readonly<{
  children: React.ReactNode;
}>) {
  return (
    <html lang="pt-br">
      <body
        className={`${inter.className} antialiased`}
      >
        <Header />
        <main>
          {children}
        </main>
      </body>
    </html>
  );
}
