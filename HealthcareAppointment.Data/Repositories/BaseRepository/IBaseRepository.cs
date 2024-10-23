using HealthcareAppointment.Data.Dtos;
using HealthcareAppointment.Data.Specifications;
using System.Linq.Expressions;

namespace HealthcareAppointment.Data.Repositories.BaseRepository
{
    public interface IBaseRepository<T, P> where T : class
	{
		Task<PaginationList<T>> GetItems(IBaseSpecification<T> specification);
		Task<T?> GetById(Expression<Func<T, bool>> Filter);
		Task<T> Add(T item);
		Task<T?> Update(Expression<Func<T, bool>> Filter, T item);
		Task<bool> Delete(P id);
	}
}
