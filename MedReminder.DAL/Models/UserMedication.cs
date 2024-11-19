namespace MedReminder.DAL.Models
{
	public class UserMedication
	{
		public int Id { get; set; }
		public int UserId { get; set; }
		public int MedicationId { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }

		public User User { get; set; } = null;
		public Medication Medication { get; set; } = null;
		public List<MedicationSchedule> MedicationSchedules { get; set; } = new List<MedicationSchedule>();
	}
}
