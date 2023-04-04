namespace TaskMaster.Domain.ValueObjects
{
	public class PagedResponse<T>
	{
		public IEnumerable<T> Items { get; set; }
		public int TotalCount { get; set; }
		public int TotalPages { get; set; }

		public static PagedResponse<T> ToPageResponse(IQueryable<T> queryable, int pageSize, int PageNumber)
		{
			var count = queryable.Count();
			return new PagedResponse<T>
			{
				TotalCount = count,
				TotalPages = (int)Math.Ceiling(count / (double)pageSize),
				Items = queryable.Skip((PageNumber - 1) * pageSize).Take(pageSize).ToList()
			};

		}
	}

}