using System.Reflection;
using Mapster;
using TaskMaster.Api.Contracts;
using TaskMaster.Application.WorkItemFeature.Dtos;
using TaskMaster.Domain.Enums;

namespace TaskMaster.Api.MapProfiles;
public static class MapsterConfig
{
	public static void RegisterMapsterConfiguration(this IServiceCollection services)
	{
		var config = TypeAdapterConfig<WorkItemQueryFilter, WorkItemFilter>
			.NewConfig()
			.IgnoreIf((src, dest) => string.IsNullOrEmpty(src.TicketType), dest => dest.WorkItemType)
			.IgnoreIf((src, dest) => string.IsNullOrEmpty(src.Priority), dest => dest.Priority)
			.Map(dest => dest.Priority, src => Enum.Parse(typeof(Priority), src.Priority, true))
			.Map(dest => dest.WorkItemType, src => Enum.Parse(typeof(WorkItemType), src.TicketType, true));
		
		
		
		TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly())
			;
	}
}