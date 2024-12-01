using MedReminder.DAL.Models;
using MedReminder.Shared.DTOs;

namespace MedReminder.Api.Services.Interfaces;

public interface IMedicationService
{
    Task<int> CreateMedicationAsync(MedicationDTO medicationDTO);
    Task<List<Medication>> GetMedicationByUserIdAsync(int userId);
    Task<Medication> GetMedicationByIdAsync(int id);
    Task<bool> DeleteMedicationAsync(int id);
    Task<bool> UpdateMedicationAsync(MedicationDTO medicationDTO);
    Task<List<Medication>> GetDailyScheduleAsync(int userId, DateTime date);
}
