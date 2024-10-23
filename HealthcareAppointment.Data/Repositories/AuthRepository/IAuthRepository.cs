using HealthcareAppointment.Models.Entities;

namespace HealthcareAppointment.Data.Repositories.AuthRepository
{
	public interface IAuthRepository
	{
		Task<User?> Authenticate(string email, string password);
	}
}
