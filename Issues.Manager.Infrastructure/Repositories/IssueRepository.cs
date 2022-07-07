using System.Linq.Expressions;
using Issues.Manager.Application.Abstractions.RepositoryContracts;
using Issues.Manager.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Issues.Manager.Infrastructure.Repositories;

public class IssueRepository : RepositoryBase<Issue>, IIssueRepository
{
    public IssueRepository(AppDbContext context)
        :base(context)
    {
    }

    
}