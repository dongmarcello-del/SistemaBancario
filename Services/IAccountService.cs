namespace SistemaBancario.Services;

using SistemaBancario.DTOs;
using SistemaBancario.Security;

public interface IAccountService
{
    Task Add(CreateAccountRequestDto account, UserClaims userClaims);
    Task Deposit(DepositInfoDto depositInfo, UserClaims userClaims);
}