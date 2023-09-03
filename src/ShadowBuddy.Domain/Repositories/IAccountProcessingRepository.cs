using ShadowBuddy.Domain.Entities;

namespace ShadowBuddy.Domain.Repositories;

public interface IAccountProcessingRepository
{
    Task<Operation[]> GetOperations(long accountId, DateTime moment);
    Task<Account[]> GetAccounts(long userId);
    Task<double> GetAccountBalance(long accountId);
    Task<Category[]> GetCategories();
    Task<Currency[]> GetCurrencies();

    Task UpdateOperation(
        long operationId,
        long operationTypeId,
        double amount,
        long categoryId,
        string comment,
        DateTime moment);

    Task<Operation> GetOperation(long operationId);

    Task<long> CreateAccount(
        long userId,
        string name,
        double balance,
        DateTime initialDate,
        long currencyId);

    Task UpdateAccount(
        long accountId,
        string name,
        DateTime initialDate,
        long currencyId);

    Task DeleteAccount(
        long accountId);

    Task CreateOperation(
        long accountId,
        long operationTypeId,
        double amount,
        long categoryId,
        string comment,
        DateTime moment);

    Task DeleteOperation(
        long operationId,
        long accountId);
}