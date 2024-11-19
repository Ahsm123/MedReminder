namespace MedReminder.DAL.Models;

public class Medication
{
	public int Id { get; set; }
	public string Name { get; set; }
	public string Dosage { get; set; }
	public string Instructions { get; set; }
	public string Description { get; set; }

	public List<User> Users { get; set; }
}
