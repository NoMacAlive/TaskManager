using FluentValidation;
using ForsythBarr.Server.Controllers.RequestModels;

namespace ForsythBarr.Server.Controllers.Validators;

public class CreateNewTaskRequestValidator : AbstractValidator<CreateNewTaskRequest> 
{
    public CreateNewTaskRequestValidator()
    {
        RuleFor(request => request.Title).NotNull();
        RuleFor(request => request.Description).NotNull();
        RuleFor(request => request.DueDate).NotNull();
        RuleFor(request => request.Priority).NotNull();
        RuleFor(request => request.Status).NotNull();
    }
}