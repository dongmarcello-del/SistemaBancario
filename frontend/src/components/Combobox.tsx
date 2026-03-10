import { useState } from "react";

type ComboBoxWithButtonProps = {
    options: string[];
    label?: string;
    onSubmit: (value: string) => void; 
};

export default function ComboBoxWithButton({ options, label, onSubmit }: ComboBoxWithButtonProps) {
    const [selected, setSelected] = useState("");

    const handleChange = (e: React.ChangeEvent<HTMLSelectElement>) => {
        setSelected(e.target.value);
    };

    const handleClick = () => {
        if (selected) {
            onSubmit(selected);
        }
    };

    return (
        <div>
            {label && <label>{label}</label>}
            <select value={selected} onChange={handleChange}>
                <option value="">Seleziona...</option>
                {options.map((opt, idx) => (
                    <option key={idx} value={opt}>
                        {opt}
                    </option>
                ))}
            </select>
            <button onClick={handleClick}>
                Vai
            </button>
        </div>
    );
}