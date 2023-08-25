namespace ShadowBuddy.Domain.Entities;

public class Operation
{
    public long Id { get; set; }
    public long AccountId { get; set; }
    public long OperationTypeId { get; set; }
    public long CurrencyId { get; set; }
    public decimal Amount { get; set; }
    public long CategoryId { get; set; }
    public DateTime Moment { get; set; }
    public string Comment { get; set; }
}