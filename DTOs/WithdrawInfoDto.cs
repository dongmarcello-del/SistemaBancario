namespace SistemaBancario.DTOs;
public class WithdrawInfoDto
{
    public Guid SenderAccountId { get; set; }
    public Guid ReceiverAccountId { get; set; }
    public decimal Amount { get; set; } = 0;
}