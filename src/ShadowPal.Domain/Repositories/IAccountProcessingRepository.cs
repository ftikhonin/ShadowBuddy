using ShadowPal.Domain.Entities;

namespace ShadowPal.Domain.Repositories;

public interface IAccountProcessingRepository
{
    Task<Operation[]> GetOperations(long userId, DateTime moment, CancellationToken cancellationToken);

    Task CreateAccount(long userId, string name, float balance, DateTime initialDate, long currencyId,
        CancellationToken cancellationToken);
}