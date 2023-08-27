using MediatR;

namespace ShadowBuddy.Handlers.Commands;

public class DeleteAccountCommand : IRequest
{
    public long AccountId { get; set; }

    public DeleteAccountCommand(long accountId)
    {
        AccountId = accountId;
    }
}