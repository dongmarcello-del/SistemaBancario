using SistemaBancario.DTOs;
using SistemaBancario.DTOs.Transaction;
using SistemaBancario.Models;
using SistemaBancario.Security;

public interface ITransactionService 
{
    public Task<List<ResponseTransactionsDto>> GetTransactions(GetTransactionsDto getTransactionsDto, UserClaims userClaims);
}