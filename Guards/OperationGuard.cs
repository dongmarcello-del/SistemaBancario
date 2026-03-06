using SistemaBancario.Data;
using SistemaBancario.DTOs;
using SistemaBancario.Models;
using SistemaBancario.Security;
using SistemaBancario.Enums;
using Microsoft.EntityFrameworkCore;
using SistemaBancario.Validators;
using SistemaBancario.DTOs.Account;

namespace SistemaBancario.Guards;

public class OperationGuard
{
    /* Se l'account risulta valido lo ritorna */
    public static async Task<Account> CheckCashOperationValidity(CashOperationInfoDto cashOperation, AppDbContext _context, UserClaims userClaims, CashOperationType cashOperationType)
    {
        var dataValidationResult = OperationDataValidator.ValidateCashOperation(cashOperation, cashOperationType);

        if (dataValidationResult != null)
            throw new InvalidOperationException(dataValidationResult.Message);
        
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
        var dataValidationResult = OperationDataValidator.ValidateTransfer(transferInfo);

        if (dataValidationResult != null)
            throw new InvalidOperationException(dataValidationResult.Message);

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