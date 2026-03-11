using FluentValidation;
using SistemaBancario.DTOs.Account;

namespace SistemaBancario.Validators;

public class TransferInfoValidator : AbstractValidator<TransferInfoDto>
{
    public TransferInfoValidator()
    {
        RuleFor(x => x.Amount)
            .GreaterThan(0).WithMessage("Non puoi trasferire un'ammontare negativo o nullo!");

        RuleFor(x => x.ReceiverAccountId)
            .NotEqual(x => x.SenderAccountId)
            .WithMessage("Non puoi trasferire denaro allo stesso account! Fai un deposito");
    }
}

public class DepositValidator : AbstractValidator<CashOperationInfoDto>
{
    public DepositValidator()
    {
        RuleFor(x => x.Amount)
            .GreaterThan(0)
            .WithMessage("Non puoi depositare un'ammontare negativo o nullo!");
    }
}

public class WithdrawValidator : AbstractValidator<CashOperationInfoDto>
{
    public WithdrawValidator()
    {
        RuleFor(x => x.Amount)
            .GreaterThan(0)
            .WithMessage("Non puoi prelevare un'ammontare negativo o nullo!");
    }
}