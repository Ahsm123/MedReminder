namespace MedReminder.DAL.Models;

public class Medication
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Name { get; set; }
    public int Dosage { get; set; }
    public string DosageUnit { get; set; }
    public string Instructions { get; set; }
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public User User { get; set; } = null!;
}
