using MediatR;

namespace ShadowBuddy.Handlers.Commands;

public class UpdateOperationCommand : IRequest
{
    public long OperationId { get; }
    public long OperationTypeId { get; }
    public double Amount { get; }
    public long CategoryId { get; }
    public string Comment { get; }
    public DateTime Moment { get; }

    public UpdateOperationCommand(
        long operationId,
        long operationTypeId,
        double amount,
        long categoryId,
        string comment,
        DateTime moment)
    {
        OperationId = operationId;
        OperationTypeId = operationTypeId;
        Amount = amount;
        CategoryId = categoryId;
        Comment = comment;
        Moment = moment;
    }
}