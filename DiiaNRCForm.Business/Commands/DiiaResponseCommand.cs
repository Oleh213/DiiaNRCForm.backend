using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DiiaNRCForm.Business.Commands;

public class DiiaResponseCommand : IRequest<IActionResult>
{
    public DiiaResponseCommand(string hashedRequestId)
    {
        HashedRequestId = hashedRequestId;
    }

    public string HashedRequestId { get; set; }
}