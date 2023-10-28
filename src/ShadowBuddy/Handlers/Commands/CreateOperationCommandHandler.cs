using MediatR;
using ShadowBuddy.Domain.Enums;
using ShadowBuddy.Domain.Repositories;

namespace ShadowBuddy.Handlers.Commands;

public class CreateOperationCommandHandler : IRequestHandler<CreateOperationCommand>
{
    private readonly IAccountProcessingRepository _accountProcessingRepository;

    public CreateOperationCommandHandler(IAccountProcessingRepository accountProcessingRepository)
    {
        _accountProcessingRepository = accountProcessingRepository ??
                                       throw new ArgumentNullException(nameof(accountProcessingRepository));
    }

    public async Task Handle(CreateOperationCommand request, CancellationToken cancellationToken) =>
        await _accountProcessingRepository.CreateOperation(
            request.AccountId,
            (OperationType) request.OperationTypeId,
            request.Amount,
            (Category) request.CategoryId,
            request.Comment,
            request.Moment);
}