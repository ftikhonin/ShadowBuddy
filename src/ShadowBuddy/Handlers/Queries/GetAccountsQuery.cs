using MediatR;

namespace ShadowBuddy.Handlers.Queries;

public class GetAccountsQuery : IRequest<GetAccountsQueryResult>
{
    public long UserId { get; set; }

    public GetAccountsQuery(long userId)
    {
        UserId = userId;
    }
}