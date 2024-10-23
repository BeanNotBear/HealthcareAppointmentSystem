using HealthcareAppointment.Models.Enum;

namespace HealthcareAppointment.Models.Entities
{
	public class User
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }
		public DateTime DateOfBirth { get; set; }
		public string Password { get; set; }
		public Role Role { get; set; }
		public string Specialization { get; set; }
		public ICollection<Appointment> PatientAppointments { get; set; }
		public ICollection<Appointment> DoctorAppointments { get; set; }
	}
}
