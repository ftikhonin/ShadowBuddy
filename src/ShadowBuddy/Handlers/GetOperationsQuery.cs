using Google.Protobuf.WellKnownTypes;
using MediatR;

namespace ShadowBuddy.Handlers;

public class GetOperationsQuery : IRequest<GetOperationsQueryResult>
{
    public long AccountId { get; set; }
    public DateTime Moment { get; set; }

    public GetOperationsQuery(long accountId, Timestamp moment)
    {
        AccountId = accountId;
        Moment = moment.ToDateTime();
    }
}