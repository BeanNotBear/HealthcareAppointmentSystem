using System.Linq.Expressions;

namespace HealthcareAppointment.Data.Specifications
{
    public interface IBaseSpecification<T>
    {
        // Filter
        Expression<Func<T, bool>> Filters { get; }

        // Include
        List<Expression<Func<T, object>>> Includes { get; }

        // Order by
        Expression<Func<T, object>> OrderBy { get; }

        // Order by Descending
        Expression<Func<T, object>> Descending { get; }

        // Take
        int Take { get; }

        // Skip
        int Skip { get; }

        // Is Paging
        bool IsPagingEnable { get; }
    }
}
