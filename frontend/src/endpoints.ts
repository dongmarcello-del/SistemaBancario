export const API_URL = "http://localhost:5097/api"

// Endpoint per la registrazione
export const LOGIN_ENDPOINT = `${API_URL}/Auth/login`;

// Endpoint riguardante l'account
export const DEPOSIT_ENDPOINT = `${API_URL}/Account/deposit`;
export const WITHDRAW_ENDPOINT = `${API_URL}/Account/withdraw`;
export const ACCOUNT_ADD_ENDPOINT = `${API_URL}/Account/add`;
export const ACCOUNT_BALANCE_ENDPOINT = `${API_URL}/Account/balance`;
export const ACCOUNT_IDS_ENDPOINT = `${API_URL}/Account/account_ids`;

// Transazioni riguardanti l'account
export const ACCOUNT_TRANSACTIONS_ENDPOINT = `${API_URL}/Transaction`;