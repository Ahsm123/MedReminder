using MedReminder.DAL.Models;
using MedReminder.Shared.DTOs;

namespace MedReminder.Api.Services.Interfaces
{
    public interface IMedicationService
    {
        Task<Medication> CreateMedicationAsync(MedicationDTO medicationDTO);
        Task<List<Medication>> GetMedicationByUserIdAsync(int userId);
    }
}
