using SistemaBancario.DTOs;

namespace SistemaBancario.Services;

public interface IAuthService
{
    public Task<ResponseMessage<string>> Register(UserDTO user);
    public Task<ResponseMessage<string>> Login(UserDTO user);
}