using SistemaBancario.Data;
using SistemaBancario.DTOs;
using SistemaBancario.Models;
using SistemaBancario.Security;
using SistemaBancario.Guards;
using SistemaBancario.Enums;

namespace SistemaBancario.Services;

public class AccountService : IAccountService
{

    private readonly AppDbContext _context;

    public AccountService(AppDbContext context)
    {
        _context = context;
    }

    // Aggiunge un nuovo account all'utente attuale, pure la creazione di un nuovo conto viene considerata transazione a se stesso
    public async Task Add(CreateAccountRequestDto account, UserClaims userClaims)
    {
        if (account.Balance < 0)
            throw new InvalidDataException("Non puoi avere un'account con un balance negativo!");

        var newAccount = new Account
        {
            UserId = userClaims.UserId,
            Balance = account.Balance
        };

        // Aggiungo l'account all'utente
        _context.Accounts.Add(newAccount);

        _context.Transactions.Add(new Transaction
        {
            Amount = account.Balance,
            SenderAccountId = newAccount.Id, 
            ReceiverAccountId = newAccount.Id
        });

        await _context.SaveChangesAsync();
    }

    // Deposita l'ammontare in un account
    public async Task Deposit(CashOperationInfoDto depositInfo, UserClaims userClaims)
    {
        /* Faccio i vari controlli, se l'account risulta valido lo ritorna altrimenti lancia un'eccezione */
        var account = await OperationGuard.CheckCashOperationValidity(depositInfo, _context, userClaims, CashOperationType.Deposit);

        /* Se fai un deposito vale come transazione a se stesso */
        _context.Transactions.Add(new Transaction
        {
            Amount = depositInfo.Amount,
            SenderAccountId = depositInfo.AccountId, 
            ReceiverAccountId = depositInfo.AccountId
        });
        
        account.Balance += depositInfo.Amount;

        await _context.SaveChangesAsync();
    }

    public async Task Withdraw(CashOperationInfoDto withdrawInfo, UserClaims userClaims)
    {
        /* Faccio i vari controlli, se l'account risulta valido lo ritorna altrimenti lancia un'eccezione */
        var account = await OperationGuard.CheckCashOperationValidity(withdrawInfo, _context, userClaims, CashOperationType.Withdraw);

        /* Il prelievo è analogo al deposito ma al posto del segno positivo c'è quello negativo */
        _context.Transactions.Add(new Transaction
        {
            Amount = -withdrawInfo.Amount,
            SenderAccountId = withdrawInfo.AccountId, 
            ReceiverAccountId = withdrawInfo.AccountId
        });
        
        account.Balance -= withdrawInfo.Amount;

        await _context.SaveChangesAsync();
    }

    public async Task Transfer(TransferInfoDto transferInfo, UserClaims userClaims)
    {
        /* Faccio i controlli */
        var (senderAccount, receiverAccount) = await OperationGuard.CheckTransferValidity(transferInfo, _context, userClaims);

        /* Transferimento dei soldi */
        senderAccount.Balance -= transferInfo.Amount;
        receiverAccount.Balance += transferInfo.Amount;

        // Aggiungo la transazione
        _context.Transactions.Add(new Transaction
        {
            Amount = transferInfo.Amount,
            SenderAccountId = transferInfo.SenderAccountId, 
            ReceiverAccountId = transferInfo.ReceiverAccountId
        });

        await _context.SaveChangesAsync();
    }
}