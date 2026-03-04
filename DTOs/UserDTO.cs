namespace SistemaBancario.DTOs;

/* 
    Questo sarà il DTO principale per l'autenticazione

    Ne uso uno solo(sia register che login) per evitare complicazioni ulteriori 
*/ 
public class UserDTO
{
    public required string Email { get; set; }
    public required string Password { get; set; }
}