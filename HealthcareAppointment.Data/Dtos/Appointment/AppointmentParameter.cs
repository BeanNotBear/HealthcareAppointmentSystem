using HealthcareAppointment.Models.Enum;
using System.ComponentModel.DataAnnotations;


namespace HealthcareAppointment.Data.Dtos.Appointment
{
	public class AppointmentParameter : BaseParameter
	{
		[DataType(DataType.Date)]
		public DateTime FromDate { get; set; } = DateTime.MinValue;

		[DataType(DataType.Date)]
		public DateTime ToDate { get; set; } = DateTime.MaxValue;

		public List<Status> Statuses { get; set; } = new List<Status>() { Status.Scheduled, Status.Completed, Status.Cancelled };

		public bool IsDescending { get; set; } = false;
	}
}
