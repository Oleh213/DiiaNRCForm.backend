using DiiaNRCForm.Abstractions.Services;
using DiiaNRCForm.Business.Commands;
using DiiaNRCForm.Infrastructure.Database;
using MediatR;


namespace DiiaNRCForm.Business.Handlers;

public class CheckDiiaSignatureCommandHandler: IRequestHandler<CheckDiiaSignatureCommand>
{
    private readonly DiiaNRCFormDbContext _context;
    private readonly IKoboToolboxService _koboToolboxService;
    
    public CheckDiiaSignatureCommandHandler(
        DiiaNRCFormDbContext context, 
        IKoboToolboxService koboToolboxService)
    {
        _context = context;
        _koboToolboxService = koboToolboxService;
    }

    public async Task Handle(CheckDiiaSignatureCommand request, CancellationToken cancellationToken)
    {
        var signature = _context.SignatureRequests.Single(sr => sr.Id == request.FormSubmission.SignatureId);

        if (!signature.Signed)
        {
            throw new Exception($"Signature {request.FormSubmission.SignatureId} not found");
        }
        
        await _koboToolboxService.UpdateSignatureStatus(request.FormSubmission.SubmissionId.Value);
    }
}