using Application.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class AsyncCommandRepository<T, TContext> : IAsyncCommandRepository<T>
                                            where T : class
                                            where TContext : DbContext
    {
        protected readonly TContext _dbContext;

        public AsyncCommandRepository(TContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task UpdateAsync(T entity)
        {
            _dbContext.Set<T>().Update(entity);
            return Task.CompletedTask;
        }

        public async Task AddAsync(T entity) => await _dbContext.Set<T>().AddAsync(entity);

        public async Task AddRangeAsync(IEnumerable<T> entities) => await _dbContext.Set<T>().AddRangeAsync(entities);

        public Task RemoveAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            return Task.CompletedTask;
        }

        public Task RemoveRangeAsync(IEnumerable<T> entities)
        {
            _dbContext.Set<T>().RemoveRange(entities);
            return Task.CompletedTask;
        }
    }
}
