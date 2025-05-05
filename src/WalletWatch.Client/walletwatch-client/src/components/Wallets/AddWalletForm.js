import React, { useState } from "react";
import { addWallet } from "../../services/WalletService";

function AddWalletForm({ family, onWalletAdded }) { 
    const [description, setDescription] = useState("");
    const [currency, setCurrency] = useState("BYN");
    const [balance, setBalance] = useState(0);
    const [parentWallet, setParentWallet] = useState("");
    const [familyMembers, setFamilyMembers] = useState("");
    const headMember = family?.headMember || { id: "unknown", name: "Не указан" };

    const handleSubmit = async (event) => {
        event.preventDefault();

        const newWallet = {
            balance,
            description,
            currency,
            family: {
                id: family.id, 
                name: family.familyName, 
                headMember: {
                    id: family.headMember.id, 
                    name: family.headMember.name 
                }
            }
        };
        
        console.log("вот что отправляю", newWallet);
        
        try {
            const wallet = await addWallet(newWallet);
            onWalletAdded(wallet);
            setDescription("");
            setCurrency("BYN");
            setBalance(0);
            setParentWallet("");
            setFamilyMembers("");
        } catch (error) {
            console.error("Ошибка:", error);
        }
    };

    return (
        <form onSubmit={handleSubmit} className="d-flex flex-column gap-2 mt-3">
            <input type="text" className="form-control" placeholder="Описание кошелька" value={description}
                   onChange={(e) => setDescription(e.target.value)} required/>
            <input type="number" className="form-control" placeholder="Баланс" value={balance}
                   onChange={(e) => setBalance(Number(e.target.value))} required/>

            <select className="form-select" value={currency} onChange={(e) => setCurrency(e.target.value)}>
                <option value="BYN">BYN (Белорусский рубль)</option>
                <option value="USD">USD (Доллар США)</option>
                <option value="RUB">RUB (Российский рубль)</option>
            </select>
            
            <input type="text" className="form-control" placeholder="ID родительского кошелька (если есть)"
                   value={parentWallet} onChange={(e) => setParentWallet(e.target.value)}/>
            <input type="text" className="form-control" placeholder="Члены семьи (через запятую)" value={familyMembers}
                   onChange={(e) => setFamilyMembers(e.target.value)}/>

            <button type="submit" className="btn btn-success">Добавить кошелек</button>
        </form>
    );
}

export default AddWalletForm;
