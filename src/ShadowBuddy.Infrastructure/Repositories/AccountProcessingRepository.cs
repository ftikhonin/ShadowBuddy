using System.Data;
using Dapper;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using ShadowBuddy.Domain.Entities;
using ShadowBuddy.Domain.Repositories;

namespace ShadowBuddy.Infrastructure.Repositories;

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

    public async Task<Operation[]> GetOperations(long accountId, DateTime moment, CancellationToken cancellationToken)
    {
        const string query =
            @"SELECT ID, AccountId, OperationTypeId, Amount, CategoryId, Comment, substr(Moment, 1, 19) AS Moment
					             FROM Operation
					            WHERE AccountId = @Id AND substr(Moment,1,4) || substr(Moment,6,2) || substr(Moment,9,2) >= @Moment";

        var param = new DynamicParameters(
            new
            {
                Id = accountId,
                Moment = moment
            }
        );

        using var connection = CreateConnection();

        var result = await connection.QueryAsync<Operation>(query, param, commandType: CommandType.Text);

        return result.ToArray();
    }

    public async Task<float> GetAccountBalance(long accountId, IDbConnection connection)
    {
        const string query = @"SELECT IFNULL(SUM(o.Amount), 0) FROM Operation as o WHERE o.AccountId = @Id";

        var param = new DynamicParameters(
            new
            {
                Id = accountId
            }
        );

        return await connection.QueryFirstOrDefaultAsync<float>(query, param, commandType: CommandType.Text);
    }

    public async Task<Operation> GetOperation(long operationId, CancellationToken cancellationToken)
    {
        const string query =
            @"SELECT Id, AccountId, OperationTypeId, Amount, CategoryId, Comment, Moment FROM Operation WHERE Id = @Id";

        var param = new DynamicParameters(
            new
            {
                Id = operationId
            }
        );

        using var connection = CreateConnection();

        return await connection.QueryFirstOrDefaultAsync<Operation>(query, param, commandType: CommandType.Text);
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

    public async Task UpdateAccount(long accountId, string name, float balance, DateTime initialDate, long currencyId,
        CancellationToken cancellationToken)
    {
        const string query = @"UPDATE Account
                               SET CurrencyId = @CurrencyId, 
                                   Balance = @Balance, 
                                   Name = @Name, 
                                   Moment = @InitialDate
                                WHERE Id = @AccountId";

        var param = new DynamicParameters(
            new
            {
                AccountId = accountId,
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
        const string query = @"DELETE FROM Account WHERE Id = @AccountId";

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

    public async Task UpdateOperation(
        long accountId,
        long operationId,
        long operationTypeId,
        float amount,
        long categoryId,
        string comment,
        DateTime moment,
        CancellationToken cancellationToken)
    {
        var param = new DynamicParameters(
            new
            {
                OperationId = operationId,
                OperationTypeId = operationTypeId,
                Amount = amount,
                CategoryId = categoryId,
                Comment = comment,
                Moment = moment
            }
        );

        using var connection = CreateConnection();
        connection.Open();

        using var trans = connection.BeginTransaction();

        try
        {
            await connection.ExecuteAsync(
                @"UPDATE Operation
                        SET OperationTypeId = @OperationTypeId,
                            Amount = @Amount, 
                            CategoryId = @CategoryId, 
                            Comment = @Comment, 
                            Moment = @Moment
                      WHERE ID = @OperationId",
                param,
                commandType: CommandType.Text);

            await UpdateAccountBalance(accountId, connection);

            trans.Commit();
        }
        catch (Exception)
        {
            trans.Rollback();
            throw;
        }
    }

    public async Task UpdateAccountBalance(
        long accountId,
        IDbConnection connection)
    {
        var balance = GetAccountBalance(accountId, connection).Result;

        var param = new DynamicParameters(
            new
            {
                AccountId = accountId,
                Balance = balance
            }
        );

        await connection.ExecuteAsync(
            "UPDATE Account SET Balance = @Balance WHERE ID = @AccountId",
            param,
            commandType: CommandType.Text);
    }

    public async Task DeleteOperation(
        long operationId,
        long accountId,
        CancellationToken cancellationToken)
    {
        var param = new DynamicParameters(
            new
            {
                OperationId = operationId,
                AccountId = accountId
            }
        );

        using var connection = CreateConnection();
        connection.Open();

        using var trans = connection.BeginTransaction();
        try
        {
            await connection.ExecuteAsync(
                @"DELETE FROM Operation WHERE Id = @OperationId",
                param,
                commandType: CommandType.Text);

            await UpdateAccountBalance(accountId, connection);
            trans.Commit();
        }
        catch (Exception)
        {
            trans.Rollback();
            throw;
        }
    }
}