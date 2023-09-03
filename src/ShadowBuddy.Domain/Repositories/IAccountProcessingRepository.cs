using ShadowBuddy.Domain.Entities;

namespace ShadowBuddy.Domain.Repositories;

public interface IAccountProcessingRepository
{
    Task<Operation[]> GetOperations(long accountId, DateTime moment, CancellationToken cancellationToken);
    Task<Account[]> GetAccounts(long userId, CancellationToken cancellationToken);
    Task<double> GetAccountBalance(long accountId);
    Task<Category[]> GetCategories();
    Task<Currency[]> GetCurrencies();

    Task UpdateOperation(
        long accountId,
        long operationId,
        long operationTypeId,
        double amount,
        long categoryId,
        string comment,
        DateTime moment,
        CancellationToken cancellationToken);

    Task<Operation> GetOperation(long operationId, CancellationToken cancellationToken);

    Task<long> CreateAccount(
        long userId,
        string name,
        double balance,
        DateTime initialDate,
        long currencyId,
        CancellationToken cancellationToken);

    Task UpdateAccount(
        long accountId,
        string name,
        DateTime initialDate,
        long currencyId,
        CancellationToken cancellationToken);

    Task DeleteAccount(
        long accountId,
        CancellationToken cancellationToken);

    Task CreateOperation(
        long accountId,
        long operationTypeId,
        double amount,
        long categoryId,
        string comment,
        DateTime moment,
        CancellationToken cancellationToken);

    Task DeleteOperation(
        long operationId,
        long accountId,
        CancellationToken cancellationToken);
}