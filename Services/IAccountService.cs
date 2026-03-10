namespace SistemaBancario.Services;

using SistemaBancario.DTOs.Account;
using SistemaBancario.Security;

public interface IAccountService
{
    Task Add(CreateAccountRequestDto account, UserClaims userClaims);
    Task Deposit(CashOperationInfoDto depositInfo, UserClaims userClaims);
    Task Withdraw(CashOperationInfoDto withdrawInfo, UserClaims userClaims);
    Task Transfer(TransferInfoDto transferInfo, UserClaims userClaims);
    Task<decimal> GetBalance(GetAccountBalanceDto getAccountBalance, UserClaims userClaims);
    Task<List<Guid>> GetAccountsId();
}