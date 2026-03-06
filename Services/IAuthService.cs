using SistemaBancario.DTOs.Auth;

namespace SistemaBancario.Services;

public interface IAuthService
{
    public Task Register(UserDto user);
    public Task<string> Login(UserDto user);
}