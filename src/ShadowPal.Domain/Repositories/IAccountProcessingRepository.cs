using ShadowPal.Domain.Entities;

namespace ShadowPal.Domain.Repositories;

public interface IAccountProcessingRepository
{
    Task<Operation[]> GetOperations(long userId, DateTime moment, CancellationToken cancellationToken);

    Task CreateAccount(
        long userId,
        string name,
        float balance,
        DateTime initialDate,
        long currencyId,
        CancellationToken cancellationToken);

    Task DeleteAccount(
        long accountId,
        CancellationToken cancellationToken);

    Task CreateOperation(
        long accountId,
        long operationTypeId,
        float amount,
        long categoryId,
        string comment,
        DateTime moment,
        CancellationToken cancellationToken);

    Task DeleteOperation(
        long operationId,
        CancellationToken cancellationToken);
}