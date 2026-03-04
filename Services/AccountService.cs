using SistemaBancario.Data;
using SistemaBancario.DTOs;
using SistemaBancario.Models;
using SistemaBancario.Security;

namespace SistemaBancario.Services;

public class AccountService : IAccountService
{

    private readonly AppDbContext _context;

    public AccountService(AppDbContext context)
    {
        _context = context;
    }

    // Aggiunge un nuovo account all'utente attuale
    public async Task Add(CreateAccountRequestDto account, UserClaims userClaims)
    {
        if (account.Balance < 0)
            throw new InvalidDataException("Non puoi avere un'account con un balance negativo!");

        // Aggiungo l'account all'utente
        _context.Accounts.Add(new Account
        {
            UserId = userClaims.UserId,
            Balance = account.Balance
        });

        await _context.SaveChangesAsync();
    }

    // Deposita l'ammontare in un account
    public async Task Deposit(DepositInfoDto depositInfo, UserClaims userClaims)
    {
        /* Faccio i vari controlli */
        if (depositInfo.Amount <= 0)
            throw new InvalidDataException("Non puoi depositare un'ammontare negativo o nullo!");
        
        var account = await _context.Accounts.FindAsync(depositInfo.AccountId);
        if (account == null)
            throw new KeyNotFoundException("Non è stato trovato nessun account con questo IBAN!");
        
        // Se l'account non appartiene all'utente
        if (userClaims.UserId != account.UserId)
            throw new UnauthorizedAccessException("Questo account non ti appartiene!");

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
}