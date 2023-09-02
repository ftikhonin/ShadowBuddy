namespace ShadowBuddy.Domain.Entities;

public class Account
{
    public long Id { get; set; }
    public long CurrencyId { get; set; }
    public string Name { get; set; }
    public DateTime Moment { get; set; }
    public double Balance { get; set; }
}