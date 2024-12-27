using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using DiiaNRCForm.Abstractions.AppSettings;
using DiiaNRCForm.Abstractions.Models;
using DiiaNRCForm.Business.Commands;
using DiiaNRCForm.Controllers.Attributes;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DiiaNRCForm.Controllers;

[ApiController]
[Route("api/authenticate")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;
    public AuthController(
        IMediator mediator, 
        AppSettings appSettings)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<AuthDeepLinkModel> Authorization(CancellationToken cancellationToken)
    {
        return await _mediator.Send(new AuthorizationCommand(), cancellationToken);
    }
    
    [HttpPost("response")]
    public async Task<IActionResult> ResponseFromDiia(CancellationToken cancellationToken)
    {
        var requestId = Request.Headers["X-Document-Request-Trace-Id"].ToString();

        return await _mediator.Send(new DiiaResponseCommand(requestId), cancellationToken);
    }

    [HttpPost("check-diia-signature")]
    [BasicAuth]
    public async Task<IActionResult> CheckDiiaSignature([FromBody] FormSubmission formSubmission, CancellationToken cancellationToken)
    {
        try
        {
            await _mediator.Send(new CheckDiiaSignatureCommand(formSubmission), cancellationToken);

            return Ok();
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new
            {
                Message = "An error occurred while processing the request.",
                Exception = e.Message
            });        
        }
    }
}