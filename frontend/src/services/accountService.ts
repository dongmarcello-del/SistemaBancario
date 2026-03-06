import getAuthHeaders from "../authHelper";
import type { cashOperationDto } from "../DTOs/account/cashOperationDto";
import type { ResponseMessage } from "../DTOs/ResponseMessage";
import { DEPOSIT_ENDPOINT, WITHDRAW_ENDPOINT } from "../endpoints";

export async function deposit(newAccount: cashOperationDto): Promise<ResponseMessage<any>> {
    const response = await fetch(DEPOSIT_ENDPOINT, {
        method: "POST",
        headers: getAuthHeaders(),
        body: JSON.stringify(newAccount)
    });

    console.log(newAccount)

    const data = await response.json();

    return data;
}

export async function withdraw(newAccount: cashOperationDto): Promise<ResponseMessage<any>> {
    const response = await fetch(WITHDRAW_ENDPOINT, {
        method: "POST",
        headers: getAuthHeaders(),
        body: JSON.stringify(newAccount)
    });

    const data = await response.json();

    return data;
}