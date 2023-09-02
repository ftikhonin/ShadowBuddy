using ShadowBuddy.Domain.Entities;

namespace ShadowBuddy.Handlers;

public class GetAccountsQueryResult
{
    public Account[] Accounts { get; set; }
}