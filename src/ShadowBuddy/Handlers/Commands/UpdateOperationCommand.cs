using MediatR;

namespace ShadowBuddy.Handlers.Commands;

public class UpdateOperationCommand : IRequest
{
    public long AccountId { get; }
    public long OperationId { get; }
    public long OperationTypeId { get; }
    public float Amount { get; }
    public long CategoryId { get; }
    public string Comment { get; }
    public DateTime Moment { get; }

    public UpdateOperationCommand(
        long accountId,
        long operationId,
        long operationTypeId,
        float amount,
        long categoryId,
        string comment,
        DateTime moment)
    {
        AccountId = accountId;
        OperationId = operationId;
        OperationTypeId = operationTypeId;
        Amount = amount;
        CategoryId = categoryId;
        Comment = comment;
        Moment = moment;
    }
}