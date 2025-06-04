"use client"
import { useState } from "react"
import { AdminSideNavigation } from "@/app/components/ui/adminSideNavigation"
import produto from "./produto"
import estoque from "./estoque"


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
                {activeComponent === "produtos" && produto()}
                {activeComponent === "estoque" && estoque()}
            </div>
        </div>

    )
}
