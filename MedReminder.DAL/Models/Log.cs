namespace MedReminder.DAL.Models;

public class Log
{
    public int Id { get; set; }
    public int UserMedicationId { get; set; }
    public DateTime ScheduledTime { get; set; }
    public DateTime? TakenAt { get; set; }
    public bool WasMissed => TakenAt == null && DateTime.UtcNow > ScheduledTime;

}
