namespace SistemaBancario.DTOs;

/* Classe per ritornare le risposte */
public class ResponseMessage<T>
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public T Data { get; set; } = default!;
}