using DiiaNRCForm.Business.Commands;
using DiiaNRCForm.Infrastructure.Database;
using DiiaNRCForm.Infrastructure.SignalR.Service;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DiiaNRCForm.Business.Handlers;

public class DiiaResponseCommandHandler: IRequestHandler<DiiaResponseCommand, IActionResult>
{
    private readonly DiiaNRCFormDbContext _context;
    private readonly IUpdateSignatureStatus _updateSignatureStatus;
    public DiiaResponseCommandHandler(
        DiiaNRCFormDbContext context, 
        IUpdateSignatureStatus updateSignatureStatus)
    {
        _context = context;
        _updateSignatureStatus = updateSignatureStatus;
    }

    public async Task<IActionResult> Handle(DiiaResponseCommand request, CancellationToken cancellationToken)
    {
        var signatureRequest = await _context.SignatureRequests.SingleOrDefaultAsync(sr=> sr.HashedId == request.HashedRequestId, cancellationToken);

        if (signatureRequest == null)
        {
            return new BadRequestObjectResult(new { success = false, message = "Missing requestId" });
        }

        signatureRequest.Signed = true;
        
        _context.SignatureRequests.Update(signatureRequest);
        
        await _context.SaveChangesAsync(cancellationToken);

        await _updateSignatureStatus.UpdateSignatureStatusAsync(signatureRequest.Id);

        return new OkObjectResult(new { success = true });
    }
}