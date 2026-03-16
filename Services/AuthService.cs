using LiveChat.Api.Data;
using LiveChat.Api.Models;
using LiveChat.Api.Services;
using Microsoft.EntityFrameworkCore;

namespace LiveChat.Api.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly LiveChatDbContext _context;
        private readonly TokenService _token;
        public AuthService(LiveChatDbContext context, TokenService tokenService)
        {
            _context = context;
            _token = tokenService;
        }

        public async Task<bool> Register(RegisterDto registerDto)
        {
            var email = await _context.Users.FirstOrDefaultAsync(u => u.Email == registerDto.Email);
            if (email != null) return false;

            var passwordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password);

            User newUser = new User
            {
                Username = registerDto.Username,
                Email = registerDto.Email,
                PasswordHash = passwordHash,
                CreatedAt = DateTime.UtcNow
            };

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<string?> Login(LoginDto loginDto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == loginDto.Email);
            if (user == null) return null;

            bool password = BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash);
            if (password == false) return null;

            var token = _token.GenerateToken(user);

            return token;
        }
    }
}