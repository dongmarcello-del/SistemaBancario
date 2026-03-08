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

    useEffect(() => {
        async function loadBalance() {
            const balance = await getCurrentBalance(accountId);
            setBalance(balance.data);
        }
        loadBalance();
        async function loadLast20Transactions() {
            const last20Transactions = await getLast20Transaction(accountId);
            setTransactions(last20Transactions.data);
        }
        loadLast20Transactions();

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
                    return deposit({ accountId, ...values });
                }}
                dataHandler={undefined}
                submitText="+"
            />
            <Form
                fields={[
                    { name: "amount", type: "number", placeholder: "prelievo" },
                ]}
                onSubmit={async (values) => {
                    return withdraw({ accountId, ...values });
                }}
                dataHandler={undefined}
                submitText="-"
            />
            <TransactionTable transactions={transactions}/>
        </>
    )
}