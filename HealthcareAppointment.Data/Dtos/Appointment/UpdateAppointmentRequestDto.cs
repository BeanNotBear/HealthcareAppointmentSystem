using HealthcareAppointment.Data.Attribute;
using HealthcareAppointment.Models.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareAppointment.Data.Dtos.Appointment
{
	public class UpdateAppointmentRequestDto
	{
		[Required]
		public Guid PatientId { get; set; }

		[Required]
		public Guid DoctorId { get; set; }

		[Required]
		[ValidateFutureDate]
		public DateTime Date { get; set; }

		[Required]
		[Range(0, 3)]
		public Status Status { get; set; }
	}
}
