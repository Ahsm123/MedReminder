using MedReminder.DAL.Models;

namespace MedReminder.Dal.Interfaces;

public interface IMedicationDao
{
	Task<Medication> GetByIdAsync(int id);
	Task<int> CreateAsync(Medication medication);
}
