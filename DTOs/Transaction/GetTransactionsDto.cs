using SistemaBancario.Enums;

namespace SistemaBancario.DTOs.Transaction;

/// <summary>
/// 
/// 0 = Deposito
/// 1 = Prelievo
/// 
/// </summary>

public class GetTransactionsDto
{
    public Guid AccountId { get; set; }
    public required CashOperationType cashOperationType { get; set; } 
}