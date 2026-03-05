using SistemaBancario.Models;
using Microsoft.AspNetCore.Mvc;
using SistemaBancario.DTOs;

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

    [HttpGet]
    public async Task<ActionResult<ResponseMessage<List<Transaction>>>> GetTransactions([FromQuery] GetTransactionsDto getTransactionsDto)
    {
        List<Transaction> transactions = await _service.GetTransactions(getTransactionsDto);

        return new ResponseMessage<List<Transaction>>
        {
            Success = true,
            Message = "Ecco le tue transazioni",
            Data = transactions
        };
    }
    
}