using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SistemaBancario.Data;
using SistemaBancario.DTOs.Auth;
using SistemaBancario.Security;
using SistemaBancario.Models;
using SistemaBancario.Validators;

namespace SistemaBancario.Services;

public class AuthService : IAuthService
{
    private readonly AppDbContext _dbContext; 
    private readonly AuthProperties _config;
    private readonly PasswordDecEnc _passwordDecEnc;

    public AuthService(AppDbContext dbContext, IOptions<AuthProperties> options)
    {
        _dbContext = dbContext;
        _config = options.Value;
        _passwordDecEnc = new PasswordDecEnc();
    }

    public async Task Register(UserDto user)
    {

        /* Controlli sulla registrazione lato (dominio), possono essere trasferiti come guard */

        var validationError = UserDataValidator.Validate(user);

        if (validationError != null)
            throw new ArgumentException(validationError.Message);

        /* Controlli sulla registrazione lato (DB) */
        var exists = await _dbContext.Users.AnyAsync(u => u.Email == user.Email);

        if (exists) 
            throw new InvalidOperationException("Email già registrata");

        /* Aggiunta dell'utente */
        await _dbContext.Users.AddAsync(
            new User()
            {
                Email = user.Email,
                Password = _passwordDecEnc.Hash(user.Password)
            }
        );

        await _dbContext.SaveChangesAsync();
    }

    public async Task<string> Login(UserDto user)
    {
        // Controlli sul database (possono essere trasferiti come guard)
        var userLogged = await _dbContext.Users.SingleOrDefaultAsync(u => u.Email == user.Email);

        if (userLogged == null) 
            throw new UnauthorizedAccessException("Utente non trovato! Prima registrati");

        // Verifica della password 
        var verifyPwd = _passwordDecEnc.Verify(userLogged, userLogged.Password, user.Password);

        if (verifyPwd == PasswordVerificationResult.Failed) 
            throw new UnauthorizedAccessException("Credenziali non valide");

        // Se è necessario un rehash
        if (verifyPwd == PasswordVerificationResult.SuccessRehashNeeded)
        {
            userLogged.Password = _passwordDecEnc.Hash(user.Password);
            await _dbContext.SaveChangesAsync();
        }

        // Restituisco il token di registrazione in base alla configurazione precedente
        var secretKey = _config.SecretKey;
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, userLogged.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var token = new JwtSecurityToken(
            issuer: _config.Issuer,
            audience: _config.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(double.Parse(_config.ExpiryMinutes)),
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}