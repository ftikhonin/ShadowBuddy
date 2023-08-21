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
        GetOperationsResponse result = new GetOperationsResponse() { };
        
        //TODO: returns correct response
        foreach (var row in response.Operations)
        {
            result.Operations.Add(new Operation
            {
                Id = 0,
                OperationType = OperationType.Income,
                Amount = (float) row.Amount,
                CategoryId = 0,
                Comment = row.Comment ?? "",
                Moment = null
            });
        }


        return result;
    }
}