using FluentValidation;
using ShadowBuddy.Service.Grpc;

namespace ShadowBuddy.Validators;

public class CreateOperationRequestValidator : AbstractValidator<CreateOperationRequest>
{
    public CreateOperationRequestValidator()
    {
        RuleFor(x => x.Amount).GreaterThan(0).WithMessage("Amount must be greater than zero");
    }
}