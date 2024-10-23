using HealthcareAppointment.Data.Data;
using HealthcareAppointment.Data.Dtos;
using HealthcareAppointment.Data.Specifications;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareAppointment.Data.Repositories.BaseRepository
{
    public class BaseRepository<T, P> : IBaseRepository<T, P> where T : class
	{

		private readonly HealthcareAppointmentDbContext _context;

		public BaseRepository(HealthcareAppointmentDbContext context)
		{
			_context = context;
		}

		public async Task<T> Add(T item)
		{
			await _context.Set<T>().AddAsync(item);
			await _context.SaveChangesAsync();
			return item;
		}

		public async Task<bool> Delete(P id)
		{
			var entity = await _context.Set<T>().FindAsync(id);
			if (entity != null)
			{
				_context.Set<T>().Remove(entity);
				await _context.SaveChangesAsync();
				return true;
			}
			return false;
		}

		public async Task<T?> GetById(Expression<Func<T, bool>> Filter)
		{
			var query = _context.Set<T>().AsQueryable();
			var entity = await query.FirstOrDefaultAsync(Filter);
			return entity;
		}

		public async Task<PaginationList<T>> GetItems(IBaseSpecification<T> specification)
		{
			var query = _context.Set<T>().AsQueryable();
			if (specification.Filters is not null)
			{
				query = query.Where(specification.Filters);
			}

			ApplyInclude(query, specification.Includes);

			if (specification.OrderBy is not null)
			{
				query = query.OrderBy(specification.OrderBy);
			}

			if (specification.Descending is not null)
			{
				query = query.OrderByDescending(specification.Descending);
			}

			var totalRecords = await query.CountAsync();

			if (specification.IsPagingEnable)
			{
				query = query.Skip(specification.Skip).Take(specification.Take);
			}

			var items = await query.ToListAsync();
			var pageNumber = specification.Skip / specification.Take + 1;
			var pageSize = specification.Take;
			var totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);
			var result = new PaginationList<T>(items, pageNumber, pageSize, totalPages, totalRecords);
			return result;
		}

		public async Task<T?> Update(Expression<Func<T, bool>> Filter, T item)
		{
			var existedEntity = await _context.Set<T>().Where(Filter).FirstOrDefaultAsync();
			if (existedEntity != null)
			{
				_context.Entry(existedEntity).CurrentValues.SetValues(item);
				await _context.SaveChangesAsync();
				return item;
			}
			return null;
		}

		private void ApplyInclude(IQueryable<T> query, List<Expression<Func<T, object>>> includes)
		{
			if (includes?.Any() == true)
			{
				foreach (var include in includes)
				{
					query = query.Include(include);
				}
			}
		}
	}
}
