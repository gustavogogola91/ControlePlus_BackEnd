"use client"

import { useRouter } from "next/navigation"
import { useEffect, useState } from "react"

type OptionType = 'home' | 'administrativo'

const parseOption = (value: string): OptionType => {
    const options: OptionType[] = ["home", "administrativo"];
    return options.includes(value as OptionType)
        ? value as OptionType
        : "home";
};

export const Header = () => {

    const router = useRouter()
    const [option, setOption] = useState<OptionType>('home')

    useEffect(() => {
        const savedOption = localStorage.getItem("headerOption") as OptionType | null;
        if (savedOption) {
            setOption(savedOption);
        }
        
    }, []);

    const handleOptionChange = (newOption: OptionType) => {
        router.push(newOption)
        localStorage.setItem("headerOption", newOption);
        setOption(newOption);
    }


    return (
        <header className="w-full flex justify-between p-5 border-b-gray">
            <h1 className="font-semibold text-[32px]">Controle <span className="text-blue ">+</span></h1>
            <nav>

                <button
                    className={
                        `font-semibold p-2 m-2 
                ${option == 'home' ? "bg-blue text-white shadow rounded-[10px]" : "bg-white text-black"}`}

                    onClick={() => {
                        handleOptionChange('home')
                    }
                    }
                >Home</button>
                <button
                    className={
                        `font-semibold p-2 m-2 
                ${option == 'administrativo' ? "bg-blue text-white rounded-[10px] shadow" : "bg-white text-black"}`}

                    onClick={() => {
                        handleOptionChange('administrativo')
                    }
                    }
                >Administrativo</button>
            </nav>
        </header>

    )
}