using Microsoft.EntityFrameworkCore;
using SistemaBancario.Data;
using SistemaBancario.DTOs.Transaction;
using SistemaBancario.Models;
using SistemaBancario.Enums;
using SistemaBancario.Guards;
using SistemaBancario.Security;

class TransactionService : ITransactionService
{
    private readonly AppDbContext _context;

    public TransactionService(AppDbContext context)
    {
        _context = context;
    }
    public async Task<List<ResponseTransactionsDto>> GetTransactions(GetTransactionsDto getTransactionsDto, UserClaims userClaims)
    {

        var account = await _context.Accounts.FindAsync(getTransactionsDto.AccountId);

        account = InfoRetrevierGuard.EnsureRetrievable(account, userClaims);

        List<ResponseTransactionsDto> transactions = _context.Transactions.Where(
            t => t.ReceiverAccountId == account.Id &&
            t.SenderAccountId == account.Id && 
            getTransactionsDto.cashOperationType == CashOperationType.Deposit ? 
            (t.Amount > 0) : // Se è maggiore di 0 è un versamento
            (t.Amount < 0) // Se è minore di 0 è un prelievo
        ).Select(a => new ResponseTransactionsDto
        {
            Id = a.Id, 
            Date = a.Date,
            Amount = a.Amount,
            cashOperationType = getTransactionsDto.cashOperationType
        }).ToList();

        return transactions;
    }
}