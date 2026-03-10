import { useEffect, useState } from "react";
import Form from "../components/Form";
import { deposit, getCurrentBalance, getLast20Transaction, withdraw } from "../services/accountService";
import { useParams } from "react-router";
import TransactionTable from "../components/TransactionTable";
import type { transactionDto } from "../DTOs/account/transactionDto";

export default function Dashboard() {

    const [balance, setBalance] = useState(0);
    const [transactions, setTransactions] = useState<transactionDto[]>([]);
    const params = useParams();
    const accountId = params.idAccount;

    async function loadData() {
        const balanceRes = await getCurrentBalance(accountId);
        setBalance(balanceRes.data);

        const txRes = await getLast20Transaction(accountId);
        setTransactions(txRes.data);
    }

    useEffect(() => {
        loadData();
    }, [accountId]); // Si mette per sicurezza

    return (
        <>
            <h1>Dashboard</h1>
            <p>Balance: {balance}</p>
            <Form
                fields={[
                    { name: "amount", type: "number", placeholder: "deposito" },
                ]}
                onSubmit={async (values) => {
                    const res = await deposit({ accountId, ...values });
                    await loadData();
                    return res; 
                }}
                dataHandler={undefined}
                submitText="+"
            />
            <Form
                fields={[
                    { name: "amount", type: "number", placeholder: "prelievo" },
                ]}
                onSubmit={async (values) => {
                    const res = await withdraw({ accountId, ...values });
                    await loadData();
                    return res; 
                }}
                dataHandler={undefined}
                submitText="-"
            />
            <TransactionTable transactions={transactions} />
        </>
    )
}