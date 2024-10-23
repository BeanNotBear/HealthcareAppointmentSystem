using HealthcareAppointment.Data.Data;
using HealthcareAppointment.Data.Repositories.BaseRepository;
using HealthcareAppointment.Models.Entities;
using HealthcareAppointment.Models.Enum;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareAppointment.Data.Repositories.AppointmentRepository
{
	public class AppointmentRepository : BaseRepository<Appointment, Guid>, IAppointmentRepository
	{
		private readonly HealthcareAppointmentDbContext _context;

		public AppointmentRepository(HealthcareAppointmentDbContext context) : base(context)
		{
			_context = context;
		}

		public async Task<Appointment?> Cancel(Guid id)
		{
			var appointment = await _context.Appointments.FirstOrDefaultAsync(x => x.Id == id);
			if (appointment != null)
			{
				appointment.Status = Status.Cancelled;
				await _context.SaveChangesAsync();
			}
			return appointment;
		}
	}
}
