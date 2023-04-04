using AutoMapper;
using Issues.Manager.Api.Contracts;
using TaskMaster.Domain.ValueObjects;

namespace TaskMaster.Api.MapProfiles
{
	public class PaggingMapping : Profile
	{
		public PaggingMapping()
		{
			CreateMap<PagingQuery, Paggination>();
		}
	}
}