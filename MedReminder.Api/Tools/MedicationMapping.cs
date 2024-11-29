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
                Id = medicationDTO.Id,
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

        public static MedicationDTO ToDTO(this Medication medication)
        {
            return new MedicationDTO
            {
                Id = medication.Id,
                UserId = medication.UserId,
                Name = medication.Name,
                Dosage = medication.Dosage,
                DosageUnit = medication.DosageUnit,
                Instructions = medication.Instructions,
                Description = medication.Description,
                StartDate = medication.StartDate,
                EndDate = medication.EndDate
            };
        }
    }
}
