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

    public async Task<Operation[]> GetOperations(long userId, DateTime moment, CancellationToken cancellationToken)
    {
        const string query = @"SELECT o.ID
						     ,o.Amount
						     ,substr(o.Moment,1,19) AS Moment
					     FROM Operation as o
					     WHERE o.ChatID = @Id 
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
}