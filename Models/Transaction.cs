namespace SistemaBancario.Models;

public class Transaction
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; } = DateTime.UtcNow;

    // Foreign key account (mandante)
    public Guid SenderAccountId { get; set; }
    public required Account SenderAccount { get; set; }

    // Foreign key account (ricevente)
    public Guid ReceiverAccountId { get; set; }
    public required Account ReceiverAccount { get; set; }
}