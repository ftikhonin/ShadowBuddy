using ShadowBuddy.Domain.Entities;

namespace ShadowBuddy.Handlers.Queries;

public class GetAccountsQueryResult
{
    public Account[] Accounts { get; set; }
}