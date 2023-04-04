using AutoMapper;
using Issues.Manager.Application.DTOs;
using Issues.Manager.Application.DTOs.Comment;
using Issues.Manager.Application.Models.Issue;
using Issues.Manager.Application.Models.User;
using Issues.Manager.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Issues.Manager.Application.MappingConfigs;

public class MappingProfiles : Profile
{
	public MappingProfiles()
	{
		CreateMap<Ticket, TicketDetailsModel>();
		CreateMap<TicketDetailsModel, Ticket>();
		CreateMap<TicketCreateRequest, Ticket>();

		//User
		CreateMap<UserRegisterModel, IdentityUser>();


		//Comments
		CreateMap<CreateCommentRequest, Comment>();
		CreateMap<Comment, CommentResponse>();
		CreateMap<CreateCommentRequest, CommentResponse>();


	}

}