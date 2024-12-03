using MedReminder.Dal.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;

namespace MedReminder.Dal.Dao;

public class ConnectionFactory : IConnectionFactory
{
    private readonly string _connectionString;

    public ConnectionFactory(string connectionString)
    {
        _connectionString = connectionString ?? throw new ArgumentException(nameof(connectionString));
    }

    public IDbConnection CreateConnection()
    {
        return new SqlConnection(_connectionString);
    }
}
