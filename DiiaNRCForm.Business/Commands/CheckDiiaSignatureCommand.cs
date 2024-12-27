using DiiaNRCForm.Abstractions.Models;
using MediatR;

namespace DiiaNRCForm.Business.Commands;

public class CheckDiiaSignatureCommand : IRequest
{
    public CheckDiiaSignatureCommand(FormSubmission formSubmission)
    {
        FormSubmission = formSubmission;
    }

    public FormSubmission FormSubmission { get; set; }
}