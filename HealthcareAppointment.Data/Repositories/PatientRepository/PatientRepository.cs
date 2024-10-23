using HealthcareAppointment.Data.Data;
using HealthcareAppointment.Data.Repositories.BaseRepository;
using HealthcareAppointment.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareAppointment.Data.Repositories.PatientRepository
{
	public class PatientRepository : BaseRepository<User, Guid>, IPatientRepository
	{

		private readonly HealthcareAppointmentDbContext _context;
		public PatientRepository(HealthcareAppointmentDbContext context) : base(context)
		{
			_context = context;
		}

		public async Task<User?> GetUserByEmailAndPassword(string email, string password)
		{
			var patient = await _context.Users.FirstOrDefaultAsync(x => x.Email == email && x.Password == password);
			return patient;
		}
	}
}
