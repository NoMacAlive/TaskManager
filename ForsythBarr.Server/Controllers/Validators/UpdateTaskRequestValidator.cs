using FluentValidation;
using ForsythBarr.Server.Controllers.RequestModels;
using TaskStatus = ForsythBarr.Server.Domain.Models.TaskStatus;

namespace ForsythBarr.Server.Controllers.Validators;

public class UpdateTaskRequestValidator : AbstractValidator<UpdateTaskRequest> 
{
    public UpdateTaskRequestValidator()
    {
        RuleFor(request => request.Id).NotNull();
        RuleFor(request => request.Title).NotNull();
        RuleFor(request => request.Description).NotNull();
        RuleFor(request => request.DueDate).NotNull();
        RuleFor(request => request.Priority).NotNull();
        RuleFor(request => request.Status).IsInEnum().NotNull();
    }
}