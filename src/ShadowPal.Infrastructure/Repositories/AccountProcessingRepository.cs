using System.Data;
using System.Reflection;
using Dapper;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using ShadowPal.Domain.Entities;
using ShadowPal.Domain.Enums;
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

    public async Task<Operation[]> GetOperations(CancellationToken cancellationToken)
    {
        var query = @"SELECT ID
						     ,Amount
						     ,substr(Moment,1,19) AS Moment
					     FROM Operation";
        using var connection = CreateConnection();
        
        var result = await connection.QueryAsync<Operation>(query);

        return result.ToArray();
    }
}