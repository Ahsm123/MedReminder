using MedReminder.DAL.Models;
using MedReminder.Shared.DTOs;

namespace MedReminder.Api.Tools
{
    public static class MedicationMapping
    {
        public static Medication ToDomain(this MedicationDTO medicationDTO)
        {
            return new Medication
            {
                Id = 0,
                UserId = medicationDTO.UserId,
                Name = medicationDTO.Name,
                Dosage = medicationDTO.Dosage,
                DosageUnit = medicationDTO.DosageUnit,
                Instructions = medicationDTO.Instructions,
                Description = medicationDTO.Description,
                StartDate = medicationDTO.StartDate,
                EndDate = medicationDTO.EndDate
            };
        }
    }
}
