using Issues.Manager.Application.DTOs;
using Issues.Manager.Application.Models.Issue;

namespace Issues.Manager.Application.Interfaces;

public interface IIssueService
{
    TicketDetailsModel Create(TicketCreateRequest request);
    TicketDetailsModel GetById(int id , bool trackChanges = false);
    IEnumerable<TicketDetailsModel> GetAll(TicketFilters ticketFilters,bool trackChanges = false);
    void Update(int id, TicketUpdateRequest updateRequest);
    void Delete(int id);
}