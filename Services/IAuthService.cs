public interface IAuthService
{
    Task<bool> Register(RegisterDto registerDto);
    Task<string?> Login(LoginDto loginDto);
}