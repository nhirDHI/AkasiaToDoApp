using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Domain.Common;

namespace ToDo.Application.Features.ToDoActivity.Queries.GetActivityDetail
{
    public class GetActivityDetailDto : BaseEntity
    {
        public string? Subject { get; set; }
        public string? Description { get; set; }
        public bool? IsDone { get; set; } = false;
        public bool? IsCanceled { get; set; } = false;
    }
}
