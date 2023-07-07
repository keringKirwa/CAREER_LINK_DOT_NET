using CareerLinkServer.Authentication;
using CareerLinkServer.DTOs;
using CareerLinkServer.models;

namespace CareerLinkServer.services;

public class AuthService
{
    private readonly JwtProvider _jwtProvider;
    private List<User> _users = new();

    public AuthService(JwtProvider jwtProvider)
    {
        _jwtProvider = jwtProvider;
    }

    public User Register(RegisterDto registerDto)
    {
        var user = new User()
        {
            UserId = new Guid(),
            Name = registerDto.Name,
            EmailAddress = registerDto.EmailAddress,
            PhoneNumber = registerDto.PhoneNumber,


        };
        _users.Add(user);
        return user;
    }
    public string Login(LoginDto registerDto)
    {
        var user = _users.Find(user1 => user1.EmailAddress == registerDto.EmailAddress);
        if (user is null)
        {
            throw new Exception("Name or EmailAddress provided  not correct ");

        }
        return _jwtProvider.GenerateToken(user.UserId.ToString(), user.EmailAddress);
    }

}