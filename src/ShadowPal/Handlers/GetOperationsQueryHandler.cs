using MediatR;
using ShadowPal.Domain.Repositories;

namespace ShadowPal.Handlers;

public class GetOperationsQueryHandler : IRequestHandler<GetOperationsQuery, GetOperationsQueryResult>
{
    private readonly IAccountProcessingRepository _accountProcessingRepository;

    public GetOperationsQueryHandler(IAccountProcessingRepository accountProcessingRepository)
    {
        _accountProcessingRepository = accountProcessingRepository ?? throw new ArgumentNullException(nameof(accountProcessingRepository));
    }

    public async Task<GetOperationsQueryResult> Handle(GetOperationsQuery request, CancellationToken cancellationToken)
    {
        var operations = await _accountProcessingRepository.GetOperations(cancellationToken);

        return new GetOperationsQueryResult()
        {
            Operations = operations
        };
    }
}