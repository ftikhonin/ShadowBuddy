using ShadowBuddy.Domain.Entities;

namespace ShadowBuddy.Domain.Repositories;

public interface IAccountProcessingRepository
{
    Task<Operation[]> GetOperations(long accountId, DateTime moment, CancellationToken cancellationToken);
    Task<float> GetAccountBalance(long accountId, CancellationToken cancellationToken);

    Task CreateAccount(
        long userId,
        string name,
        float balance,
        DateTime initialDate,
        long currencyId,
        CancellationToken cancellationToken);

    Task UpdateAccount(
        long accountId,
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

    Task UpdateOperation(
        long operationId,
        long operationTypeId,
        float amount,
        long categoryId,
        string comment,
        DateTime moment,
        CancellationToken cancellationToken);
    
    Task UpdateAccountBalance(
        long accountId,
        float amount,
        CancellationToken cancellationToken);

    Task DeleteOperation(
        long operationId,
        CancellationToken cancellationToken);
}