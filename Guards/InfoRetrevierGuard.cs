using SistemaBancario.Data;
using SistemaBancario.DTOs;
using SistemaBancario.Models;
using SistemaBancario.Security;

namespace SistemaBancario.Guards;
public class InfoRetrevierGuard
{
    public static async Task<Account> EnsureRetrievable(GetAccountBalanceDto getAccountBalanceDto, AppDbContext _context, UserClaims userClaims)
    {
        var account = await _context.Accounts.FindAsync(getAccountBalanceDto.AccountId);

        if (account == null)
            throw new KeyNotFoundException("Non è stato trovato nessun account con questo id!");

        if (account.UserId != userClaims.UserId) 
            throw new UnauthorizedAccessException("Non puoi accedere ad un'account non tuo!");
        
        return account;
    }
}