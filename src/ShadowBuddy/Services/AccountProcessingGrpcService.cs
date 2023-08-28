using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using MediatR;
using ShadowBuddy.Handlers.Commands;
using ShadowBuddy.Handlers.Queries;
using ShadowBuddy.Service.Grpc;

namespace ShadowBuddy.Services;

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
        var command = new GetOperationsQuery(request.AccountId, request.Moment);
        var response = await _mediator.Send(command, context.CancellationToken);

        GetOperationsResponse result = new GetOperationsResponse();

        foreach (var row in response.Operations)
        {
            result.Operations.Add(new Operation
            {
                Id = row.Id,
                AccountId = row.AccountId,
                OperationTypeId = row.OperationTypeId,
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
        var command = new CreateAccountCommand(request.UserId, request.Name, request.Balance,
            request.InitialDate.ToDateTime(),
            request.CurrencyId);

        await _mediator.Send(command, context.CancellationToken);

        return new Empty();
    }

    public override async Task<Empty> UpdateAccount(UpdateAccountRequest request,
        ServerCallContext context)
    {
        var command = new UpdateAccountCommand(request.AccountId, request.Name, request.Balance,
            request.InitialDate.ToDateTime(),
            request.CurrencyId);

        await _mediator.Send(command, context.CancellationToken);

        return new Empty();
    }

    public override async Task<Empty> DeleteAccount(DeleteAccountRequest request,
        ServerCallContext context)
    {
        var command = new DeleteAccountCommand(request.AccountId);

        await _mediator.Send(command, context.CancellationToken);

        return new Empty();
    }

    public override async Task<Empty> CreateOperation(CreateOperationRequest request,
        ServerCallContext context)
    {
        var command = new CreateOperationCommand(request.AccountId, request.OperationTypeId, request.Amount,
            request.CategoryId,
            request.Comment, request.Moment.ToDateTime());

        await _mediator.Send(command, context.CancellationToken);

        return new Empty();
    }

    public override async Task<Empty> UpdateOperation(UpdateOperationRequest request,
        ServerCallContext context)
    {
        var command = new UpdateOperationCommand( request.OperationId, request.OperationTypeId,
            request.Amount,
            request.CategoryId,
            request.Comment, request.Moment.ToDateTime());

        await _mediator.Send(command, context.CancellationToken);

        return new Empty();
    }

    public override async Task<Empty> DeleteOperation(DeleteOperationRequest request,
        ServerCallContext context)
    {
        var command = new DeleteOperationCommand(request.OperationId);

        await _mediator.Send(command, context.CancellationToken);

        return new Empty();
    }
}