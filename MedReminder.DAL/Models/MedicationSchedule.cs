namespace MedReminder.DAL.Models
{
    public class MedicationSchedule
    {
        public int Id { get; set; }
        public int UserMedicationId { get; set; }
        public DateTime TimeToTake { get; set; }
        public bool IsRecurring { get; set; }
        public RecurrenceType Recurrence { get; set; }

    }

    public enum RecurrenceType
    {
        None = 0,
        Daily = 1,
        Weekly = 2,
        Monthly = 3
    }

}
