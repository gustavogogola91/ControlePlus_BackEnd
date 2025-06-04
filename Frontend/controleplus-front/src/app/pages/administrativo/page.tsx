"use client"
import { useState } from "react"
import { AdminSideNavigation } from "@/app/components/ui/adminSideNavigation"
import Produto from "./Produto"
import Estoque from "./Estoque"


export default function administrativo() {
    const [activeComponent, setActiveComponent] = useState('produtos')

    return (
        <div className="flex">
            <aside className="abslute left-0">
                <AdminSideNavigation
                    activeComponent={activeComponent}
                    setActiveComponent={setActiveComponent} />

            </aside>
            <div className="flex mx-auto">
                {activeComponent === "produtos" && <Produto/>}
                {activeComponent === "estoque" && <Estoque/>}
            </div>
        </div>

    )
}
