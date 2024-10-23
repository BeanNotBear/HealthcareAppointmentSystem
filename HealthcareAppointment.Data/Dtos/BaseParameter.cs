using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareAppointment.Data.Dtos
{
	public class BaseParameter
	{
		private int MAX_PAGE_SIZE = 5;
		private int pageNumber = 1;
		public int PageNumber
		{
			get => pageNumber;
			set => pageNumber = (value < 0) ? 1 : value;
		}
		private int pageSize = 5;
		public int PageSize
		{
			get => pageSize;
			set => pageSize = (value > MAX_PAGE_SIZE || value < 0) ? MAX_PAGE_SIZE : value;
		}
	}
}
