namespace HealthcareAppointment.Data.Dtos
{
	public class PaginationList<T>
	{
		public ICollection<T> Items { get; }
		public int PageNumber { get; }
		public int PageSize { get; }
		public int TotalPages { get; }
		public int TotalRecords { get; }
		public bool IsHasNextPage => PageNumber < TotalPages;

		public bool IsHasPreviousPage => PageNumber > 1;

		public PaginationList(ICollection<T> items, int pageNumber, int pageSize, int totalPages, int totalRecords)
		{
			Items = items;
			PageNumber = pageNumber;
			PageSize = pageSize;
			TotalPages = totalPages;
			TotalRecords = totalRecords;
		}
	}
}
