using DiiaNRCForm.Abstractions.Entities;
using DiiaNRCForm.Abstractions.Interfaces;
using DiiaNRCForm.Abstractions.Models;
using DiiaNRCForm.Abstractions.Services;
using DiiaNRCForm.Business.Commands;
using DiiaNRCForm.Infrastructure.Database;
using MediatR;

namespace DiiaNRCForm.Business.Handlers;

public class AuthorizationCommandHandler: IRequestHandler<AuthorizationCommand, AuthDeepLinkModel>
{
    private readonly IDiiaService _diiaService;
    private readonly DiiaNRCFormDbContext _context;
    private readonly IQRCreator _qrCreator;

    public AuthorizationCommandHandler(
        IDiiaService diiaService, 
        DiiaNRCFormDbContext context, 
        IQRCreator qrCreator)
    {
        _diiaService = diiaService;
        _context = context;
        _qrCreator = qrCreator;
    }

    public async Task<AuthDeepLinkModel> Handle(AuthorizationCommand request, CancellationToken cancellationToken)
    {
        var signatureRequest = new SignatureRequest
        {
            Id = Guid.NewGuid(),
            Signed = false
        };
        
        var diiaResponse = await _diiaService.Authorization(signatureRequest.Id.ToString());
        
        signatureRequest.HashedId = diiaResponse.HashedId;
        
        await _context.SignatureRequests.AddAsync(signatureRequest, cancellationToken);
        
        await _context.SaveChangesAsync(cancellationToken);
        
        return diiaResponse;
    }
}
