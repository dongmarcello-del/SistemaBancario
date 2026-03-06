using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SistemaBancario.DTOs;
using SistemaBancario.DTOs.Account;
using SistemaBancario.Security;
using SistemaBancario.Services;

namespace SistemaBancario.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly IAccountService _service;

    public AccountController(IAccountService service)
    {
        _service = service;
    }

    // Serve ad aggiungere un nuovo account all'utente principale
    [Authorize]
    [HttpPost("/add")]
    public async Task<ActionResult<ResponseMessage<string>>> AddAccount(CreateAccountRequestDto account)
    {
        // Prendo i claim dell'utente
        var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var userClaims = new UserClaims { UserId = Guid.Parse(currentUserId)};

        // Id dell'utente corrente preso dal claim
        try
        {
            await _service.Add(new CreateAccountRequestDto
            {
                Balance = account.Balance
            }, userClaims);

            return Ok(new ResponseMessage<string>()
            {
                Success = true,
                Message = "Account aggiunto con successo!"
            });
        } 
        catch (InvalidDataException ex)
        {
            return BadRequest(new ResponseMessage<string>()
            {
                Success = false,
                Message = ex.Message
            });
        }
    }

    [Authorize]
    [HttpPost("/deposit")]
    public async Task<ActionResult<ResponseMessage<string>>> Deposit(CashOperationInfoDto depositInfo)
    {
        // Prendo i claim dell'utente
        var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

        try
        {
            var userClaims = new UserClaims { UserId = Guid.Parse(currentUserId)};
            await _service.Deposit(depositInfo, userClaims);

            return Ok(new ResponseMessage<string>()
            {
                Success = true,
                Message = "Deposito avvenuto con successo!"
            });
        }
        catch (InvalidDataException ex)
        {
            return BadRequest(new ResponseMessage<string>()
            {
                Success = false,
                Message = ex.Message
            });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new ResponseMessage<string>()
            {
                Success = false,
                Message = ex.Message
            });
        }
    }

    [Authorize]
    [HttpPost("/withdraw")]
    public async Task<ActionResult<ResponseMessage<string>>> Withdraw(CashOperationInfoDto withdrawInfo)
    {
        // Prendo i claim dell'utente
        var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var userClaims = new UserClaims { UserId = Guid.Parse(currentUserId)};

        try
        {
            await _service.Withdraw(withdrawInfo, userClaims);

            return Ok(new ResponseMessage<string>()
            {
                Success = true,
                Message = "Prelievo avvenuto con successo!"
            });
        }
        catch (Exception ex) when (ex is InvalidDataException || ex is InvalidOperationException)
        {
            return BadRequest(new ResponseMessage<string>()
            {
                Success = false,
                Message = ex.Message
            });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new ResponseMessage<string>()
            {
                Success = false,
                Message = ex.Message
            });
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new ResponseMessage<string>()
            {
                Success = false,
                Message = ex.Message
            });
        }
    }

    [Authorize]
    [HttpPost("/transfer")]
    public async Task<ActionResult<ResponseMessage<string>>> Transfer(TransferInfoDto transferInfo)
    {
        // Prendo i claim dell'utente
        var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var userClaims = new UserClaims { UserId = Guid.Parse(currentUserId)};

        try
        {
            await _service.Transfer(transferInfo, userClaims);

            return Ok(new ResponseMessage<string>()
            {
                Success = true,
                Message = "Scambio avvenuto con successo!"
            });
        }
        catch (Exception ex) when (ex is InvalidDataException || ex is InvalidOperationException)
        {
            return BadRequest(new ResponseMessage<string>()
            {
                Success = false,
                Message = ex.Message
            });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new ResponseMessage<string>()
            {
                Success = false,
                Message = ex.Message
            });
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new ResponseMessage<string>()
            {
                Success = false,
                Message = ex.Message
            });
        }
        catch (FormatException)
        {
            return BadRequest(new ResponseMessage<string>()
            {
                Success = false,
                Message = "Id account non valido!"
            });
        }
    }

    [Authorize]
    [HttpGet("/balance")]
    public async Task<ActionResult<ResponseMessage<decimal>>> GetBalance([FromQuery] GetAccountBalanceDto getAccountBalance)
    {
        var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var userClaims = new UserClaims { UserId = Guid.Parse(currentUserId)};

        try
        {
            var balance = await _service.GetBalance(getAccountBalance, userClaims);

            return Ok(new ResponseMessage<decimal>()
            {
                Success = true,
                Message = "Balance preso con successo!",
                Data = balance
            });
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new ResponseMessage<string>()
            {
                Success = false,
                Message = ex.Message
            });
        }
        catch (KeyNotFoundException ex)
        {
            return BadRequest(new ResponseMessage<string>()
            {
                Success = false,
                Message = ex.Message
            });
        }
    }
}