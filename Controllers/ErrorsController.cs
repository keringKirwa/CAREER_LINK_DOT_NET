using Microsoft.AspNetCore.Mvc;

namespace CareerLinkServer.Controllers;

public class ErrorsController : ControllerBase
{
    [HttpGet]
    [Route("/error")]
    public IActionResult Error()
    {
        return Problem();
    }

}