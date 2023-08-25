using Google.Protobuf.WellKnownTypes;
using MediatR;

namespace ShadowBuddy.Handlers;

public class GetOperationsQuery : IRequest<GetOperationsQueryResult>
{
    public long UserId { get; set; }
    public DateTime Moment { get; set; }

    public GetOperationsQuery(long userId, Timestamp moment)
    {
        UserId = userId;
        Moment = moment.ToDateTime();
    }
}