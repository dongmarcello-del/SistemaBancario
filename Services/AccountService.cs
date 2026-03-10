using SistemaBancario.Data;
using SistemaBancario.DTOs.Account;
using SistemaBancario.Models;
using SistemaBancario.Security;
using SistemaBancario.Guards;
using SistemaBancario.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace SistemaBancario.Services;

/*
    - Solo i servizi lanciano errori, i controller li intercettano e scrivono la risposta
    - I validatori dei dati dell'utente (che non si interfacciano col DB) ritornano solo ResponseMessage
    - Mentre i Guards, che fanno validazione 
*/

public class AccountService : IAccountService
{

    private readonly AppDbContext _context;

    public AccountService(AppDbContext context)
    {
        _context = context;
    }

    // Scrittura database
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
        Console.WriteLine($"Deposit request: AccountId={depositInfo.AccountId}, Amount={depositInfo.Amount}");
        var account = await _context.Accounts.FindAsync(depositInfo.AccountId);
        /* Faccio i vari controlli, se l'account risulta valido lo ritorna altrimenti lancia un'eccezione */
        account = OperationGuard.CheckCashOperationValidity(depositInfo, account, userClaims, TransactionType.Deposit);

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
        var account = await _context.Accounts.FindAsync(withdrawInfo.AccountId);
        /* Faccio i vari controlli, se l'account risulta valido lo ritorna altrimenti lancia un'eccezione */
        account = OperationGuard.CheckCashOperationValidity(withdrawInfo, account, userClaims, TransactionType.Withdraw);

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
        var accounts = await _context.Accounts
            .Where(a => a.Id == transferInfo.SenderAccountId || a.Id == transferInfo.ReceiverAccountId)
            .ToListAsync();

        /* Faccio i controlli */
        var (senderAccount, receiverAccount) = OperationGuard.CheckTransferValidity(transferInfo, accounts, userClaims);

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

    // Lettura database

    public async Task<decimal> GetBalance(GetAccountBalanceDto getAccountBalanceDto, UserClaims userClaims)
    {

        var account = await _context.Accounts.FindAsync(getAccountBalanceDto.AccountId);

        account = InfoRetrevierGuard.EnsureRetrievable(account, userClaims);

        // Se è null ritorno il balance attuale
        if (getAccountBalanceDto.Date == null) 
            return account.Balance;
        
        /// Transazione
        /// Sender: accountId, Receiver: accountId Amount > 0 Deposito (positivo per accountId)
        /// Sender: accountId, Receiver: accountId Amount < 0 Prelievo (negativo per accountId)
        /// Sender: accountId, Receiver: accountId2 Amount > 0 Trasferimento (negativo per accountId)
        /// Sender: accountId, Receiver: accountId Amount >= 0 Creazione account (ed è la prima transazione) (positivo per accountId)
        /// Sender: accountId2 Receiver: accountId Amount > 0 Trasferimento (positivo per accountId)

        // Se c'è una data ritorno il balance in quella data


        var balance = await _context.Transactions
                                            .Where(
                                                t => (t.ReceiverAccountId == getAccountBalanceDto.AccountId 
                                                || t.SenderAccountId == getAccountBalanceDto.AccountId)
                                                && t.Date <= getAccountBalanceDto.Date.Value
                                            ).SumAsync(t => t.ReceiverAccountId == getAccountBalanceDto.AccountId ? t.Amount : -t.Amount);
        return balance;
    }

    public async Task<List<Guid>> GetAccountsId()
    {
        var accountsId = await _context.Accounts
                .Select(a => a.Id)
                .ToListAsync();

        return accountsId;
    }
}