using System.Data;
using Dapper;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using ShadowPal.Domain.Entities;
using ShadowPal.Domain.Repositories;

namespace ShadowPal.Infrastructure.Repositories;

public class AccountProcessingRepository : IAccountProcessingRepository
{
    protected readonly IConfiguration Configuration;

    public AccountProcessingRepository(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IDbConnection CreateConnection()
    {
        return new SqliteConnection(Configuration.GetConnectionString("SQLiteConnection"));
    }

    public async Task<Operation[]> GetOperations(long userId, DateTime moment, CancellationToken cancellationToken)
    {
        const string query = @"SELECT o.ID
                                     ,o.AccountId
                                     ,o.OperationTypeId
                                     ,o.CurrencyId
						             ,o.Amount
                                     ,o.CategoryId
                                     ,o.Comment
						             ,substr(o.Moment,1,19) AS Moment
					     FROM Operation as o
					     WHERE o.AccountId = @Id 
							   AND substr(o.Moment,1,4) || substr(o.Moment,6,2) || substr(o.Moment,9,2) >= @Moment";

        var param = new DynamicParameters(
            new
            {
                Id = userId,
                Moment = moment
            }
        );

        using var connection = CreateConnection();

        var result = await connection.QueryAsync<Operation>(query, param, commandType: CommandType.Text);

        return result.ToArray();
    }

    public async Task CreateAccount(long userId, string name, float balance, DateTime initialDate, long currencyId,
        CancellationToken cancellationToken)
    {
        const string query = @"INSERT INTO Account(UserId, CurrencyId, Balance, Name, Moment)
                                VALUES(@UserId, @CurrencyId, @Balance, @Name, @InitialDate)";

        var param = new DynamicParameters(
            new
            {
                UserId = userId,
                CurrencyId = currencyId,
                Balance = balance,
                Name = name,
                InitialDate = initialDate
            }
        );

        using var connection = CreateConnection();
        await connection.ExecuteAsync(query, param, commandType: CommandType.Text);
    }

    public async Task DeleteAccount(
        long accountId,
        CancellationToken cancellationToken)
    {
        const string query = @"DELETE FROM Account
                                WHERE Id = @AccountId";

        var param = new DynamicParameters(
            new
            {
                AccountId = accountId
            }
        );

        using var connection = CreateConnection();
        await connection.ExecuteAsync(query, param, commandType: CommandType.Text);
    }

    public async Task CreateOperation(
        long accountId,
        long operationTypeId,
        float amount,
        long categoryId,
        string comment,
        DateTime moment,
        CancellationToken cancellationToken)
    {
        const string query = @"INSERT INTO Operation(AccountId, OperationTypeId, Amount, CategoryId, Comment, Moment)
                                VALUES(@AccountId, @OperationTypeId, @Amount, @CategoryId, @Comment, @Moment)";

        var param = new DynamicParameters(
            new
            {
                AccountId = accountId,
                OperationTypeId = operationTypeId,
                Amount = amount,
                CategoryId = categoryId,
                Comment = comment,
                Moment = moment
            }
        );

        using var connection = CreateConnection();
        await connection.ExecuteAsync(query, param, commandType: CommandType.Text);
    }

    public async Task DeleteOperation(
        long operationId,
        CancellationToken cancellationToken)
    {
        const string query = @"DELETE FROM Operation
                                WHERE Id = @OperationId";

        var param = new DynamicParameters(
            new
            {
                OperationId = operationId
            }
        );

        using var connection = CreateConnection();
        await connection.ExecuteAsync(query, param, commandType: CommandType.Text);
    }
}