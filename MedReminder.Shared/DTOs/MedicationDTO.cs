using System.ComponentModel.DataAnnotations;

namespace MedReminder.Shared.DTOs;

public class MedicationDTO
{
    public int Id { get; set; }
    [Required]
    public int UserId { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public int Dosage { get; set; }
    [Required]
    public string DosageUnit { get; set; }
    [Required]
    public string Instructions { get; set; }
    [Required]
    public string Description { get; set; }
    [Required]
    public DateTime StartDate { get; set; } = DateTime.Now;
    [Required]
    public DateTime EndDate { get; set; } = DateTime.Now.AddMonths(1);
    [Required]
    public TimeSpan TimeToTake { get; set; }
    [Required]
    public string RecurrencePattern { get; set; } = "Daily";
}
