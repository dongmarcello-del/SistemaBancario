import { useState } from "react";
import type { FormProps } from "../props/FormProps";

/**
 * Utente inserisce dati nella form e invia -> viene richiamato onSubmit(values) 
 * Se ritorna un dato -> altra funzione per elaborare(dataHandler)
 * Se non ritorna nulla(solo messaggio) -> riaggiorna del componente ebbasta
 */
export default function Form({ fields, onSubmit, dataHandler, submitText}: FormProps) {
    const [values, setValues] = useState(
        fields.reduce((acc, f) => ({ ...acc, [f.name]: "" }), {} as Record<string, string>)
    );
    const [responseOutput, setResponseOutput] = useState("");

    const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        const result = await onSubmit(values);

        if (result.data && dataHandler)
            dataHandler(result.data);
        
        setResponseOutput(result.message);
    }

    const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        setValues({ ...values, [e.target.name]: e.target.value})
    }

    return (
        <form onSubmit={handleSubmit}>
            {
                fields.map((f) => (
                    <input key={f.name} name={f.name} type={f.type || "text"} placeholder={f.placeholder || ""} value={values[f.name]} onChange={handleChange}/>
                ))
            }
            <button type="submit">{submitText}</button>
            <p>{responseOutput}</p>
        </form>
    )
}