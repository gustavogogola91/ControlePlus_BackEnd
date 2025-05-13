"use client"

import { Component, FC, useEffect, useState } from "react"

interface SideNavigationsProps {
    activeComponent: string,
    setActiveComponent: (component: string) => void;
}

export const DashboardSideNavigation: FC<SideNavigationsProps> = ({ activeComponent, setActiveComponent }) => {

    return (
        <>
            <nav className="flex flex-col gap-10 m-10">
                <button
                    className={activeComponent == 'dashboard' ? "text-blue" : "text-red"}
                    onClick={() => setActiveComponent('dashboard')}
                >Dashboard</button>
                <button
                    className={activeComponent == 'relatorios' ? "text-blue" : "text-red"}
                    onClick={() => setActiveComponent('relatorios')}
                >Relatórios</button>
                <button
                    className={activeComponent == 'historico' ? "text-blue" : "text-red"}
                    onClick={() => setActiveComponent('historico')}
                >Histórico</button>
            </nav>
        </>
    )
}
