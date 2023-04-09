using Bogus;
using Issues.Manager.Domain.Entities;
using Issues.Manager.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Issues.Manager.Infrastructure.DBConfiguration;

public static class MigrationHelper
{
	public static async Task RunMigrationsAsync(IServiceProvider serviceProvider)
	{
		var dbContext = serviceProvider.GetRequiredService<AppDbContext>();

		await dbContext.Database.MigrateAsync();

		var userId = dbContext.Users.FirstOrDefault(x => x.Email == "GeraldTest@email.com")?.Id;

		if (dbContext.Tickets.Any() && userId != null)
		{
			DateTime startDate = DateTime.Today.AddMonths(-12);
			DateTime endDate = DateTime.Today;


			var commentsFaker = new Faker<Comment>()
			.RuleFor(x => x.UserId, x => userId)
			.RuleFor(x => x.PostedDate, x => x.Date.RecentOffset(5, startDate).DateTime)
			.RuleFor(x => x.Content, x => x.Lorem.Lines());

			var ticketssFaker = new Faker<TaskEntity>()

			.RuleFor(x => x.Comments, c => commentsFaker.Generate(5))
			.RuleFor(x => x.CompletedAt, d => d.Date.Between(startDate, endDate).OrNull(d, 0.25f))
			.RuleFor(x => x.Description, d => d.Lorem.Sentence(5, 15))
			.RuleFor(x => x.Created, d => d.Date.Between(endDate.AddMonths(-1), endDate.AddMonths(-10)))
			.RuleFor(x => x.IsCompleted, c => c.PickRandom(new[] { true, false }))
			.RuleFor(x => x.Priority, c => c.PickRandom(new[] { Priority.High, Priority.Low, Priority.Medium }))
			.RuleFor(x => x.TicketType, c => c.PickRandom(new[] { TicketType.Bug, TicketType.Feature, TicketType.Documentation }))
			.RuleFor(x => x.Title, c => c.Name.JobType())
			.RuleFor(x => x.UserId, c => userId);


			await dbContext.Tickets.AddRangeAsync(ticketssFaker.Generate(300));
			await dbContext.SaveChangesAsync();
		}
	}
}