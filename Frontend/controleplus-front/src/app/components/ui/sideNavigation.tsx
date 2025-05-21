"use client"

import { Calendar, House, Clipboard } from "lucide-react";
import { Component, FC, useEffect, useState } from "react"

interface SideNavigationsProps {
    activeComponent: string,
    setActiveComponent: (component: string) => void;
}

export const DashboardSideNavigation: FC<SideNavigationsProps> = ({ activeComponent, setActiveComponent }) => (
    <>
        <nav className="flex flex-col w-full gap-10 m-10">
            <button
                className={`
                        flex gap-2 items-center px-2 pt-2 pb-1 font-semibold cursor-pointer
                        ${activeComponent == 'dashboard' ? "text-blue bg-dark-gray rounded-[10px]" : "text-black "}
                    `}

                onClick={() => setActiveComponent('dashboard')}
            > <House className="inline mb-1" /> Dashboard</button>
            <button
                className={`
                        flex gap-2 items-center px-2 pt-2 pb-1 font-semibold cursor-pointer
                        ${activeComponent == 'relatorios' ? "text-blue bg-dark-gray rounded-[10px]" : "text-black "}
                    `}

                onClick={() => setActiveComponent('relatorios')}
            ><Clipboard className="inline mb-1" /> Relatórios</button>
            <button
                className={`
                        flex gap-2 items-center px-2 pt-2 pb-1 font-semibold cursor-pointer
                        ${activeComponent == 'historico' ? "text-blue bg-dark-gray rounded-[10px]" : "text-black "}
                    `}

                onClick={() => setActiveComponent('historico')}
            ><Calendar className="inline mb-1" /> Histórico</button>
        </nav>
    </>
)
