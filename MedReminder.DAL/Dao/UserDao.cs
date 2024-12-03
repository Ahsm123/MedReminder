using Dapper;
using MedReminder.Dal.Interfaces;
using MedReminder.DAL.Models;
using Microsoft.Data.SqlClient;

namespace MedReminder.Dal.Dao;

public class UserDao : IUserDao
{
    private const string GetByIdSql = @" SELECT Id, FirstName, LastName, Email, PasswordHash, CreatedAt FROM Users WHERE Id = @Id";
    private const string GetUserByEmailSql = @" SELECT * FROM Users WHERE Email = @Email";
    private const string CreateUserSql = @"INSERT INTO Users (FirstName, LastName, Email, PasswordHash, CreatedAt) VALUES (@FirstName, @LastName, @Email, @PasswordHash, @CreatedAt) SELECT SCOPE_IDENTITY()";

    private readonly IConnectionFactory _connectionFactory;

    public UserDao(IConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<User> GetByIdAsync(int id)
    {
        if (id <= 0)
        {
            throw new ArgumentException("Invalid id", nameof(id));
        }

        try
        {
            using var connection = _connectionFactory.CreateConnection();

            var user = await connection.QueryFirstOrDefaultAsync<User>(GetByIdSql, new { Id = id });
            if (user == null)
            {
                throw new KeyNotFoundException($"User with id {id} not found");
            }
            return user;
        }
        catch (SqlException ex)
        {
            throw new DataAccessException("An error occurred while getting user by id", ex);
        }
    }

    public async Task<int> CreateUserAsync(User user)
    {

        if (user == null)
        {
            throw new ArgumentNullException(nameof(user), "User cannot be null");
        }

        try
        {
            using var connection = _connectionFactory.CreateConnection();

            var id = await connection.ExecuteScalarAsync<int>(CreateUserSql, user);

            return id;
        }
        catch (SqlException ex)
        {
            throw new DataAccessException("An error occurred while creating the user.", ex);
        }

    }

    public async Task<User> GetUserByEmailAsync(string email)
    {
        if (email == null)
        {
            throw new ArgumentNullException(nameof(email), "Email cannot be null");
        }

        try
        {
            using var connection = _connectionFactory.CreateConnection();

            var user = await connection.QueryFirstOrDefaultAsync<User>(GetUserByEmailSql, new { Email = email });
            return user;
        }
        catch (SqlException ex)
        {
            throw new DataAccessException("An error occurred while getting user by email", ex);
        }
    }
}
