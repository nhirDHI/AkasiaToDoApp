using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Application.Features.ToDoActivity.Commands.CreateActivity;
using ToDo.Application.Features.ToDoActivity.Commands.UpdateActivity;
using ToDo.Application.Features.ToDoActivity.Queries.GetActivities;
using ToDo.Application.Features.ToDoActivity.Queries.GetActivityDetail;
using ToDo.Domain.Entities;

namespace ToDo.Application.MappingProfiles
{
    public class ToDoActivityProfiles : Profile
    {
        public ToDoActivityProfiles()
        {
            CreateMap<CreateActivityCommandDto, ToDoActivity>();
            CreateMap<UpdateActivityCommandDto, ToDoActivity>();
            CreateMap<GetActivitiesDto, ToDoActivity>().ReverseMap();
            CreateMap<GetActivityDetailDto, ToDoActivity>().ReverseMap();
        }
    }
}
