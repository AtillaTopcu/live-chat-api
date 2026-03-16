using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService AuthServices;

    public AuthController(IAuthService authService)
    {
        this.AuthServices = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
        var result = await AuthServices.Register(registerDto);
        if (result == false) return BadRequest("Bu E-Mail adresi zaten kullanımdadır.");

        return Ok("Hesabınız başarıyla oluşturuldu.");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        var result = await AuthServices.Login(loginDto);
        if (result == null) return Unauthorized();

        return Ok(result);
    }
}