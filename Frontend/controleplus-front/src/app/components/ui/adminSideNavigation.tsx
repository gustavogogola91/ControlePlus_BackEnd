import { Archive, List } from "lucide-react";
import { FC } from "react";


interface SideNavigationsProps {
    activeComponent: string,
    setActiveComponent: (component: string) => void;
}




export const AdminSideNavigation: FC<SideNavigationsProps> = ({ activeComponent, setActiveComponent }) => {


    return (
        <>
            <nav className="flex flex-col w-full gap-10 m-10">
                <button
                    className={`
                        flex gap-2 items-center px-2 pt-2 pb-1 font-semibold cursor-pointer
                        ${activeComponent == 'produtos' ? "text-blue bg-dark-gray rounded-[10px]" : "text-black "}
                    `}

                    onClick={() => setActiveComponent('produtos')}
                > <List className="inline mb-1" /> Produtos</button>
                <button
                    className={`
                        flex gap-2 items-center px-2 pt-2 pb-1 font-semibold cursor-pointer
                        ${activeComponent == 'estoque' ? "text-blue bg-dark-gray rounded-[10px]" : "text-black "}
                    `}

                    onClick={() => setActiveComponent('estoque')}
                ><Archive className="inline mb-1" /> Estoque</button>
                
            </nav>
        </>
    )


}