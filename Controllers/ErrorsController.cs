using Microsoft.AspNetCore.Mvc;

namespace CareerLinkServer.Controllers;

[ApiController]
[Route("[controller]")]
public class ErrorsController : ControllerBase
{
    [Route("/error")]
    public IActionResult Error()
    {
        return Problem();
    }

}