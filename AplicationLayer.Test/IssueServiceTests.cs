using System;
using AutoMapper;
using Issues.Manager.Application.DTOs;
using Issues.Manager.Application.MappingConfigs;
using Issues.Manager.Application.Services;
using Issues.Manager.Application.Services.Logger;
using Issues.Manager.Domain.Contracts;
using Issues.Manager.Domain.Entities;
using Issues.Manager.Domain.Enums;
using Issues.Manager.Domain.Exceptions;
using Issues.Manager.Infrastructure.Repositories;
using Moq;
using Xunit;

namespace AplicationLayer.Test;

public class IssueServiceTests
{
    private IIssueService _systemUnderTest;
    private readonly Mock<IRepositoryManager> _unitOfWork = new Mock<IRepositoryManager>();
    private readonly Mock<ILoggerManager> _loggerMock = new Mock<ILoggerManager>();
    private readonly Mock<IRepositoryBase<Issue>> _issueRepositoryMock = new Mock<IRepositoryBase<Issue>>();
    private IMapper _mapper;
    private readonly Issue _issue;

    public IssueServiceTests()
    {
        var profiles = new MappingProfiles();
        var configuration = new MapperConfiguration(cfg => cfg.AddProfile(profiles));
        _mapper = new Mapper(configuration);
        _issue =  new Issue
        {
            Title = "test Issue",
            Description = "this is the test issue",
            CompletedAt = null,
            Created = DateTime.Now,
            Id = 1
        };
    }


    [Fact]
    public void GetIssueById_ShouldReturnIssueDto_WhenValidId()
    {
        //Arrange
        _issueRepositoryMock.Setup(i => 
            i.FindByCondition(x => x.Id == 1
                , false)).Returns(_issue).Verifiable();

        _unitOfWork.Setup(m => m.IssuesRepository)
            .Returns(_issueRepositoryMock.Object);

        _systemUnderTest = new IssueService(_unitOfWork.Object, _mapper);
        
        //Act
        var result = _systemUnderTest.GetById(1, false);
        
        //Assert
        Assert.NotNull(result);
        Assert.IsType<IssueReponse>(result);

    }

    [Fact]
    public void GetIssueById_ShouldReturnIssueNotFoundException_WhenInvalidValidId()
    {
        

    }

    [Fact]
    public void CreateIssue_ShouldReturnIssueDto_WhenValidIssue()
    {
        

    }
} 