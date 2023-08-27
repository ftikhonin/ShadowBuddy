using System.Transactions;
using MediatR;
using ShadowBuddy.Domain.Repositories;

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
        using (var transactionScope = new TransactionScope())
        {
            await _accountProcessingRepository.UpdateOperation(request.OperationId, request.OperationTypeId,
                request.Amount,
                request.CategoryId, request.Comment, request.Moment, cancellationToken);

            var balance = _accountProcessingRepository.GetAccountBalance(request.AccountId, cancellationToken).Result;

            await _accountProcessingRepository.UpdateAccountBalance(request.AccountId, balance,
                cancellationToken);

            transactionScope.Complete();
        }
    }
}