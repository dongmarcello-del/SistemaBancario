using SistemaBancario.DTOs;
using System.Net.Mail;

namespace SistemaBancario.Validators;

/* 
    Classe che si occupa di gestire la validità delle credenziali dell'utente NON lato database

    Da usare solo nella registrazione
*/

public class UserDataValidator
{
    public static ResponseMessage<string>? Validate(UserDto user)
    {
        if (string.IsNullOrWhiteSpace(user.Email) || string.IsNullOrWhiteSpace(user.Password)) return new ResponseMessage<string> {
            Success = false,
            Message = "Devi inserire la mail o la password"
        };

        if (!IsValidEmail(user.Email)) return new ResponseMessage<string> {
            Success = false,
            Message = "Devi inserire una mail corretta!"
        };

        if (user.Password.Length < 8) return new ResponseMessage<string> {
            Success = false,
            Message = "Password troppo corta! Min. 8 caratteri"
        };

        return null;
    } 

    private static bool IsValidEmail(string email)
    {
        try
        {
            var addr = new MailAddress(email);

            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }
}