using ShadowBuddy.Domain.Entities;

namespace ShadowBuddy.Handlers;

public class GetCurrenciesQueryResult
{
    public Currency[] Currencies { get; set; }
}