using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Application.Contracts.Data;
using ToDo.Data.DatabaseContext;
using ToDo.Domain.Entities;

namespace ToDo.Data.Repositories
{
    public class ToDoActivityRepository : GenericRepository<ToDoActivity>, IToDoActivityRepository
    {
        public ToDoActivityRepository(ToDoContext context) : base(context)
        {
            
        }

        public async Task<ToDoActivity?> GetLatestActivityAsync()
        {
            return await _context.ToDoActivities.OrderByDescending(x => x.CreatedDate).FirstOrDefaultAsync();
        }
    }
}
