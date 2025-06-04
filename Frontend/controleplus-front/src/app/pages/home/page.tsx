"use client"

import { DashboardSideNavigation } from "@/app/components/ui/sideNavigation";
import { useState } from "react";

import Dashboard from "./dashboard";
import Relatorios from "./relatorios";
import Historico from "./historico";



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
                {activeComponent === "dashboard" && <Dashboard/> }
                {activeComponent === "relatorios" && <Relatorios/> }
                {activeComponent === "historico" && <Historico/> }
            </div>
        </div>
    )
}