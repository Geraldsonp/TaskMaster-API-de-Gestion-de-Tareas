using Issues.Manager.Application.DTOs;
using Issues.Manager.Application.Models.Issue;
using TaskMaster.Domain.ValueObjects;

namespace Issues.Manager.Application.Interfaces;

public interface IIssueService
{
	TicketDetailsModel Create(TicketCreateRequest request);
	TicketDetailsModel GetById(int id);
	IEnumerable<TicketDetailsModel> GetAll(TicketFilters ticketFilters, Paggination paggination);
	void Update(int id, TicketUpdateRequest updateRequest);
	void Delete(int id);
}