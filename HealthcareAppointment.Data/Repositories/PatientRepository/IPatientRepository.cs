using HealthcareAppointment.Data.Repositories.BaseRepository;
using HealthcareAppointment.Models.Entities;

namespace HealthcareAppointment.Data.Repositories.PatientRepository
{
	public interface IPatientRepository : IBaseRepository<User, Guid>
	{
	}
}
