﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo.Application.Features.ToDoActivity.Queries.GetActivities
{
    public record GetActivitiesQuery : IRequest<List<GetActivitiesDto>>;
}
