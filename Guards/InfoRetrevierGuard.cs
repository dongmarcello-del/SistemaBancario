using SistemaBancario.Data;
using SistemaBancario.DTOs;
using SistemaBancario.Models;
using SistemaBancario.Security;
using SistemaBancario.DTOs.Account;

namespace SistemaBancario.Guards;
public class InfoRetrevierGuard
{
    public static Account EnsureRetrievable(Account? account, UserClaims userClaims)
    {
        if (account == null)
            throw new KeyNotFoundException("Non è stato trovato nessun account con questo id!");

        if (account.UserId != userClaims.UserId) 
            throw new UnauthorizedAccessException("Non puoi accedere ad un'account non tuo!");

        return account;
    }
}