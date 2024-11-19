using Dapper;
using MedReminder.Dal.Interfaces;
using MedReminder.DAL.Models;
using Microsoft.Data.SqlClient;

namespace MedReminder.Dal.Dao;

public class MedicationDao : IMedicationDao
{
	private const string GetByIdSql = "SELECT * FROM Medications WHERE Id = @Id";
	private const string CreateSql = "INSERT INTO Medications (Name, Dosage, Instructions, Description) VALUES (@Name, @Dosage, @Instructions, @Description) SELECT SCOPE_IDENTITY()";

	private readonly string _connectionString;

	public MedicationDao(string connectionString)
	{
		_connectionString = connectionString;
	}
	public async Task<Medication> GetByIdAsync(int id)
	{

		if (id <= 0)
		{
			throw new ArgumentException("Id must be greater than 0", nameof(id));
		}

		try
		{
			using var connection = new SqlConnection(_connectionString);
			await connection.OpenAsync();

			var medication = await connection.QueryFirstOrDefaultAsync<Medication>(GetByIdSql, new { Id = id });

			if (medication == null)
			{
				throw new KeyNotFoundException($"Medication with id {id} not found");
			}

			return medication;
		}
		catch (SqlException ex)
		{
			throw new Exception("An error occurred while getting medication by id", ex);
		}
	}

	public async Task<int> CreateAsync(Medication medication)
	{

		if (medication == null)
		{
			throw new ArgumentNullException(nameof(medication), "Medication cannot be null");
		}

		try
		{
			using var connection = new SqlConnection(_connectionString);
			await connection.OpenAsync();

			var id = await connection.ExecuteScalarAsync<int>(CreateSql, medication);

			return id;

		}
		catch (SqlException ex)
		{
			throw new Exception("An error occurred while creating medication", ex);
		}

	}
}
