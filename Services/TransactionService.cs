using Microsoft.EntityFrameworkCore;
using SistemaBancario.Data;
using SistemaBancario.DTOs;
using SistemaBancario.Models;
using SistemaBancario.Enums;

class TransactionService : ITransactionService
{
    private readonly AppDbContext _context;

    public TransactionService(AppDbContext context)
    {
        _context = context;
    }
    public async Task<List<Transaction>> GetTransactions(GetTransactionsDto getTransactionsDto)
    {
        List<Transaction> transactions = await _context.Transactions.Where(
            t => t.ReceiverAccountId == getTransactionsDto.AccountId &&
            t.SenderAccountId == getTransactionsDto.AccountId && 
            getTransactionsDto.cashOperationType == CashOperationType.Deposit ? 
            (t.Amount > 0) : // Se è maggiore di 0 è un versamento
            (t.Amount < 0) // Se è minore di 0 è un prelievo
        ).ToListAsync();

        return transactions;
    }
}