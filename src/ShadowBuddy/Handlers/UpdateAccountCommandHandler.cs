using Google.Protobuf.WellKnownTypes;
using MediatR;
using ShadowBuddy.Domain.Repositories;
using ShadowBuddy.Infrastructure.Exceptions;

namespace ShadowBuddy.Handlers;

public class UpdateAccountCommandHandler : IRequestHandler<UpdateAccountCommand>
{
    private readonly IAccountProcessingRepository _accountProcessingRepository;

    public UpdateAccountCommandHandler(IAccountProcessingRepository accountProcessingRepository)
    {
        _accountProcessingRepository = accountProcessingRepository ??
                                       throw new ArgumentNullException(nameof(accountProcessingRepository));
    }

    public async Task Handle(UpdateAccountCommand request, CancellationToken cancellationToken) =>
        await _accountProcessingRepository.UpdateAccount(request.AccountId, request.Name, request.Balance,
            request.InitialDate, request.CurrencyId, cancellationToken);
}