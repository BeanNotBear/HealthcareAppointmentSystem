using HealthcareAppointment.Data.Data;
using HealthcareAppointment.Data.Repositories.BaseRepository;
using HealthcareAppointment.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareAppointment.Data.Repositories.DoctorRepository
{
	public class DoctorRepository : BaseRepository<User, Guid>, IDoctorRepository
	{

		public DoctorRepository(HealthcareAppointmentDbContext context) : base(context)
		{
		}
	}
}
