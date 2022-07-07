using AutoMapper;
using Issues.Manager.Application.DTOs;
using Issues.Manager.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Issues.Manager.Application.MappingConfigs;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Issue, IssueDto>();
        CreateMap<IssueDto, Issue>();
        CreateMap<CreateIssueDto, Issue>();
        CreateMap<UserRegistrationDto, IdentityUser>();
    }
}