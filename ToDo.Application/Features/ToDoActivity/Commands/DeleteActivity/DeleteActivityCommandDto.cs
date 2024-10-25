using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo.Application.Features.ToDoActivity.Commands.DeleteActivity
{
    public class DeleteActivityCommandDto : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
