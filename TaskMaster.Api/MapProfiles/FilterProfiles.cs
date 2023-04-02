using AutoMapper;
using Issues.Manager.Api.Contracts;
using Issues.Manager.Application.DTOs;

namespace Issues.Manager.Api.MapProfiles;

public class FilterProfiles : Profile
{
	public FilterProfiles()
	{
		CreateMap<TicketFilterQuery, TicketFilters>().ReverseMap();
	}
}