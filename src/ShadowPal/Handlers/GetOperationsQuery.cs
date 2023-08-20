using MediatR;

namespace ShadowPal.Handlers;

public class GetOperationsQuery : IRequest<GetOperationsQueryResult>
{
    public GetOperationsQuery()
    {
    }
}