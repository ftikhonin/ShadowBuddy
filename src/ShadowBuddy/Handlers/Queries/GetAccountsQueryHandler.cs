using MediatR;
using ShadowBuddy.Domain.Repositories;
using ShadowBuddy.Infrastructure.Exceptions;

namespace ShadowBuddy.Handlers.Queries;

public class GetAccountsQueryHandler : IRequestHandler<GetAccountsQuery, GetAccountsQueryResult>
{
    private readonly IAccountProcessingRepository _accountProcessingRepository;

    public GetAccountsQueryHandler(IAccountProcessingRepository accountProcessingRepository)
    {
        _accountProcessingRepository = accountProcessingRepository ??
                                       throw new ArgumentNullException(nameof(accountProcessingRepository));
    }

    public async Task<GetAccountsQueryResult> Handle(GetAccountsQuery request, CancellationToken cancellationToken)
    {
        var accounts = await _accountProcessingRepository.GetAccounts(request.UserId);

        if (accounts is null || !accounts.Any())
        {
            throw new NotFoundException($"Accounts not found. UserId = {request.UserId}");
        }

        foreach (var account in accounts)
        {
            account.Balance = _accountProcessingRepository.GetAccountBalance(account.Id).Result;
        }


        return new GetAccountsQueryResult(accounts);
    }
}