using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using MediatR;
using ShadowPal.Handlers;
using ShadowPal.Service.Grpc;

namespace ShadowPal.Services;

public class AccountProcessingGrpcService : AccountProcessingService.AccountProcessingServiceBase
{
    private readonly IMediator _mediator;

    public AccountProcessingGrpcService(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    public override async Task<GetOperationsResponse> GetOperations(GetOperationsRequest request,
        ServerCallContext context)
    {
        var command = new GetOperationsQuery(request.UserId, request.Moment);
        var response = await _mediator.Send(command, context.CancellationToken);

        GetOperationsResponse result = new GetOperationsResponse();

        foreach (var row in response.Operations)
        {
            result.Operations.Add(new Operation
            {
                Id = row.Id,
                AccountId = row.AccountId,
                OperationTypeId = row.OperationTypeId,
                CurrencyId = row.CurrencyId,
                Amount = (float) row.Amount,
                CategoryId = row.CategoryId,
                Comment = row.Comment ?? "",
                Moment = row.Moment.ToUniversalTime().ToTimestamp()
            });
        }

        return result;
    }

    public override async Task<Empty> CreateAccount(CreateAccountRequest request,
        ServerCallContext context)
    {
        var command = new CreateAccountCommand(request.UserId, request.Name, request.Balance, request.InitialDate,
            request.CurrencyId);

        await _mediator.Send(command, context.CancellationToken);

        return new Empty();
    }
}