using MediatR;
using ShadowBuddy.Domain.Repositories;
using ShadowBuddy.Infrastructure.Exceptions;

namespace ShadowBuddy.Handlers.Commands;

public class UpdateOperationCommandHandler : IRequestHandler<UpdateOperationCommand>
{
    private readonly IAccountProcessingRepository _accountProcessingRepository;

    public UpdateOperationCommandHandler(IAccountProcessingRepository accountProcessingRepository)
    {
        _accountProcessingRepository = accountProcessingRepository ??
                                       throw new ArgumentNullException(nameof(accountProcessingRepository));
    }

    public async Task Handle(UpdateOperationCommand request, CancellationToken cancellationToken)
    {
        var operation = _accountProcessingRepository.GetOperation(request.OperationId).Result;

        if (operation is null)
        {
            throw new NotFoundException($"Operation not found! {request.OperationId}");
        }

        await _accountProcessingRepository.UpdateOperation(
            request.OperationId,
            request.OperationTypeId,
            request.Amount,
            request.CategoryId,
            request.Comment,
            request.Moment);
    }
}