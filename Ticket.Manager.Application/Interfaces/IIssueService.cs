using Issues.Manager.Application.DTOs;

namespace Issues.Manager.Application.Services;

public interface IIssueService
{
    TicketDetailsModel Create(CreateIssueRequest issueRequest);
    TicketDetailsModel GetById(int id , bool trackChanges = false);
    IEnumerable<TicketDetailsModel> GetAll(TicketFilters ticketFilters,bool trackChanges = false);
    TicketDetailsModel Update(TicketDetailsModel ticketDetailsModel);
    void Delete(int id);
}