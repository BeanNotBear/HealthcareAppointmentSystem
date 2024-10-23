using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareAppointment.Data.Attribute
{
	public class ValidateFutureDate : ValidationAttribute
	{
		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			if (value is DateTime dateTimeValue)
			{
				if (dateTimeValue < DateTime.Now)
				{
					return new ValidationResult("Date of birth must be more than or equal to the current date.");
				}
			}
			return ValidationResult.Success;
		}
	}
}
