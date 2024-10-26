using Microsoft.EntityFrameworkCore;
using ToDo.Application.Contracts.Data;
using ToDo.Data.DatabaseContext;
using ToDo.Domain.Common;
using ToDo.Domain.Entities;

namespace ToDo.Data.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        protected readonly ToDoContext _context;

        public GenericRepository(ToDoContext context)
        {
            this._context = context;
        }

        public async Task CreateAsync(T entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        //public async Task DeleteAsync(int id)
        //{
        //    _context.Remove(id);
        //    await _context.SaveChangesAsync();
        //}

        public async Task DeleteAsync(T entity)
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<List<T>> GetAsync()
        {
            return await _context.Set<T>().AsNoTracking().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().AsNoTracking().FirstOrDefaultAsync(q => q.Id == id);
        }

        public async Task UpdateAsync(int id, T entity)
        {
            _context.Entry(entity).State = EntityState.Modified; // Gets or sets state that this entity is being tracked for ChangeTracking
            await _context.SaveChangesAsync();
        }
    }
}
