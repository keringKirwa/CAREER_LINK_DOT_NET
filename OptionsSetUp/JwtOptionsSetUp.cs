using CareerLinkServer.Authentication;
using Microsoft.Extensions.Options;

namespace CareerLinkServer.OptionsSetUp;

public class JwtOptionsSetUp : IConfigureOptions<JwtOptions>
{
    private const string SectionName = "JWT";
    private readonly IConfiguration _configuration;

    public JwtOptionsSetUp(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void Configure(JwtOptions options)
    {
        _configuration.GetSection(SectionName).Bind(options);
        
    }
}