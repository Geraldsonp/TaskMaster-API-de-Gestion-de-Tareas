using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.Xunit2;
using FluentAssertions;
using Issues.Manager.Api.Contracts;
using Issues.Manager.Application.DTOs;
using Issues.Manager.Application.Models.Issue;
using Issues.Manager.Domain.Enums;
using Issues.Manager.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TicketManager.Test.Configuration;
using Xunit;

namespace TicketManager.Test.Controllers;

public class TicketControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _httpClient;
    private readonly AppDbContext? _context;
    private readonly IdentityUser? _user;

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
        var scopedServiceProvider = factory.Services.CreateScope();
        var identityHelper = new IdentityHelper(_httpClient);
        var token =  identityHelper.AuthenticateUser();
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Result);
        _context = scopedServiceProvider.ServiceProvider.GetService<AppDbContext>();
        _user = _context?.Users.FirstOrDefault(x => x.Email == "Testing@Email.com");
    }

    [Fact]
    public async Task CreateTicket_ReturnsBadRequest_WhenInvalidData()
    {
        //Arrange
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
        if (_user?.Id != null)
        {

            await CreateTicketsForCurrentUser(_user?.Id, 3);
        }

        var tasksCount = _context.Tickets.ToList().Count;

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
        _httpClient.DefaultRequestHeaders.Clear();

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
        if (_user?.Id != null)
            await CreateTicketsForCurrentUser(_user?.Id, 10);
        var queryParameters = new TicketFilterQueryParameters()
        {
            Priority = Priority.High
        };

        var tasks = _context.Tickets.ToList().Where(task => task.Priority == queryParameters.Priority);
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
         if (_user?.Id != null)
            await CreateTicketsForCurrentUser(_user?.Id, 10);

        var tasks = _context.Tickets.ToList().Where(task => task.TicketType == TicketType.Feature);
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
        if (_user?.Id != null)
            await CreateTicketsForCurrentUser(_user?.Id, 10);

        var tasks = _context.Tickets.ToList().Where(task => task.TicketType == TicketType.Bug && task.Priority == Priority.Low);
        var tasksCount = tasks.Count();

        //Act
        var response = await _httpClient.GetAsync("api/Ticket?ticketType=Bug&priority=Low");
        var responseString = response.Content.ReadAsStringAsync();
        var tickets = await response.Content.ReadFromJsonAsync<IEnumerable<TicketDetailsModel>>();

        //assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        tickets.Count().Should().Be(tasksCount);
    }

    [Fact]
    public async Task GetTicketById_WhenGivenValidId_Return200OkAndTicket()
    {
        //Arrange
        if (_user?.Id != null)
            await CreateTicketsForCurrentUser(_user?.Id, 4);

        //Act
        var response = await _httpClient.GetAsync($"api/Ticket/{4}");
        var responseString = response.Content.ReadAsStringAsync();
        var taskObject = await response.Content.ReadFromJsonAsync<TicketDetailsModel>();

        //Assert
        response.IsSuccessStatusCode.Should().BeTrue();
        taskObject.Id.Should().Be(4);
    }

    [Theory, AutoData]
    public async Task UpdateTicket_WhenGivenValidData_UpdatesModelReturns200Ok(string description, TicketType ticketType, Priority priority, string title )
    {
        //Arrange
        await CreateTicketsForCurrentUser(_user.Id, 3);
        var ticketDetails = await _httpClient.GetFromJsonAsync<TicketDetailsModel>($"api/Ticket/{2}");
        var updateRequest = new TicketUpdateRequest()
        {
            Description = description,
            TicketType = ticketType,
            Priority = priority,
            Title = title
        };

        //Act
        var response = await _httpClient.PutAsJsonAsync($"api/Ticket/{2}", updateRequest);

        var getResponse = await _httpClient.GetAsync($"api/Ticket/{2}");
        getResponse.IsSuccessStatusCode.Should().BeTrue();
        var ticketDetailsUpdated = await getResponse.Content.ReadFromJsonAsync<TicketDetailsModel>();

        ticketDetailsUpdated.TicketType.Should().Be(ticketType);
        ticketDetailsUpdated.Priority.Should().Be(priority);
        ticketDetailsUpdated.Description.Should().Be(description);
        ticketDetailsUpdated.Title.Should().Be(title);
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task DeleteTicket_WhenGivenValidId_Return200Ok()
    {
        //Arrange
        var ticketsCount = 4;

        if (_user?.Id != null)
            await CreateTicketsForCurrentUser(_user?.Id, ticketsCount);

        //Act
        var deleteResponse = await _httpClient.DeleteAsync($"api/Ticket/{4}");
        var responseString = deleteResponse.Content.ReadAsStringAsync();

        var getTicketsResponse = await _httpClient.GetAsync("api/Ticket");
        var tickets = await getTicketsResponse.Content.ReadFromJsonAsync<IEnumerable<TicketDetailsModel>>();

        //Assert
        deleteResponse.IsSuccessStatusCode.Should().BeTrue();
        tickets.Count().Should().Be(ticketsCount - 1);
    }

    [Fact]
    public async Task DeleteTicket_WhenGivenInValidId_Return404NotFound()
    {
        //Arrange
        var ticketsCount = 4;

        if (_user?.Id != null)
            await CreateTicketsForCurrentUser(_user?.Id, ticketsCount);

        //Act
        var deleteResponse = await _httpClient.DeleteAsync($"api/Ticket/{5}");
        var responseString = deleteResponse.Content.ReadAsStringAsync();

        var getTicketsResponse = await _httpClient.GetAsync("api/Ticket");
        var tickets = await getTicketsResponse.Content.ReadFromJsonAsync<IEnumerable<TicketDetailsModel>>();

        //Assert
        deleteResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
        tickets.Count().Should().Be(ticketsCount);
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