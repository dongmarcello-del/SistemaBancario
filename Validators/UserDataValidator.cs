using FluentValidation;
using SistemaBancario.DTOs.Auth;
using System.Net.Mail;

namespace SistemaBancario.Validators;

public class UserDataValidator : AbstractValidator<UserDto>
{
    public UserDataValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Devi inserire la mail")
            .Must(IsValidEmail).WithMessage("Devi inserire una mail corretta!");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Devi inserire la password")
            .MinimumLength(8).WithMessage("Password troppo corta! Min. 8 caratteri");
    }

    private bool IsValidEmail(string email)
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