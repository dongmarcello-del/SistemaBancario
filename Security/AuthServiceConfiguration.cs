using System.Text;
using SistemaBancario.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace SistemaBancario.Security;

/*
    Classe helper che configura la validazione nel Program.cs

    Validazione:
    - Scrivi le proprietà dell'autenticazione all'interno di appsettings.json
    - Questa classe le mappa all'interno dell'oggetto AuthProperties
    - Aggiungi il servizio di autenticazione usando l'oggetto AuthProperties
    - Puoi usare [Authorize] nell'endpoint che ti valida il token
*/

public static class AuthServiceConfiguration
{
    public static IServiceCollection AddAuthServices(this IServiceCollection services, IConfiguration configuration)
    {
        /* 
            - Prendo la sezione AuthPropertiesConfig dentro appsettings.json e la mappo dentro AuthProperties
            - Rendo la configurazione riutilizzabile da tutte le classi

            Poi la configurazione servirà anche ad AuthService.cs
        */
        services.Configure<AuthProperties>(configuration.GetSection("AuthPropertiesConfig"));
        // Quando qualcuno richiede IAuthService crea e fornisci un AuthService
        services.AddScoped<IAuthService, AuthService>();

        // Prendo le proprietà della configurazione mappate all'interno della classe AuthProperties
        var jwtSettings = configuration.GetSection("AuthPropertiesConfig").Get<AuthProperties>();
        // Prendo la chiave e la encoddo in UTF8
        var key = Encoding.UTF8.GetBytes(jwtSettings.SecretKey);

        /*
            Uso la tipologia di autenticazione predefinita.
            Nell'header:
            Authorization: Bearer {token}
        */
        services.AddAuthentication(options =>
        {
            // Quando devo identificare un'utente usa JWT
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            // Quando il token è invalido ritorna 401 Unauthorized
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            // Quando arriva jwt validalo usando queste regole
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings.Issuer,
                ValidAudience = jwtSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ClockSkew = TimeSpan.Zero
            };
        });

        services.AddAuthorization();

        return services;
    }
}