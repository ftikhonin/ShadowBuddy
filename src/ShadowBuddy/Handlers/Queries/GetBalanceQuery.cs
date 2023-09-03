using MediatR;

namespace ShadowBuddy.Handlers.Queries;

public class GetBalanceQuery : IRequest<GetBalanceQueryResult>
{
    public long UserId { get; set; }

    public GetBalanceQuery(long userId)
    {
        UserId = userId;
    }
}