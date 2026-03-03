namespace SistemaBancario.Models;
public class User
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public required string Email { get; set; }
    public required string Password { get; set; }
    
    // Un utente può avere diversi conti
    public List<Account>? Accounts { get; set; } 
}