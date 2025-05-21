"use client"

import { DashboardSideNavigation } from "@/app/components/ui/sideNavigation";
import { useState } from "react";

import dashboard from "./dashboard";
import relatorios from "./relatorios";
import historico from "./historico";


export default function home() {
    const [activeComponent, setActiveComponent] = useState('dashboard')


    return (
        <div className="flex">
            <aside className="abslute left-0">
                <DashboardSideNavigation
                    activeComponent={activeComponent}
                    setActiveComponent={setActiveComponent}
                />
            </aside>
            <div className="flex mx-auto">
                {activeComponent === "dashboard" && dashboard() }
                {activeComponent === "relatorios" && relatorios() }
                {activeComponent === "historico" && historico() }
            </div>
        </div>
    )
}