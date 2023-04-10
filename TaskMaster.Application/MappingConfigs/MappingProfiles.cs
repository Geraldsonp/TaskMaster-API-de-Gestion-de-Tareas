using AutoMapper;
using Microsoft.AspNetCore.Identity;
using TaskMaster.Application.Models.Comment;
using TaskMaster.Application.Models.User;
using TaskMaster.Application.TaskEntity.Dtos;
using TaskMaster.Domain.Entities;

namespace TaskMaster.Application.MappingConfigs;

public class MappingProfiles : Profile
{
	public MappingProfiles()
	{
		CreateMap<Domain.Entities.TaskEntity, TaskEntityDto>();
		CreateMap<TaskEntityDto, Domain.Entities.TaskEntity>();
		CreateMap<TaskCreateDto, Domain.Entities.TaskEntity>();

		//User
		CreateMap<UserRegisterModel, IdentityUser>();


		//Comments
		CreateMap<CreateCommentRequest, Comment>();
		CreateMap<Comment, CommentResponse>();
		CreateMap<CreateCommentRequest, CommentResponse>();


	}

}