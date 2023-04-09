using AutoMapper;
using Issues.Manager.Application.DTOs.Comment;
using Issues.Manager.Application.Models.User;
using Issues.Manager.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using TaskMaster.Application.TaskEntity.Dtos;

namespace Issues.Manager.Application.MappingConfigs;

public class MappingProfiles : Profile
{
	public MappingProfiles()
	{
		CreateMap<TaskEntity, TaskEntityDto>();
		CreateMap<TaskEntityDto, TaskEntity>();
		CreateMap<TaskCreateDto, TaskEntity>();

		//User
		CreateMap<UserRegisterModel, IdentityUser>();


		//Comments
		CreateMap<CreateCommentRequest, Comment>();
		CreateMap<Comment, CommentResponse>();
		CreateMap<CreateCommentRequest, CommentResponse>();


	}

}