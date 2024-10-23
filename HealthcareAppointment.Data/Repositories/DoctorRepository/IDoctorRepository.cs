using HealthcareAppointment.Data.Repositories.BaseRepository;
using HealthcareAppointment.Models.Entities;

namespace HealthcareAppointment.Data.Repositories.DoctorRepository
{
	public interface IDoctorRepository : IBaseRepository<User, Guid>
	{
	}
}
