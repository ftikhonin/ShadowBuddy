using MediatR;
using ShadowBuddy.Domain.Repositories;
using ShadowBuddy.Infrastructure.Exceptions;

namespace ShadowBuddy.Handlers;

public class GetOperationsQueryHandler : IRequestHandler<GetOperationsQuery, GetOperationsQueryResult>
{
    private readonly IAccountProcessingRepository _accountProcessingRepository;

    public GetOperationsQueryHandler(IAccountProcessingRepository accountProcessingRepository)
    {
        _accountProcessingRepository = accountProcessingRepository ??
                                       throw new ArgumentNullException(nameof(accountProcessingRepository));
    }

    public async Task<GetOperationsQueryResult> Handle(GetOperationsQuery request, CancellationToken cancellationToken)
    {
        var operations =
            await _accountProcessingRepository.GetOperations(request.AccountId, request.Moment, cancellationToken);
        
        if (!operations.Any())
        {
            throw new NotFoundException($"Operations not found. AccountId = {request.AccountId}, Moment = {request.Moment}");
        }


        return new GetOperationsQueryResult()
        {
            Operations = operations
        };
    }
}