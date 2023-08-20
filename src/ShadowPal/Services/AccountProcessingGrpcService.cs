using Google.Protobuf.WellKnownTypes;
using ShadowPal.Services;
using Grpc.Core;
using MediatR;
using ShadowPal.Domain.Entities;
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
        var command = new GetOperationsQuery();
        var response = await _mediator.Send(command, context.CancellationToken);
        //TODO: returns correct response
        return new GetOperationsResponse
        {
            Id = 1,
            OperationType = OperationType.Expense,
            //TODO: float -> Decimal
            Amount = 1,
            CategoryId = 0,
            Comment = 0,
            Moment = new Timestamp()
        };
    }
}