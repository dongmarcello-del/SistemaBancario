import { useEffect } from "react";
import Form from "../components/Form";
import { deposit, withdraw } from "../services/accountService";
import { useParams } from "react-router";

export default function Dashboard() {

    const params = useParams();
    const accountId = params.idAccount;

    useEffect(() => {

    });

    return (
        <>
            <h1>Dashboard</h1>
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
        </>
    )
}