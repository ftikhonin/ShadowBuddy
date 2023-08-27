using MediatR;

namespace ShadowBuddy.Handlers.Commands;

public class UpdateAccountCommand : IRequest
{
    public long AccountId { get; }
    public string Name { get; }
    public float Balance { get; }
    public DateTime InitialDate { get; }
    public long CurrencyId { get; }

    public UpdateAccountCommand(long accountId, string name, float balance, DateTime initialDate, long currencyId)
    {
        AccountId = accountId;
        Name = name;
        Balance = balance;
        InitialDate = initialDate;
        CurrencyId = currencyId;
    }
}