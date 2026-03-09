import type { cashOperationDto } from "../DTOs/account/cashOperationDto";
import type { ResponseMessage } from "../DTOs/ResponseMessage";
import {
    ACCOUNT_BALANCE_ENDPOINT,
    ACCOUNT_TRANSACTIONS_ENDPOINT,
    DEPOSIT_ENDPOINT,
    WITHDRAW_ENDPOINT
} from "../endpoints";
import { apiFetch } from "./apiFetch";

export async function deposit(newAccount: cashOperationDto): Promise<ResponseMessage<any>> {

    return apiFetch(DEPOSIT_ENDPOINT, {
        method: "POST",
        body: JSON.stringify(newAccount)
    });
}

export async function withdraw(newAccount: cashOperationDto): Promise<ResponseMessage<any>> {

    return apiFetch(WITHDRAW_ENDPOINT, {
        method: "POST",
        body: JSON.stringify(newAccount)
    });
}

export async function getCurrentBalance(accountId: string | undefined): Promise<ResponseMessage<any>> {

    return apiFetch(
        ACCOUNT_BALANCE_ENDPOINT + `?AccountId=${accountId}`,
        {
            method: "GET"
        }
    );
}

export async function getLast20Transaction(accountId: string | undefined): Promise<ResponseMessage<any>> {

    return apiFetch(
        ACCOUNT_TRANSACTIONS_ENDPOINT + `?AccountId=${accountId}&Limit=20`,
        {
            method: "GET"
        }
    );
}