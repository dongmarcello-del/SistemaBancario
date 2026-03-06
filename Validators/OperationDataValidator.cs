using SistemaBancario.DTOs;
using SistemaBancario.Enums;
using SistemaBancario.DTOs.Account;

namespace SistemaBancario.Validators;

public class OperationDataValidator
{
    public static ResponseMessage<string>? ValidateTransfer(TransferInfoDto transferInfo)
    {
        /* Faccio i controlli */
        if (transferInfo.Amount <= 0)
            return new ResponseMessage<string> {
                Success = false,
                Message = "Non puoi trasferire un'ammontare negativo o nullo!"
            };
        
        // Non puoi trasferire i soldi ad uno stesso account
        if (transferInfo.SenderAccountId == transferInfo.ReceiverAccountId)
            return new ResponseMessage<string> {
                Success = false,
                Message = "Non puoi trasferire denaro allo stesso account! fai un deposito"
            };

        return null;
    }

    public static ResponseMessage<string>? ValidateCashOperation(CashOperationInfoDto cashOperation, CashOperationType cashOperationType)
    {
        if (cashOperation.Amount <= 0 && cashOperationType == CashOperationType.Deposit)
        {
            string verb = cashOperationType == CashOperationType.Deposit ? "depositare" : "prelevare";
            return new ResponseMessage<string> {
                Success = false,
                Message = $"Non puoi {verb} un'ammontare negativo o nullo!"
            };
        }
        
        return null;
    }
}