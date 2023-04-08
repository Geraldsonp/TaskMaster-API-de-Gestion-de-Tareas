namespace TaskMaster.Domain.ValueObjects
{
	public class PagedResponse<T>
	{
		public IEnumerable<T> Items { get; set; }
		public int TotalRecords { get; set; }
		public int TotalPages { get; set; }
		public bool HasNext { get; set; }
		public bool HasPrevious { get; set; }

		public static PagedResponse<T> ToPagedResponse(IQueryable<T>? queryable, int pageSize, int PageNumber)
		{
			var count = queryable.Count();
			return new PagedResponse<T>
			{
				TotalRecords = count,
				TotalPages = (int)Math.Ceiling(count / (double)pageSize),
				HasNext = count - (PageNumber - 1 * pageSize) > 0,
				HasPrevious = count - (PageNumber - 1 * pageSize) > 0,
				Items = queryable.Skip((PageNumber - 1) * pageSize).Take(pageSize).ToList(),
			};

		}
	}

}