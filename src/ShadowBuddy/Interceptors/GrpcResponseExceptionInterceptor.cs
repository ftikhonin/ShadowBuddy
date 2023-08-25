using Grpc.Core;
using Grpc.Core.Interceptors;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Logging;
using ShadowBuddy.Infrastructure.Exceptions;

namespace ShadowBuddy.Interceptors;

public class GrpcResponseExceptionInterceptor : Interceptor
{
    private readonly ILogger<GrpcResponseExceptionInterceptor> _logger;

    public GrpcResponseExceptionInterceptor(ILogger<GrpcResponseExceptionInterceptor> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(TRequest request,
        ServerCallContext context, UnaryServerMethod<TRequest, TResponse> continuation)
    {
        try
        {
            return await continuation(request, context);
        }
        catch (SqliteException e)
        {
            throw new RpcException(new Status(StatusCode.Internal, e.Message, e));
        }
        catch (NotFoundException e)
        {
            throw new RpcException(new Status(StatusCode.NotFound, e.Message, e));
        }
    }
}