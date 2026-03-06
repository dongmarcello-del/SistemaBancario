import type { AuthDto } from "../DTOs/auth/AuthDto";
import type { ResponseMessage } from "../DTOs/ResponseMessage";
import { LOGIN_ENDPOINT } from "../endpoints";

export async function login(authDto: AuthDto): Promise<ResponseMessage<any>> {
    const response = await fetch(LOGIN_ENDPOINT, {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(authDto)
    });

    const data = await response.json();

    return data;
}
