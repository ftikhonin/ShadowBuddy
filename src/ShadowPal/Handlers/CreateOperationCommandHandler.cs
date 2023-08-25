using Google.Protobuf.WellKnownTypes;
using MediatR;
using ShadowPal.Domain.Repositories;
using ShadowPal.Infrastructure.Exceptions;

namespace ShadowPal.Handlers;

public class CreateOperationCommandHandler : IRequestHandler<CreateOperationCommand>
{
    private readonly IAccountProcessingRepository _accountProcessingRepository;

    public CreateOperationCommandHandler(IAccountProcessingRepository accountProcessingRepository)
    {
        _accountProcessingRepository = accountProcessingRepository ??
                                       throw new ArgumentNullException(nameof(accountProcessingRepository));
    }

    public async Task Handle(CreateOperationCommand request, CancellationToken cancellationToken) =>
        await _accountProcessingRepository.CreateOperation(request.AccountId, request.OperationTypeId, request.Amount,
            request.CategoryId, request.Comment, request.Moment, cancellationToken);
}