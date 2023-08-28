using MediatR;
using ShadowBuddy.Domain.Repositories;
using ShadowBuddy.Infrastructure.Exceptions;

namespace ShadowBuddy.Handlers.Commands;

public class DeleteOperationCommandHandler : IRequestHandler<DeleteOperationCommand>
{
    private readonly IAccountProcessingRepository _accountProcessingRepository;

    public DeleteOperationCommandHandler(IAccountProcessingRepository accountProcessingRepository)
    {
        _accountProcessingRepository = accountProcessingRepository ??
                                       throw new ArgumentNullException(nameof(accountProcessingRepository));
    }

    public async Task Handle(DeleteOperationCommand request, CancellationToken cancellationToken)
    {
        var operation = _accountProcessingRepository.GetOperation(request.OperationId, cancellationToken).Result;

        if (operation is null)
        {
            throw new NotFoundException($"Operation not found! {request.OperationId}");
        }

        await _accountProcessingRepository.DeleteOperation(request.OperationId, operation.AccountId, cancellationToken);
    }
}