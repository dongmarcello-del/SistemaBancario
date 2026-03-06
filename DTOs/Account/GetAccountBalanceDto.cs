namespace SistemaBancario.DTOs.Account;

public class GetAccountBalanceDto
{
    public Guid AccountId { get; set; }
    public DateTime? Date { get; set; }
}