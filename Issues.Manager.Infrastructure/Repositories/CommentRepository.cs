using Issues.Manager.Application.Abstractions.RepositoryContracts;
using Issues.Manager.Domain.Entities;
namespace Issues.Manager.Infrastructure.Repositories;

public class CommentRepository : RepositoryBase<Comment>, ICommentsRepository
{
    public CommentRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}