using System.Data;
using MoodTracker.ConnectionFactory.Interfaces;
using Npgsql;

namespace MoodTracker.ConnectionFactory;

public class NpgsqlConnectionFactory : IDbConnectionFactory
{
    private readonly NpgsqlDataSource _connectionString;

    public NpgsqlConnectionFactory(NpgsqlDataSource connectionString)
    {
        _connectionString = connectionString;
    }

    public IDbConnection Create() => _connectionString.CreateConnection();
}