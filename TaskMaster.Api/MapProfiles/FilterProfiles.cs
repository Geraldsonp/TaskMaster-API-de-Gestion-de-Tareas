using AutoMapper;
using Issues.Manager.Api.Contracts;
using Issues.Manager.Application.DTOs;
using Issues.Manager.Domain.Enums;

namespace Issues.Manager.Api.MapProfiles;

public class FilterProfiles : Profile
{
	public FilterProfiles()
	{
		CreateMap<TicketFilterQuery, TicketFilters>()
		.ForMember(x => x.Priority, s => s.MapFrom(x => Enum.Parse(typeof(Priority), x.Priority, true)))
		.ForMember(x => x.TicketType, s => s.MapFrom(x => Enum.Parse(typeof(TicketType), x.TicketType, true)))
		.ReverseMap();
	}
}