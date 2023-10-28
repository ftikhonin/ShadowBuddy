using System.Data;
using Dapper;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using ShadowBuddy.Domain.Entities;
using ShadowBuddy.Domain.Enums;
using ShadowBuddy.Domain.Repositories;
using Category = ShadowBuddy.Domain.Entities.Category;
using Currency = ShadowBuddy.Domain.Entities.Currency;

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
        return new SqliteConnection(Configuration.GetConnectionString("ReadWriteConnection"));
    }
    
    public IDbConnection CreateReadOnlyConnection()
    {
        return new SqliteConnection(Configuration.GetConnectionString("ReadOnlyConnection"));
    }
    
    public async Task<Operation[]> GetOperations(long accountId, DateTime moment)
    {
        const string query =
            @"SELECT ID, AccountId, OperationTypeId, Amount, CategoryId, Comment, substr(Moment, 1, 19) AS Moment
					             FROM Operation
					            WHERE AccountId = @Id AND date(Moment) = date(@Moment)";

        var param = new DynamicParameters(
            new
            {
                Id = accountId,
                Moment = moment
            }
        );

        using var connection = CreateReadOnlyConnection();

        var result = await connection.QueryAsync<Operation>(query, param, commandType: CommandType.Text);

        return result.ToArray();
    }

    public async Task<Category[]> GetCategories()
    {
        const string query = @"SELECT ID, Name, ShortName, Label FROM Category";

        using var connection = CreateReadOnlyConnection();

        var result = await connection.QueryAsync<Category>(query, null, commandType: CommandType.Text);

        return result.ToArray();
    }

    public async Task<Currency[]> GetCurrencies()
    {
        const string query = @"SELECT ID, Name, ShortName, Label FROM Currency";

        using var connection = CreateReadOnlyConnection();

        var result = await connection.QueryAsync<Currency>(query, null, commandType: CommandType.Text);

        return result.ToArray();
    }

    public async Task<Account[]> GetAccounts(long userId)
    {
        const string query = @"SELECT ID, CurrencyId, Name, substr(Moment, 1, 19) AS Moment
			                     FROM Account
			                    WHERE UserId = @UserId";

        var param = new DynamicParameters(
            new
            {
                UserId = userId,
            }
        );

        using var connection = CreateReadOnlyConnection();

        var result = await connection.QueryAsync<Account>(query, param, commandType: CommandType.Text);

        return result.ToArray();
    }

    public async Task<double> GetAccountBalance(long accountId)
    {
        const string query = @"SELECT IFNULL(SUM(o.Amount), 0) FROM Operation as o WHERE o.AccountId = @Id";

        var param = new DynamicParameters(
            new
            {
                Id = accountId
            }
        );
        using var connection = CreateReadOnlyConnection();
        return await connection.QueryFirstOrDefaultAsync<double>(query, param, commandType: CommandType.Text);
    }

    public async Task<Operation> GetOperation(long operationId)
    {
        const string query =
            @"SELECT Id, AccountId, OperationTypeId, Amount, CategoryId, Comment, Moment FROM Operation WHERE Id = @Id";

        var param = new DynamicParameters(
            new
            {
                Id = operationId
            }
        );

        using var connection = CreateReadOnlyConnection();

        return await connection.QueryFirstOrDefaultAsync<Operation>(query, param, commandType: CommandType.Text);
    }

    public async Task<long> CreateAccount(
        long userId, 
        string name, 
        double balance, 
        DateTime initialDate,
        long currencyId)
    {
        const string query = @"INSERT INTO Account(UserId, CurrencyId,  Name, Moment)
                                VALUES(@UserId, @CurrencyId, @Name, @InitialDate)
                                RETURNING ID";

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
        return await connection.QueryFirstOrDefaultAsync<long>(query, param, commandType: CommandType.Text);
    }

    public async Task UpdateAccount(long accountId, string name, DateTime initialDate, long currencyId)
    {
        const string query = @"UPDATE Account
                               SET CurrencyId = @CurrencyId,
                                   Name = @Name, 
                                   Moment = @InitialDate
                                WHERE Id = @AccountId";

        var param = new DynamicParameters(
            new
            {
                AccountId = accountId,
                CurrencyId = currencyId,
                Name = name,
                InitialDate = initialDate
            }
        );

        using var connection = CreateConnection();
        await connection.ExecuteAsync(query, param, commandType: CommandType.Text);
    }

    public async Task DeleteAccount(long accountId)
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
        OperationType operationType,
        double amount,
        Domain.Enums.Category category,
        string comment,
        DateTime moment)
    {
        const string query = @"INSERT INTO Operation(AccountId, OperationTypeId, Amount, CategoryId, Comment, Moment)
                                VALUES(@AccountId, @OperationTypeId, @Amount, @CategoryId, @Comment, @Moment)";

        var param = new DynamicParameters(
            new
            {
                AccountId = accountId,
                OperationTypeId = operationType,
                Amount = amount,
                CategoryId = category,
                Comment = comment,
                Moment = moment
            }
        );

        using var connection = CreateConnection();
        await connection.ExecuteAsync(query, param, commandType: CommandType.Text);
    }

    public async Task UpdateOperation(
        long operationId,
        long operationTypeId,
        double amount,
        long categoryId,
        string comment,
        DateTime moment)
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
    }

    public async Task DeleteOperation(
        long operationId,
        long accountId)
    {
        var param = new DynamicParameters(
            new
            {
                OperationId = operationId,
                AccountId = accountId
            }
        );

        using var connection = CreateConnection();

        await connection.ExecuteAsync(
            @"DELETE FROM Operation WHERE Id = @OperationId",
            param,
            commandType: CommandType.Text);
    }
}