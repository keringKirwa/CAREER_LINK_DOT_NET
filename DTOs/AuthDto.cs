namespace CareerLinkServer.DTOs;


public record RegisterDto(string Name, string EmailAddress, string PhoneNumber);

public record LoginDto(string Name, string EmailAddress);
