using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SistemaBancario.DTOs;
using SistemaBancario.DTOs.Auth;
using SistemaBancario.Models;
using SistemaBancario.Services;

namespace SistemaBancario.Controllers;

// Aggiungere i codici di ritornjo

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

    [HttpPost("register")]
    public async Task<ActionResult<ResponseMessage<string>>> Register(UserDto user)
    {
        try
        {
            await _auth.Register(user);

            return Created("", new ResponseMessage<string>
            {
                Success = true, 
                Message = "Utente creato con successo"
            });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new ResponseMessage<string>
            {
                Success = false, 
                Message = ex.Message
            });
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new ResponseMessage<string>
            {
                Success = false, 
                Message = ex.Message
            });
        }
    }

    /* Login: 
       - Se il login è un successo ritorna il token
       - Se il login fallisce da un messaggio di errore
    */
    [HttpPost("login")]
    public async Task<ActionResult<ResponseMessage<string>>> Login(UserDto user)
    {
        try
        {
            var token = await _auth.Login(user);

            return Ok(new ResponseMessage<string>
            {
                Success = true,
                Message = "Utente loggato con successo!",
                Data = token
            });
        } 
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new ResponseMessage<string>
            {
                Success = false, 
                Message = ex.Message
            });
        }
    }
}