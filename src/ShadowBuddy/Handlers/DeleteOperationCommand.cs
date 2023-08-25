using Google.Protobuf.WellKnownTypes;
using MediatR;

namespace ShadowBuddy.Handlers;

public class DeleteOperationCommand : IRequest
{
    public long OperationId { get; set; }

    public DeleteOperationCommand(long operationId)
    {
        OperationId = operationId;
    }
}