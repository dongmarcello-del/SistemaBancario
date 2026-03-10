import Form from "../components/Form";
import { login } from "../services/authService";
import { useNavigate } from "react-router-dom";

export default function Login() {

    const navigate = useNavigate();

    return (
        <>
           <Form
                fields={[
                    { name: "email", type: "email", placeholder: "Email" },
                    { name: "password", type: "password", placeholder: "Password"}
                ]}
                onSubmit={login}
                dataHandler={(data) => {
                    localStorage.setItem("token", data);
                    navigate("/dashboard")
                }}
                submitText="Login"
           />
        </>
    )
}