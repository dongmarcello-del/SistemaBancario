namespace SistemaBancario.DTOs.Account;

// Riguarda le operazioni che riguardano i contanti (deposito e prelievo)

public class CashOperationInfoDto
{
    public Guid AccountId { get; set; }
    public decimal Amount { get; set; } = 0;
}