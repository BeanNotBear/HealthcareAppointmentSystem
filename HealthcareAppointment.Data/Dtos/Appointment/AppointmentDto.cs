using HealthcareAppointment.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareAppointment.Data.Dtos.Appointment
{
	public class AppointmentDto
	{
		public Guid Id { get; set; }
		public Guid PatientId { get; set; }
		public Guid DoctorId { get; set; }
		public DateTime Date { get; set; }
		public string Status { get; set; }
	}
}
