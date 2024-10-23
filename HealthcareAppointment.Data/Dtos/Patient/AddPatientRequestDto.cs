using HealthcareAppointment.Models.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareAppointment.Data.Dtos.Patient
{
	public class AddPatientRequestDto
	{
		[Required]
		[MinLength(3)]
		[MaxLength(255)]
		public string Name { get; set; }

		[Required]
		[MinLength(5)]
		[MaxLength(255)]
		[EmailAddress]
		public string Email { get; set; }

		[DataType(DataType.Date)]
		[NotFutureDate]
		public DateTime DateOfBirth { get; set; }

		[Required(ErrorMessage = "Password is required.")]
		[StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters long.")]
		[RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]+$",
		ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, one digit, and one special character.")]
		public string Password { get; set; }

		[Required]
		[MinLength(3)]
		[MaxLength(255)]
		public string Specialization { get; set; }
	}
}
