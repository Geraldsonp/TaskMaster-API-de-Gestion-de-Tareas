using System;
using AutoMapper;
using Issues.Manager.Application.Abstractions.RepositoryContracts;
using Issues.Manager.Application.DTOs;
using Issues.Manager.Application.MappingConfigs;
using Issues.Manager.Application.Services;
using Issues.Manager.Application.Services.Logger;
using Issues.Manager.Domain.Entities;
using Issues.Manager.Domain.Enums;
using Issues.Manager.Domain.Exceptions;
using Moq;
using Xunit;

namespace AplicationLayer.Test;

public class IssueServiceTests
{
    private IIssueService _systemUnderTest;
    private readonly Mock<IRepositoryManager> _unitOfWork = new Mock<IRepositoryManager>();
    private readonly Mock<ILoggerManager> _loggerMock = new Mock<ILoggerManager>();
    private readonly Mock<IIssueRepository> _issueRepositoryMock = new Mock<IIssueRepository>();
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

        _unitOfWork.Setup(m => m.Issue)
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
        //Arrange

        var issueRepositoryMock = new Mock<IIssueRepository>();
        issueRepositoryMock.Setup(i => 
            i.FindByCondition(x => x.Id == 1
                , false)).Returns(_issue).Verifiable();

        _unitOfWork.Setup(m => m.Issue)
            .Returns(issueRepositoryMock.Object);

        _systemUnderTest = new IssueService(_unitOfWork.Object, _mapper);
        
        //Act

        //Assert
        Assert.Throws<IssueNotFoundException>( () => _systemUnderTest.GetById(5, false));

    }

    [Fact]
    public void CreateIssue_ShouldReturnIssueDto_WhenValidIssue()
    {
        var issue = new CreateIssueRequest()
        {
            Description = "test",
            IssueType = IssueType.Bug,
            Priority = Priority.High,
            Title = "Bobo"
        };
        var userid = 1;
        
        //Arrange
        var userId = "qweqweqweqwe";
        _unitOfWork.Setup(u =>
            u.User.FindByCondition(u => u.IdentityId == userId, false)).Returns(new User(){Id = userid}).Verifiable();

        _unitOfWork.Setup(u => u.Issue.Create(new Issue()));
        _unitOfWork.Setup(u => u.SaveChanges());

        var result2 = _unitOfWork.Object.User.FindByCondition(u => u.IdentityId == userId, false);

        _systemUnderTest = new IssueService(_unitOfWork.Object, _mapper);
        
        //Act

        var result = _systemUnderTest.Create(issue, userId);

        //Assert
        Assert.IsType<IssueReponse>(result);
        Assert.Equal(1, result.UserId);
        _unitOfWork.Verify(u => u.SaveChanges(), Times.Once);

    }
} 