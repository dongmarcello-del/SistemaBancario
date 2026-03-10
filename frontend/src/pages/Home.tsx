import { useNavigate } from "react-router-dom";
import ComboBoxWithButton from "../components/Combobox";
import { useEffect, useState } from "react";
import { getAccountsId } from "../services/accountService";

export default function Home() {

    const navigate = useNavigate();
    const [accountsId, setAccountsId] = useState([]);

    async function loadData() {
        const accountsId = await getAccountsId();
        setAccountsId(accountsId.data);
    }

    useEffect(() => {
        loadData();
    }, []);

    return (
        <>
            <ComboBoxWithButton 
            options={accountsId} 
            label="Seleziona l'account" 
            onSubmit={(value) => {
                navigate("/dashboard/" + value);
            }} />
        </>
    )
}