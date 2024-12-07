using Dapper;
using MedReminder.Dal.Interfaces;
using MedReminder.DAL.Models;
using Microsoft.Data.SqlClient;

namespace MedReminder.Dal.Dao;

public class UserDao : IUserDao
{
    private const string GetByIdSql = @"SELECT Id, FirstName, LastName, Email, PhoneNumber, PasswordHash, CreatedAt FROM Users WHERE Id = @Id";
    private const string GetUserByEmailSql = @"SELECT * FROM Users WHERE Email = @Email";
    private const string CreateUserSql = @"
        INSERT INTO Users (FirstName, LastName, Email, PhoneNumber, PasswordHash, CreatedAt) 
        VALUES (@FirstName, @LastName, @Email, @PasswordHash, @CreatedAt) 
        SELECT SCOPE_IDENTITY()";
    private const string GetUserByRefreshTokenSql = @"SELECT * FROM Users WHERE RefreshToken = @RefreshToken";
    private const string UpdateRefreshTokenSql = @"
        UPDATE Users 
        SET RefreshToken = @RefreshToken, RefreshTokenExpiry = @RefreshTokenExpiry 
        WHERE Id = @Id";
    private const string UpdateUserSql = @"
        UPDATE Users 
        SET FirstName = @FirstName, LastName = @LastName, Email = @Email, PhoneNumber = @PhoneNumber 
        WHERE Id = @Id";

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
        if (string.IsNullOrWhiteSpace(email))
        {
            throw new ArgumentNullException(nameof(email), "Email cannot be null or empty");
        }

        try
        {
            using var connection = _connectionFactory.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<User>(GetUserByEmailSql, new { Email = email });
        }
        catch (SqlException ex)
        {
            throw new DataAccessException("An error occurred while getting user by email", ex);
        }
    }

    public async Task<User> GetUserByRefreshTokenAsync(string refreshToken)
    {
        if (string.IsNullOrWhiteSpace(refreshToken))
        {
            throw new ArgumentNullException(nameof(refreshToken), "Refresh token cannot be null or empty");
        }

        try
        {
            using var connection = _connectionFactory.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<User>(GetUserByRefreshTokenSql, new { RefreshToken = refreshToken });
        }
        catch (SqlException ex)
        {
            throw new DataAccessException("An error occurred while getting user by refresh token", ex);
        }
    }

    public async Task UpdateRefreshTokenAsync(int userId, string refreshToken, DateTime refreshTokenExpiry)
    {
        if (userId <= 0)
        {
            throw new ArgumentException("Invalid user ID", nameof(userId));
        }

        if (string.IsNullOrWhiteSpace(refreshToken))
        {
            throw new ArgumentNullException(nameof(refreshToken), "Refresh token cannot be null or empty");
        }

        try
        {
            using var connection = _connectionFactory.CreateConnection();
            await connection.ExecuteAsync(UpdateRefreshTokenSql, new
            {
                Id = userId,
                RefreshToken = refreshToken,
                RefreshTokenExpiry = refreshTokenExpiry
            });
        }
        catch (SqlException ex)
        {
            throw new DataAccessException("An error occurred while updating the refresh token", ex);
        }
    }

    public async Task<bool> UpdateUserAsync(User user)
    {
        try
        {
            using var connection = _connectionFactory.CreateConnection();
            return await connection.ExecuteAsync(UpdateUserSql, user) > 0;
        }
        catch (Exception ex)
        {
            throw new DataAccessException($"An error occurred while updating medication with id {user.Id}: '{ex.Message}'.", ex);
        }
    }

}

