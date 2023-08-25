using Google.Protobuf.WellKnownTypes;
using MediatR;

namespace ShadowPal.Handlers;

public class DeleteAccountCommand : IRequest
{
    public long AccountId { get; set; }

    public DeleteAccountCommand(long accountId)
    {
        AccountId = accountId;
    }
}