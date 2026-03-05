using SistemaBancario.DTOs;
using SistemaBancario.Models;

public interface ITransactionService 
{
    public Task<List<Transaction>> GetTransactions(GetTransactionsDto getTransactionsDto);
}