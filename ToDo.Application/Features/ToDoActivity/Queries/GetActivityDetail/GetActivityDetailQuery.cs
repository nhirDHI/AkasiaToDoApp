using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo.Application.Features.ToDoActivity.Queries.GetActivityDetail
{
    public record GetActivityDetailQuery(int id) : IRequest<GetActivityDetailDto>;
}
