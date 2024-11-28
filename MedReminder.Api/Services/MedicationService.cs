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
    public async Task<Medication> CreateMedicationAsync(MedicationDTO medicationDTO)
    {
        var medication = medicationDTO.ToDomain();

        medication.Id = await _medicationDao.CreateAsync(medication);
        return medication;

    }

    public async Task<List<Medication>> GetMedicationByUserIdAsync(int userId)
    {
        var medications = await _medicationDao.GetMedicationsByUserIdAsync(userId);
        return medications;
    }
}
