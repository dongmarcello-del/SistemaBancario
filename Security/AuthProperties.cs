namespace SistemaBancario.Security;

public class AuthProperties
{
    public required string Issuer { get; set; }
    public required string Audience { get; set; }
    public required string SecretKey { get; set; }
    public required string ExpiryMinutes { get; set; }
}