using MediatR;

namespace ShadowBuddy.Handlers;

public class CreateOperationCommand : IRequest
{
    public long AccountId { get; set; }
    public long OperationTypeId { get; set; }
    public float Amount { get; set; }
    public long CategoryId { get; set; }
    public string Comment { get; set; }
    public DateTime Moment { get; set; }

    public CreateOperationCommand(long accountId,
        long operationTypeId,
        float amount,
        long categoryId,
        string comment,
        DateTime moment)
    {
        AccountId = accountId;
        OperationTypeId = operationTypeId;
        Amount = amount;
        CategoryId = categoryId;
        Comment = comment;
        Moment = moment;
    }
}