namespace SistemaBancario.Models;
public class Account
{
    // Lo trattiamo come IBAN
    public Guid Id { get; set; } = Guid.NewGuid(); 
    public decimal Balance { get; set; } = 0;

    // Foreign key di utente
    public Guid UserId { get; set; }
    public User? User { get; set; }

    // Transazioni dove l'account è un mandante
    public List<Transaction>? SentTransactions { get; set; }

    // Transazioni dove l'account è un ricevente
    public List<Transaction>? ReceivedTransactions { get; set; }

}