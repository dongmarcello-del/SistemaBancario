using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SistemaBancario.DTOs;
using SistemaBancario.Models;
using SistemaBancario.Services;

namespace SistemaBancario.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    /* Register:
       - Se il register è un successo ritorna un messaggio con su scritto registrazione di successo
         (poi sta al frontend fare la redirect alla pagina di login)
       - Se il register è un fallimento:
            Casi:
                - Email già esiste
                - Email non valida (ex. non ha la chioccola)
                - Password troppo corta (min. 8 caratteri)
        Ritorna un messaggio di errore
    */

    private readonly IAuthService _auth;

    public AuthController(IAuthService auth)
    {
        _auth = auth;
    }

    [HttpPost("/register")]
    public async Task<ResponseMessage<string>> Register(UserDTO user)
    {
        return await _auth.Register(user);
    }

    /* Login: 
       - Se il login è un successo ritorna il token
       - Se il login fallisce da un messaggio di errore
    */
    [HttpPost("/login")]
    public async Task<ResponseMessage<string>> Login(UserDTO user)
    {
        return await _auth.Login(user);
    }
}