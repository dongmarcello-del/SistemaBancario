using SistemaBancario.Models;
using Microsoft.AspNetCore.Mvc;
using SistemaBancario.DTOs;
using SistemaBancario.DTOs.Transaction;
using System.Security.Claims;
using SistemaBancario.Security;
using Microsoft.AspNetCore.Authorization;

namespace SistemaBancario.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransactionController : ControllerBase
{

    private readonly ITransactionService _service;

    public TransactionController(ITransactionService service)
    {
        _service = service;
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<ResponseMessage<List<ResponseTransactionsDto>>>> GetTransactions([FromQuery] GetTransactionsDto getTransactionsDto)
    {
        var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var userClaims = new UserClaims { UserId = Guid.Parse(currentUserId)};
        try
        {
            List<ResponseTransactionsDto> transactions = await _service.GetTransactions(getTransactionsDto, userClaims);
            return new ResponseMessage<List<ResponseTransactionsDto>>
            {
                Success = true,
                Message = "Ecco le tue transazioni",
                Data = transactions
            };
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