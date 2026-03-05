using SistemaBancario.Enums;

namespace SistemaBancario.DTOs;

/// <summary>
/// Domani usare questo formato al posto di List<Transaction> -> List<ResponseTransactionsDto>
/// </summary>

public class ResponseTransactionsDto
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public decimal Amount { get; set; }
    public CashOperationType cashOperationType { get; set; }
}