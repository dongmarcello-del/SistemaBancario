using SistemaBancario.Data;
using SistemaBancario.DTOs;
using SistemaBancario.Models;
using SistemaBancario.Security;
using SistemaBancario.Enums;
using Microsoft.EntityFrameworkCore;

namespace SistemaBancario.Guards;

/*
    Questa classe ritorna gli account se le operazioni sono valide
*/

public class OperationGuard
{
    /* Se l'account risulta valido lo ritorna */
    public static async Task<Account> CheckCashOperationValidity(CashOperationInfoDto cashOperation, AppDbContext _context, UserClaims userClaims, CashOperationType cashOperationType)
    {
        if (cashOperation.Amount <= 0)
            throw new InvalidDataException("Non puoi prelevare un'ammontare negativo o nullo!");
        
        var account = await _context.Accounts.FindAsync(cashOperation.AccountId);
        if (account == null)
            throw new KeyNotFoundException("Non è stato trovato nessun account con questo IBAN!");
        
        // Se l'account non appartiene all'utente
        if (userClaims.UserId != account.UserId)
            throw new UnauthorizedAccessException("Questo account non ti appartiene!");

        if (cashOperationType == CashOperationType.Withdraw && cashOperation.Amount > account.Balance)
            throw new InvalidOperationException("Non hai abbastanza denaro in questo account!");

        return account;
    }

    /* Se i controlli van bene ritorna i due account */
    public static async Task<(Account senderAccount, Account receiverAccount)> CheckTransferValidity(TransferInfoDto transferInfo, AppDbContext _context, UserClaims userClaims)
    {
        /* Faccio i controlli */
        if (transferInfo.Amount <= 0)
            throw new InvalidDataException("Non puoi trasferire un'ammontare negativo o nullo!");
        
        // Non puoi trasferire i soldi ad uno stesso account
        if (transferInfo.SenderAccountId == transferInfo.ReceiverAccountId)
            throw new InvalidOperationException("Non puoi trasferire denaro allo stesso account! fai un deposito");

        var accounts = await _context.Accounts
            .Where(a => a.Id == transferInfo.SenderAccountId || a.Id == transferInfo.ReceiverAccountId)
            .ToListAsync();

        // Se non trova uno dei due account
        if (accounts.Count != 2)
            throw new KeyNotFoundException("Non sono stati trovati gli account!");

        var senderAccount = accounts.First(a => a.Id == transferInfo.SenderAccountId);
        var receiverAccount = accounts.First(a => a.Id == transferInfo.ReceiverAccountId);

        // Gli account esistono ma non appartengono all'utente
        if (senderAccount.UserId != userClaims.UserId || receiverAccount.UserId != userClaims.UserId)
            throw new UnauthorizedAccessException("Non sei autorizzato ad accedere a questi account!");

        // Controllo che il sender abbia abbastanza soldi
        if (senderAccount.Balance < transferInfo.Amount)
            throw new InvalidOperationException("Il mandante non ha abbastanza soldi");

        return (senderAccount, receiverAccount);
    }
}