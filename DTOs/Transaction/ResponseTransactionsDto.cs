using SistemaBancario.Enums;

namespace SistemaBancario.DTOs.Transaction;

/// <summary>
/// Domani usare questo formato al posto di List<Transaction> -> List<ResponseTransactionsDto>
/// e suddividere in sottogruppi i dto
/// </summary>

public class ResponseTransactionsDto
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public decimal Amount { get; set; }
    public CashOperationType cashOperationType { get; set; }
}