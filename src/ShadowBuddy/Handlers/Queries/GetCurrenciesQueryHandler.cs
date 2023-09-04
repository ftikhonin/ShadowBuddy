using MediatR;
using ShadowBuddy.Domain.Repositories;
using ShadowBuddy.Infrastructure.Exceptions;

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
        
        if (currencies is null || !currencies.Any())
        {
            throw new NotFoundException($"Currencies not found.");
        }
        
        return new GetCurrenciesQueryResult(currencies);
    }
}