using MediatR;
using ShadowBuddy.Domain.Repositories;

namespace ShadowBuddy.Handlers.Queries;

public class GetCurrenciesQueryHandler : IRequestHandler<GetCurrenciesQuery, GetCurrenciesQueryResult>
{
    private readonly IAccountProcessingRepository _accountProcessingRepository;

    public GetCurrenciesQueryHandler(IAccountProcessingRepository accountProcessingRepository)
    {
        _accountProcessingRepository = accountProcessingRepository ??
                                       throw new ArgumentNullException(nameof(accountProcessingRepository));
    }

    public async Task<GetCurrenciesQueryResult> Handle(GetCurrenciesQuery request, CancellationToken cancellationToken)
    {
        var currencies = await _accountProcessingRepository.GetCurrencies();

        return new GetCurrenciesQueryResult()
        {
            Currencies = currencies
        };
    }
}