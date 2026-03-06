namespace SistemaBancario.DTOs.Account;
public class TransferInfoDto
{
    public Guid SenderAccountId { get; set; }
    public Guid ReceiverAccountId { get; set; }
    public decimal Amount { get; set; } = 0;
}