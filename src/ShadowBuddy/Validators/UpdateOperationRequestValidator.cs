using FluentValidation;
using ShadowBuddy.Service.Grpc;

namespace ShadowBuddy.Validators;

public class UpdateOperationRequestValidator : AbstractValidator<UpdateOperationRequest>
{
    public UpdateOperationRequestValidator()
    {
        RuleFor(x => x.Amount).GreaterThan(0).WithMessage("Amount must be greater than zero");
    }
}