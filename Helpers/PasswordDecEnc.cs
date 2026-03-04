using Microsoft.AspNetCore.Identity;
using SistemaBancario.Models;

namespace SistemaBancario.Helpers;

/*
    Classe helper per cryptare e decryptare la password
*/

public class PasswordDecEnc
{
    private readonly PasswordHasher<User> _hasher = new();

    // Ritorna la password hashata
    public string Hash(string password) => _hasher.HashPassword(null, password);

    // Verifica che l'hash corrisponda alla password
    public PasswordVerificationResult Verify(User user, string hashedPassword, string password)
    {
        return _hasher.VerifyHashedPassword(user, hashedPassword, password);
    }
}