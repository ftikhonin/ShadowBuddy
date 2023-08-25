using Google.Protobuf.WellKnownTypes;
using MediatR;
using ShadowPal.Domain.Repositories;
using ShadowPal.Infrastructure.Exceptions;

namespace ShadowPal.Handlers;

public class DeleteAccountCommandHandler : IRequestHandler<DeleteAccountCommand>
{
    private readonly IAccountProcessingRepository _accountProcessingRepository;

    public DeleteAccountCommandHandler(IAccountProcessingRepository accountProcessingRepository)
    {
        _accountProcessingRepository = accountProcessingRepository ??
                                       throw new ArgumentNullException(nameof(accountProcessingRepository));
    }

    public async Task Handle(DeleteAccountCommand request, CancellationToken cancellationToken) =>
        await _accountProcessingRepository.DeleteAccount(request.AccountId, cancellationToken);
}