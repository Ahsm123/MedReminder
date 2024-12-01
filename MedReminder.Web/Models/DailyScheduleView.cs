namespace MedReminder.Web.Models
{
    public class DailyScheduleView
    {
        public DateTime Date { get; set; }
        public List<ScheduledMedicationViewModel> Medications { get; set; } = new();

    }
}
