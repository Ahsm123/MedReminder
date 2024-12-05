using System.ComponentModel.DataAnnotations;

namespace MedReminder.Shared.DTOs;

public class MedicationDTO
{
    public int Id { get; set; }
    [Required]
    public int UserId { get; set; }
    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; }
    [Required(ErrorMessage = "Dosage is required")]
    public int Dosage { get; set; }
    [Required(ErrorMessage = "DosageUnit is required")]
    public string DosageUnit { get; set; }
    [Required(ErrorMessage = "Instructions are required")]
    [MaxLength(100)]
    public string Instructions { get; set; }
    [Required(ErrorMessage = "Description is required")]
    [MaxLength(100)]
    public string Description { get; set; }
    [Required(ErrorMessage = "Start date is required")]
    public DateTime StartDate { get; set; } = DateTime.Now;
    [Required(ErrorMessage = "End date is required")]
    public DateTime EndDate { get; set; } = DateTime.Now.AddMonths(1);
    [Required(ErrorMessage = "TimeToTake is required")]
    public TimeSpan TimeToTake { get; set; }
    [Required(ErrorMessage = "Recurrence type is required")]
    public string RecurrencePattern { get; set; }

    public List<string> TimeOptions { get; set; } = Enumerable
    .Range(0, 96)
    .Select(i => TimeSpan.FromMinutes(i * 15).ToString(@"hh\:mm"))
    .ToList();
}
