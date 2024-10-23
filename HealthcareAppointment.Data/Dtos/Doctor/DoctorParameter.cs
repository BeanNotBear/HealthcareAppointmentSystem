using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareAppointment.Data.Dtos.Doctor
{
	public class DoctorParameter : BaseParameter
	{
		public string? Search { get; set; }
		public bool IsDescending { get; set; } = false;
	}
}
