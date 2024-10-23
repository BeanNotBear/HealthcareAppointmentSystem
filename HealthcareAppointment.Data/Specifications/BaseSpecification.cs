using System.Linq.Expressions;

namespace HealthcareAppointment.Data.Specifications
{
    public class BaseSpecification<T> : IBaseSpecification<T>
    {
        public BaseSpecification()
        {
        }

        public BaseSpecification(Expression<Func<T, bool>> filters)
        {
            Filters = filters;
        }

        public Expression<Func<T, bool>> Filters { get; private set; }

        public List<Expression<Func<T, object>>> Includes { get; private set; } = new List<Expression<Func<T, object>>>();

        public Expression<Func<T, object>> OrderBy { get; private set; }

        public Expression<Func<T, object>> Descending { get; private set; }

        public int Take { get; private set; }

        public int Skip { get; private set; }

        public bool IsPagingEnable { get; private set; }

        public void AddInclude(Expression<Func<T, object>> include) => Includes.Add(include);
        public void AddOrderBy(Expression<Func<T, object>> orderBy) => OrderBy = orderBy;
        public void AddDescending(Expression<Func<T, object>> descending) => Descending = descending;
        public void EnablePaging(int take, int skip)
        {
            Take = take;
            Skip = skip;
            IsPagingEnable = true;
        }
    }
}
