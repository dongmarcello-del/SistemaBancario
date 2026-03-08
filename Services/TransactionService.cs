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

        IQueryable<Transaction> query = _context.Transactions
                                        .Where(
                                            t => t.ReceiverAccountId == account.Id || 
                                            t.SenderAccountId == account.Id
                                        );

        query = getTransactionsDto.cashOperationType switch
        {
            TransactionType.Deposit => query.Where(t => t.ReceiverAccountId == account.Id && t.SenderAccountId == account.Id && t.Amount > 0),
            TransactionType.Withdraw => query.Where(t => t.ReceiverAccountId == account.Id && t.SenderAccountId == account.Id && t.Amount < 0),
            TransactionType.Transfer => query.Where(t => t.ReceiverAccountId != t.SenderAccountId),
            _ => query
        };

        var transactions = query
            .Take(getTransactionsDto.Limit ?? int.MaxValue)
            .Select(a => new ResponseTransactionsDto
            {
                Id = a.Id,
                Date = a.Date,
                Amount = a.Amount,
                SenderAccountId = a.SenderAccountId == a.ReceiverAccountId ? null : a.SenderAccountId, 
                ReceiverAccountId = a.ReceiverAccountId == a.SenderAccountId ? null : a.ReceiverAccountId,
                cashOperationTypeString = a.SenderAccountId == a.ReceiverAccountId
                ? (a.Amount >= 0 ? "Deposit" : "Withdraw")
                : "Transfer"
            })
            .ToList();

        return transactions;
    }
}