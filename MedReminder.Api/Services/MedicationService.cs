using MedReminder.Api.Services.Interfaces;
using MedReminder.Api.Tools;
using MedReminder.Dal.Interfaces;
using MedReminder.DAL.Models;
using MedReminder.Shared.DTOs;

namespace MedReminder.Api.Services;

public class MedicationService : IMedicationService
{
    private readonly IMedicationDao _medicationDao;

    public MedicationService(IMedicationDao medicationDao)
    {
        _medicationDao = medicationDao;
    }
    public async Task<int> CreateMedicationAsync(MedicationDTO medicationDTO)
    {
        var medication = medicationDTO.ToDomain();

        var medicationId = await _medicationDao.CreateMedicationAsync(medication);
        return medicationId;

    }

    public async Task<List<Medication>> GetMedicationByUserIdAsync(int userId)
    {
        var medications = await _medicationDao.GetMedicationsByUserIdAsync(userId);
        return medications;
    }

    public async Task<Medication> GetMedicationByIdAsync(int id)
    {
        var medication = await _medicationDao.GetMedicationByIdAsync(id);
        return medication;
    }

    public async Task<bool> DeleteMedicationAsync(int id)
    {
        var isDeleted = await _medicationDao.DeleteMedicationAsync(id);
        return isDeleted;
    }

    public async Task<bool> UpdateMedicationAsync(MedicationDTO medicationDTO)
    {
        var medication = medicationDTO.ToDomain();
        var isUpdated = await _medicationDao.UpdateMedicationAsync(medication);
        return isUpdated;
    }

    public async Task<List<Medication>> GetDailyScheduleAsync(int userId, DateTime date) //TODO: Gør det mere robust, tilføj evt. enums for type safety
    {
        var medications = await _medicationDao.GetMedicationsByUserIdAsync(userId);

        return medications
            .Where(m =>
            {
                if (m.RecurrencePattern == "Daily")
                    return true;

                if (m.RecurrencePattern.StartsWith("Every"))
                {
                    var days = int.Parse(m.RecurrencePattern.Replace("Every ", "").Replace(" days", ""));
                    return (date - m.StartDate).Days % days == 0;
                }

                if (m.RecurrencePattern.StartsWith("Weekly"))
                {
                    var day = m.RecurrencePattern.Replace("Weekly - ", "");
                    return date.DayOfWeek.ToString() == day;
                }

                return false;
            })
            .Where(m => m.StartDate <= date && m.EndDate >= date)
            .ToList();
    }

}
