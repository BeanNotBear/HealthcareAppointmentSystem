using HealthcareAppointment.Data.Data;
using HealthcareAppointment.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareAppointment.Data.Repositories.AuthRepository
{
	public class AuthRepository : IAuthRepository
	{
		private readonly HealthcareAppointmentDbContext _context;

		public AuthRepository(HealthcareAppointmentDbContext context)
		{
			_context = context;
		}

		public async Task<User?> Authenticate(string email, string password)
		{
			var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == email && x.Password == password);
			return user;
		}
	}
}
