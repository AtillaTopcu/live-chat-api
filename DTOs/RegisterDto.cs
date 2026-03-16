using System.ComponentModel.DataAnnotations;

public class RegisterDto
{
    public required string Username { get; set; }
    [EmailAddress]
    public required string Email { get; set; }
    public required string Password { get; set; }
}