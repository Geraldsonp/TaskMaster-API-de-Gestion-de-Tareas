using AutoMapper;
using TaskMaster.Domain.ValueObjects;

namespace TaskMaster.Application.ExtensionMethods
{
	public static class PagedResponseExtensions
	{

		public static PagedResponse<TDest> ToMappedPagedResponse<Tsource, TDest>(this IQueryable<Tsource> source, int pageSize, int pageNumber, IMapper mapper)
		{
			var count = source.Count();


			return new PagedResponse<TDest>
			{
				TotalRecords = count,
				TotalPages = (int)Math.Ceiling(count / (double)pageSize),
				HasNext = count - (pageNumber * pageSize) > 0,
				HasPrevious = (pageNumber * pageSize) > pageSize,
				Items = mapper.Map<IEnumerable<TDest>>(source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList())
			};
		}
	}
}