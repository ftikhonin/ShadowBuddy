using MediatR;

namespace ShadowBuddy.Handlers.Commands;

public class UpdateAccountCommand : IRequest
{
    public long AccountId { get; }
    public string Name { get; }
    public double Balance { get; }
    public DateTime InitialDate { get; }
    public long CurrencyId { get; }

    public UpdateAccountCommand(long accountId, string name, double balance, DateTime initialDate, long currencyId)
    {
        AccountId = accountId;
        Name = name;
        Balance = balance;
        InitialDate = initialDate;
        CurrencyId = currencyId;
    }
}