using SistemaBancario.DTOs;
using SistemaBancario.DTOs.Transaction;
using SistemaBancario.Models;

public interface ITransactionService 
{
    public Task<List<Transaction>> GetTransactions(GetTransactionsDto getTransactionsDto);
}