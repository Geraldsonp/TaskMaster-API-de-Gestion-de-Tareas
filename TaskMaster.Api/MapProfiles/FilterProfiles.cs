using System.Reflection;
using Mapster;
using TaskMaster.Api.Contracts;
using TaskMaster.Application.TaskEntity.Dtos;
using TaskMaster.Domain.Enums;

namespace TaskMaster.Api.MapProfiles;
public static class MapsterConfig
{
	public static void RegisterMapsterConfiguration(this IServiceCollection services)
	{
		var config = TypeAdapterConfig<TicketFilterQuery, TaskFilter>
			.NewConfig()
			.IgnoreIf((src, dest) => string.IsNullOrEmpty(src.TicketType), dest => dest.TicketType)
			.IgnoreIf((src, dest) => string.IsNullOrEmpty(src.Priority), dest => dest.Priority)
			.Map(dest => dest.Priority, src => Enum.Parse(typeof(Priority), src.Priority, true))
			.Map(dest => dest.TicketType, src => Enum.Parse(typeof(TicketType), src.TicketType, true));
		
		
		
		TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly())
			;
	}
}