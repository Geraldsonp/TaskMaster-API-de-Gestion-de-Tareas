using AutoMapper;
using Issues.Manager.Business.DTOs;
using Issues.Manager.Domain.Entities;

namespace Issues.Manager.Business.MappingConfigs;

public class IssueMappingConfig : Profile
{
    public IssueMappingConfig()
    {
        CreateMap<Issue, IssueDto>();
        CreateMap<CreateIssueDto, Issue>();
    }
}