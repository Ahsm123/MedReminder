using Dapper;
using MedReminder.Dal.Interfaces;
using MedReminder.DAL.Models;
using Microsoft.Data.SqlClient;

namespace MedReminder.Dal.Dao;

public class MedicationDao : IMedicationDao
{
    private const string GetByIdSql = "SELECT * FROM Medications WHERE UserId = @UserId";
    private const string CreateSql = @"
            INSERT INTO Medications (UserId, Name, Dosage, DosageUnit, Instructions, Description, StartDate, EndDate)
            VALUES (@UserId, @Name, @Dosage, @DosageUnit, @Instructions, @Description, @StartDate, @EndDate);
            SELECT CAST(SCOPE_IDENTITY() as int)";
    private const string GetMedicationsByUserIdSql = "SELECT * FROM Medications WHERE UserId = @UserId";


    private readonly string _connectionString;

    public MedicationDao(string connectionString)
    {
        _connectionString = connectionString;
    }
    public async Task<Medication> GetByIdAsync(int id)
    {
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

    public async Task<List<Medication>> GetMedicationsByUserIdAsync(int userId)
    {
        try
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            var medications = await connection.QueryAsync<Medication>(GetMedicationsByUserIdSql, new { UserId = userId });

            return medications.ToList();
        }
        catch (SqlException ex)
        {
            throw new Exception("An error occurred while getting medications by user id", ex);
        }
    }

    public async Task<int> CreateAsync(Medication medication)
    {
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
