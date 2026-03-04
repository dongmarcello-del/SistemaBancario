namespace SistemaBancario.Security;

/* Classe per tenere i claim dell'utente */
public class UserClaims
{
    public required Guid UserId { get; set; }
}