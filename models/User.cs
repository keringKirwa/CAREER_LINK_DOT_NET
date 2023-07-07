namespace CareerLinkServer.models;

public class User
{
    public Guid UserId { get; set; }
    
    public string Name { get; set; }
    public string EmailAddress { get; set; }
    public string PhoneNumber { get; set; }

}