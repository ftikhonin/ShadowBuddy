using MediatR;
using ShadowBuddy.Domain.Repositories;
using ShadowBuddy.Infrastructure.Exceptions;

namespace ShadowBuddy.Handlers.Queries;

public class GetBalanceQueryHandler : IRequestHandler<GetBalanceQuery, GetBalanceQueryResult>
{
    private readonly IAccountProcessingRepository _accountProcessingRepository;

    public GetBalanceQueryHandler(IAccountProcessingRepository accountProcessingRepository)
    {
        _accountProcessingRepository = accountProcessingRepository ??
                                       throw new ArgumentNullException(nameof(accountProcessingRepository));
    }

    public async Task<GetBalanceQueryResult> Handle(GetBalanceQuery request, CancellationToken cancellationToken)
    {
        var accounts = await _accountProcessingRepository.GetAccounts(request.UserId);

        if (!accounts.Any())
        {
            throw new NotFoundException($"Operations not found. UserId = {request.UserId}");
        }

        foreach (var account in accounts)
        {
            account.Balance = _accountProcessingRepository.GetAccountBalance(account.Id).Result;
        }


        return new GetBalanceQueryResult()
        {
            Balance = accounts.Sum(x => x.Balance)
        };
    }
}