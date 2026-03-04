namespace SistemaBancario.DTOs;

/*

    Frontend -> DepositInfoDto -> Controller -> DepositInfoDto -> Service
    Immagazzina le informazioni di deposito e in quale account
*/

public class DepositInfoDto
{
    public Guid AccountId { get; set; }
    public decimal Amount { get; set; } = 0;
}