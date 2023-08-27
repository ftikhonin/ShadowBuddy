using MediatR;

namespace ShadowBuddy.Handlers.Commands;

public class DeleteOperationCommand : IRequest
{
    public long OperationId { get; set; }

    public DeleteOperationCommand(long operationId)
    {
        OperationId = operationId;
    }
}