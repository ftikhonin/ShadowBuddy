using ShadowPal.Domain.Enums;

namespace ShadowPal.Domain.Entities;

public class Operation
{
    public OperationType Type { get; set; }
    public decimal Amount { get; set; }
    public DateTime Moment { get; set; }
    public string Comment { get; set; }
    public Currency Currency { get; set; }
}