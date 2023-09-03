using ShadowBuddy.Domain.Entities;

namespace ShadowBuddy.Handlers.Queries;

public class GetCurrenciesQueryResult
{
    public Currency[] Currencies { get; set; }
}