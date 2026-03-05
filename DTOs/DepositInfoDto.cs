namespace SistemaBancario.DTOs;

/*

    Frontend -> DepositInfoDto -> Controller -> DepositInfoDto -> Service
    Immagazzina le informazioni di deposito e in quale account
*/

// Riguarda le operazioni che riguardano i contanti (deposito e prelievo)

public class CashOperationInfoDto
{
    public Guid AccountId { get; set; }
    public decimal Amount { get; set; } = 0;
}