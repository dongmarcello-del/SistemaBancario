import getAuthHeaders from "../authHelper";
import type { cashOperationDto } from "../DTOs/account/cashOperationDto";
import type { ResponseMessage } from "../DTOs/ResponseMessage";
import { ACCOUNT_BALANCE_ENDPOINT, ACCOUNT_TRANSACTIONS_ENDPOINT, DEPOSIT_ENDPOINT, WITHDRAW_ENDPOINT } from "../endpoints";

export async function deposit(newAccount: cashOperationDto): Promise<ResponseMessage<any>> {
    const response = await fetch(DEPOSIT_ENDPOINT, {
        method: "POST",
        headers: getAuthHeaders(),
        body: JSON.stringify(newAccount)
    });


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

export async function getCurrentBalance(accountId: string | undefined): Promise<ResponseMessage<any>> {
    const response = await fetch(ACCOUNT_BALANCE_ENDPOINT + `?AccountId=${accountId}`, {
        method: "GET",
        headers: getAuthHeaders(),
    });

    const data = await response.json();

    return data;
}

export async function getLast20Transaction(accountId: string | undefined) {
    const response = await fetch(ACCOUNT_TRANSACTIONS_ENDPOINT + `?AccountId=${accountId}&Limit=20`, {
        method: "GET",
        headers: getAuthHeaders(),
    });

    const data = await response.json();

    return data;
}