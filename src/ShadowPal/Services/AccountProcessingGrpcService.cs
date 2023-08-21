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

        //TODO: returns correct response
        return new GetOperationsResponse
        {
            Operations =
            {
                new Operation
                {
                    Id = 0,
                    OperationType = OperationType.Expense,
                    Amount = 100,
                    CategoryId = 0,
                    Comment = 0,
                    Moment = null
                }
            }
        };
    }
}