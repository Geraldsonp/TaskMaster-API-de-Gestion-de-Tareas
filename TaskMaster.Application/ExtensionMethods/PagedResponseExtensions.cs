using Mapster;
using TaskMaster.Domain.ValueObjects;

namespace TaskMaster.Application.ExtensionMethods
{
	public static class PagedResponseExtensions
	{

		public static PagedResponse<TDest> ToMappedPagedResponse<Tsource, TDest>(this IQueryable<Tsource> source, Pagination paging)
		{
			var count = source.Count();


			return new PagedResponse<TDest>
			{
				TotalRecords = count,
				TotalPages = (int)Math.Ceiling(count / (double)paging.PageSize),
				HasNext = count - (paging.PageNumber * paging.PageSize) > 0,
				HasPrevious = (paging.PageNumber * paging.PageSize) > paging.PageSize,
				Items = source.Skip((paging.PageNumber - 1) * paging.PageSize).Take(paging.PageSize).ToList().Adapt<IEnumerable<TDest>>()
			};
		}
	}
}