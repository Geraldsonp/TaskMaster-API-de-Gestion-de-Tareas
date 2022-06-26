using AutoMapper;
using Issues.Manager.Business.DTOs;
using Issues.Manager.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Issues.Manager.Business.MappingConfigs;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Issue, IssueDto>();
        CreateMap<CreateIssueDto, Issue>();
        CreateMap<UserRegistrationDto, IdentityUser>();
    }
}