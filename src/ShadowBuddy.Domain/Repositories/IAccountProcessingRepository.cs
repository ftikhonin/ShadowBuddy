using System.Data;
using ShadowBuddy.Domain.Entities;

namespace ShadowBuddy.Domain.Repositories;

public interface IAccountProcessingRepository
{
    Task<Operation[]> GetOperations(long accountId, DateTime moment, CancellationToken cancellationToken);

    Task<float> GetAccountBalance(long accountId, CancellationToken cancellationToken);

    Task<Operation> GetOperation(long operationId, CancellationToken cancellationToken);

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
        long accountId,
        long operationId,
        long operationTypeId,
        float amount,
        long categoryId,
        string comment,
        DateTime moment,
        CancellationToken cancellationToken);

    Task UpdateAccountBalance(
        long accountId,
        IDbConnection connection);

    Task DeleteOperation(
        long operationId,
        long accountId,
        CancellationToken cancellationToken);
}