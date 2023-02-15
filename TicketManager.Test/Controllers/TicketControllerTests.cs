using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Issues.Manager.Api.Contracts;
using Issues.Manager.Application.DTOs;
using Issues.Manager.Application.Models.Issue;
using Issues.Manager.Domain.Enums;
using Issues.Manager.Infrastructure;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TicketManager.Test.Configuration;
using Xunit;

namespace TicketManager.Test.Controllers;

public class TicketControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _httpClient;
    private readonly IServiceScope _scopedServiceProvider;
    private readonly IdentityHelper _identityHelper;

    public TicketControllerTests(WebApplicationFactory<Program> factory)
    {
        factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("TicketTest"));
            });
        });
        _httpClient = factory.CreateClient();
        _scopedServiceProvider = factory.Services.CreateScope();
        _identityHelper = new IdentityHelper(_httpClient);
    }

    [Fact]
    public async Task CreateTicket_ReturnsBadRequest_WhenInvalidData()
    {
        //Arrange
        var token = await _identityHelper.AuthenticateUser();
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var request = new TicketCreateRequest
        {
            Priority = Priority.High,
            TicketType = TicketType.Bug,
            Title = string.Empty
        };

        //Act
        var response = await _httpClient.PostAsJsonAsync("Api/Ticket", request);
        var content = await response.Content.ReadAsStringAsync();

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task CreateTicket_ReturnsCreated_WhenValidData()
    {
        //Arrange
        var token = await _identityHelper.AuthenticateUser();
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var request = new TicketCreateRequest
        {
            Description = "testing",
            Priority = Priority.High,
            TicketType = TicketType.Bug,
            Title = "Test Ticket"
        };

        //Act
        var response = await _httpClient.PostAsJsonAsync("Api/Ticket", request);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);
    }

    [Fact]
    public async Task GetTickets_ReturnsListOfTickets_WhenCalled()
    {
        //Arrange
        var token = await _identityHelper.AuthenticateUser();
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var context = _scopedServiceProvider.ServiceProvider.GetService<AppDbContext>();

        var user = context.Users.FirstOrDefault(x => x.Email == "Testing@Email.com");

        if (user?.Id != null)
            await CreateTicketsForCurrentUser(user?.Id, 3);

        var tasksCount = context.Tickets.ToList().Count;

        //Act
        var response = await _httpClient.GetAsync("api/Ticket");
        var responseString = response.Content.ReadAsStringAsync();
        var tickets = await response.Content.ReadFromJsonAsync<IEnumerable<TicketDetailsModel>>();

        //assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        tickets.Should().NotBeEmpty();
        tickets.Count().Should().Be(tasksCount);
    }

    [Fact]
    public async Task GetTickets_ReturnsBadRequest_WhenNotAuthorized()
    {
        //Arrange


        //Act
        var response = await _httpClient.GetAsync("api/Ticket");
        var responseString = response.Content.ReadAsStringAsync();

        //assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task GetTicket_PriorityFilterParameters_ReturnsFilteredList()
    {
        //Arrange
        var token = await _identityHelper.AuthenticateUser();
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var context = _scopedServiceProvider.ServiceProvider.GetService<AppDbContext>();

        var user = context.Users.FirstOrDefault(x => x.Email == "Testing@Email.com");

        if (user?.Id != null)
            await CreateTicketsForCurrentUser(user?.Id, 10);
        var queryParameters = new TicketFilterQueryParameters()
        {
            Priority = Priority.High
        };

        var tasks = context.Tickets.ToList().Where(task => task.Priority == queryParameters.Priority);
        var tasksCount = tasks.Count();



        //Act
        var response = await _httpClient.GetAsync("api/Ticket?priority=High");
        var responseString = response.Content.ReadAsStringAsync();
        var tickets = await response.Content.ReadFromJsonAsync<IEnumerable<TicketDetailsModel>>();

        //assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        tickets.Count().Should().Be(tasksCount);
    }

    [Fact]
    public async Task GetTicket_TicketTypeParameters_ReturnsFilteredList()
    {
        //Arrange
        var token = await _identityHelper.AuthenticateUser();
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var context = _scopedServiceProvider.ServiceProvider.GetService<AppDbContext>();

        var user = context.Users.FirstOrDefault(x => x.Email == "Testing@Email.com");

        if (user?.Id != null)
            await CreateTicketsForCurrentUser(user?.Id, 10);

        var tasks = context.Tickets.ToList().Where(task => task.TicketType == TicketType.Feature);
        var tasksCount = tasks.Count();



        //Act
        var response = await _httpClient.GetAsync("api/Ticket?ticketType=Feature");
        var responseString = response.Content.ReadAsStringAsync();
        var tickets = await response.Content.ReadFromJsonAsync<IEnumerable<TicketDetailsModel>>();

        //assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        tickets.Count().Should().Be(tasksCount);
    }
    [Fact]
    public async Task GetTicket_TicketTypeAndPriorityFilterParameters_ReturnsFilteredList()
    {
        //Arrange
        var token = await _identityHelper.AuthenticateUser();
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var context = _scopedServiceProvider.ServiceProvider.GetService<AppDbContext>();

        var user = context.Users.FirstOrDefault(x => x.Email == "Testing@Email.com");

        if (user?.Id != null)
            await CreateTicketsForCurrentUser(user?.Id, 10);

        var tasks = context.Tickets.ToList().Where(task => task.TicketType == TicketType.Bug && task.Priority == Priority.Low);
        var tasksCount = tasks.Count();



        //Act
        var response = await _httpClient.GetAsync("api/Ticket?ticketType=Bug&priority=Low");
        var responseString = response.Content.ReadAsStringAsync();
        var tickets = await response.Content.ReadFromJsonAsync<IEnumerable<TicketDetailsModel>>();

        //assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        tickets.Count().Should().Be(tasksCount);
    }

    #region HelperMethods

    private async Task CreateTicketsForCurrentUser(string userId, int count)
    {
        var autoFixture = new Fixture();
        var tickets = autoFixture.Build<TicketCreateRequest>()
            .CreateMany(count);

        foreach (var request in tickets)
        {
            var response = await _httpClient.PostAsJsonAsync("Api/Ticket", request);
            var responseMsj = response.Content.ReadAsStringAsync();
        }
    }

    #endregion
}