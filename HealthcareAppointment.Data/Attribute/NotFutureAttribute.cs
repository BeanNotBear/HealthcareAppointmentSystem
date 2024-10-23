using System;
using System.ComponentModel.DataAnnotations;

public class NotFutureDateAttribute : ValidationAttribute
{
	protected override ValidationResult IsValid(object value, ValidationContext validationContext)
	{
		if (value is DateTime dateTimeValue)
		{
			if (dateTimeValue > DateTime.Now)
			{
				return new ValidationResult("Date of birth must be less than or equal to the current date.");
			}
		}
		return ValidationResult.Success;
	}
}
