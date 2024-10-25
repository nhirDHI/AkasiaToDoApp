using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Domain.Common;

namespace ToDo.Domain.Entities
{
    public class ToDoActivity : BaseEntity
    {
        public string? ActivityCode { get; set; }
        public string? Subject { get; set; }
        public string? Description { get; set; }
        public bool? IsDone { get; set; }
        public bool? IsCanceled { get; set; }

    }
}
