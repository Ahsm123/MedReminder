using MedReminder.DAL.Models;

namespace MedReminder.Dal.Interfaces;

public interface IMedicationDao
{
    Task<Medication> GetMedicationByIdAsync(int id);
    Task<int> CreateMedicationAsync(Medication medication);
    Task<List<Medication>> GetMedicationsByUserIdAsync(int userId);
    Task<bool> DeleteMedicationAsync(int id);
    Task<bool> UpdateMedicationAsync(Medication medication);
}
