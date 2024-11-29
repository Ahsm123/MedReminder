using Dapper;
using MedReminder.Dal.Interfaces;
using MedReminder.DAL.Models;
using Microsoft.Data.SqlClient;

namespace MedReminder.Dal.Dao;

public class MedicationDao : IMedicationDao
{
    private const string GetMedicationByIdQuery = "SELECT * FROM Medications WHERE Id = @Id";
    private const string CreateMedicationQuery = @"
            INSERT INTO Medications (UserId, Name, Dosage, DosageUnit, Instructions, Description, StartDate, EndDate)
            VALUES (@UserId, @Name, @Dosage, @DosageUnit, @Instructions, @Description, @StartDate, @EndDate);
            SELECT CAST(SCOPE_IDENTITY() as int)";
    private const string GetMedicationsByUserIdQuery = "SELECT * FROM Medications WHERE UserId = @UserId";
    private const string DeleteMedicationQuery = "DELETE FROM Medications WHERE Id = @Id";
    private const string UpdateMedicationQuery = @"
            UPDATE Medications
            SET Name = @Name, Dosage = @Dosage, DosageUnit = @DosageUnit, Instructions = @Instructions, Description = @Description, StartDate = @StartDate, EndDate = @EndDate
            WHERE Id = @Id";


    private readonly string _connectionString;

    public MedicationDao(string connectionString)
    {
        _connectionString = connectionString;
    }
    public async Task<Medication> GetMedicationByIdAsync(int id)
    {
        try
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            var medication = await connection.QueryFirstOrDefaultAsync<Medication>(GetMedicationByIdQuery, new { Id = id });

            if (medication == null)
            {
                throw new KeyNotFoundException($"Medication with id {id} not found");
            }

            return medication;
        }
        catch (Exception ex)
        {
            throw new Exception($"An error occurred while retrieving medication with id {id}: '{ex.Message}'.", ex);
        }
    }

    public async Task<List<Medication>> GetMedicationsByUserIdAsync(int userId)
    {
        try
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            var medications = await connection.QueryAsync<Medication>(GetMedicationsByUserIdQuery, new { UserId = userId });

            return medications.ToList();
        }
        catch (Exception ex)
        {
            throw new Exception($"An error occurred while retrieving medications ´for user with id {userId}: '{ex.Message}'.", ex);
        }
    }

    public async Task<int> CreateMedicationAsync(Medication medication)
    {
        try
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            var id = await connection.ExecuteScalarAsync<int>(CreateMedicationQuery, medication);

            return id;

        }
        catch (Exception ex)
        {
            throw new Exception($"An error occurred while inserting new medication: '{ex.Message}'.", ex);
        }

    }

    public async Task<bool> DeleteMedicationAsync(int id)
    {
        try
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.ExecuteAsync(DeleteMedicationQuery, new { Id = id }) > 0;
        }
        catch (Exception ex)
        {
            throw new Exception($"An error occurred while deleting medication with id {id}: '{ex.Message}'.", ex);
        }
    }

    public async Task<bool> UpdateMedicationAsync(Medication medication)
    {
        try
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.ExecuteAsync(UpdateMedicationQuery, medication) > 0;
        }
        catch (Exception ex)
        {
            throw new Exception($"An error occurred while updating medication with id {medication.Id}: '{ex.Message}'.", ex);
        }
    }
}