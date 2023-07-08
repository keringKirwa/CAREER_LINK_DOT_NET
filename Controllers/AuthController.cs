using CareerLinkServer.DTOs;
using CareerLinkServer.models;
using CareerLinkServer.services;
using Microsoft.AspNetCore.Mvc;

namespace CareerLinkServer.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;
    
    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    
    [HttpPost("register", Name = "Register")]
    public IActionResult Register(RegisterDto registerDto, CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
        {
            return BadRequest("Registration was canceled.");
        }

        var user = new User()
        {
            UserId = new Guid(),
            PhoneNumber = registerDto.PhoneNumber,
            EmailAddress = registerDto.EmailAddress,
            Name = registerDto.Name

        };
       return  Ok(_authService.Register(registerDto));
        
    }

    
    [HttpPost("login", Name = "Login")]
    public IActionResult Login([FromBody] LoginDto loginDto)
    {

        return Ok(_authService.Login(loginDto));


    }
    
}
