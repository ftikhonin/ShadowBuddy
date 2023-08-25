using Google.Protobuf.WellKnownTypes;
using MediatR;
using ShadowBuddy.Domain.Repositories;
using ShadowBuddy.Infrastructure.Exceptions;

namespace ShadowBuddy.Handlers;

public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand>
{
    private readonly IAccountProcessingRepository _accountProcessingRepository;

    public CreateAccountCommandHandler(IAccountProcessingRepository accountProcessingRepository)
    {
        _accountProcessingRepository = accountProcessingRepository ??
                                       throw new ArgumentNullException(nameof(accountProcessingRepository));
    }

    public async Task Handle(CreateAccountCommand request, CancellationToken cancellationToken) =>
        await _accountProcessingRepository.CreateAccount(request.UserId, request.Name, request.Balance,
            request.InitialDate, request.CurrencyId, cancellationToken);
}