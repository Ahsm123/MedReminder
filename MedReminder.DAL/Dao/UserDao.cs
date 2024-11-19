using Dapper;
using MedReminder.Dal.Interfaces;
using MedReminder.DAL.Models;
using Microsoft.Data.SqlClient;

namespace MedReminder.Dal.Dao;

public class UserDao : IUserDao
{
	private const string GetByIdSql = @" SELECT Id, FirstName, LastName, Email, PasswordHash, CreatedAt FROM Users WHERE Id = @Id";
	private const string CreateSql = "INSERT INTO Users (FirstName, LastName, Email, PasswordHash) VALUES (@FirstName, @LastName, @Email, @PasswordHash) SELECT SCOPE_IDENTITY()";

	private readonly string _connectionString;

	public UserDao(string connectionString)
	{
		_connectionString = connectionString;
	}

	public async Task<User> GetByIdAsync(int id)
	{
		if (id <= 0)
		{
			throw new ArgumentException("Invalid id", nameof(id));
		}

		try
		{
			using var connection = new SqlConnection(_connectionString);
			await connection.OpenAsync();

			var user = await connection.QueryFirstOrDefaultAsync<User>(GetByIdSql, new { Id = id });
			if (user == null)
			{
				throw new KeyNotFoundException($"User with id {id} not found");
			}
			return user;
		}
		catch (SqlException ex)
		{
			throw new Exception("An error occurred while getting user by id", ex);
		}
	}

	public async Task<int> CreateAsync(User user)
	{

		if (user == null)
		{
			throw new ArgumentNullException(nameof(user), "User cannot be null");
		}

		try
		{
			using var connection = new SqlConnection(_connectionString);
			await connection.OpenAsync();

			var id = await connection.ExecuteScalarAsync<int>(CreateSql, user);

			return id;
		}
		catch (SqlException ex)
		{
			throw new Exception("An error occurred while creating the user.", ex);
		}

	}
}
