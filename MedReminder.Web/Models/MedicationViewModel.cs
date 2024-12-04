namespace MedReminder.Web.Models
{
    public class MedicationViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Dosage { get; set; }
        public string DosageUnit { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; }
        public string Instructions { get; set; }
    }
}
