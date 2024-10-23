using HealthcareAppointment.Data.Repositories.BaseRepository;
using HealthcareAppointment.Models.Entities;

namespace HealthcareAppointment.Data.Repositories.AppointmentRepository
{
	public interface IAppointmentRepository : IBaseRepository<Appointment, Guid>
	{
		public Task<Appointment?> Cancel(Guid id);
	}
}
