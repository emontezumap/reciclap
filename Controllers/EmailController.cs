using Microsoft.AspNetCore.Mvc;
using Mail;

namespace Controllers;

[ApiController]
[Route("[controller]")]
public class EmailController : Controller
{
    private readonly IMailService mailService;

    public EmailController(IMailService mailService)
    {
        this.mailService = mailService;
    }

    [HttpPost("Send")]
    public async Task<IActionResult> Send([FromForm] MailRequest request)
    {
        try
        {
            await mailService.SendEmailAsync(request);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = $"Email no enviado: {ex.Message}" });
        }
    }
}
