using MediatR;
using ShadowBuddy.Domain.Repositories;

namespace ShadowBuddy.Handlers.Commands;

public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand>
{
    private readonly IAccountProcessingRepository _accountProcessingRepository;

    public CreateAccountCommandHandler(IAccountProcessingRepository accountProcessingRepository)
    {
        _accountProcessingRepository = accountProcessingRepository ??
                                       throw new ArgumentNullException(nameof(accountProcessingRepository));
    }

    public async Task Handle(CreateAccountCommand request, CancellationToken cancellationToken)
    {
        var accountId = await _accountProcessingRepository.CreateAccount(request.UserId, request.Name, request.Balance,
            request.InitialDate, request.CurrencyId, cancellationToken);

        await _accountProcessingRepository.CreateOperation(accountId, 1, request.Balance, 1, "", request.InitialDate,
            cancellationToken);
    }
}