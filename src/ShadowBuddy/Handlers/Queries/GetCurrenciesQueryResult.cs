using ShadowBuddy.Domain.Entities;

namespace ShadowBuddy.Handlers.Queries;

public class GetCurrenciesQueryResult
{
    public Currency[] Currencies { get; set; }

    public GetCurrenciesQueryResult(Currency[] currencies)
    {
        Currencies = currencies;
    }
}