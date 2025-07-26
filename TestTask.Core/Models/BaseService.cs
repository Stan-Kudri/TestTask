using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TestTask.Core.DBContext;
using TestTask.Core.Exeption;

namespace TestTask.Core.Models
{
    public abstract class BaseService<T>(AppDbContext appDbContext, DbSet<T> dbSet)
        where T : Entity
    {
        protected readonly AppDbContext _appDbContext = appDbContext;
        protected readonly DbSet<T> _dbSet = dbSet;

        public virtual async Task AddAsync(T item, CancellationToken cancellationToken = default)
        {
            BusinessLogicException.ThrowIfNull(item);

            if (await _dbSet.AnyAsync(e => e.Id == item.Id, cancellationToken))
            {
                BusinessLogicException.ThrowUniqueIDBusy<T>(item.Id);
            }

            await _dbSet.AddAsync(item, cancellationToken);
            await _appDbContext.SaveChangesAsync(cancellationToken);
        }

        public virtual async Task UpdataAsync(T item, CancellationToken cancellationToken = default)
        {
            BusinessLogicException.ThrowIfNull(item);

            var oldItem = await _dbSet.FirstOrDefaultAsync(e => e.Id == item.Id, cancellationToken)
                          ?? throw NotFoundException.NotFoundIdProperty<T>(item.Id);
            UpdataItem(oldItem, item);
            await _appDbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task RemoveAsync(int id, CancellationToken cancellationToken = default)
        {
            var item = await _dbSet.FirstOrDefaultAsync(e => e.Id == id, cancellationToken)
                            ?? throw NotFoundException.NotFoundIdProperty<T>(id);

            _dbSet.Remove(item);
            await _appDbContext.SaveChangesAsync(cancellationToken);
        }

        public abstract Task UpsertAsync(T item, CancellationToken cancellationToken = default);

        public async Task AddRangeAsync(List<T> items, CancellationToken cancellationToken = default)
        {
            foreach (var item in items)
            {
                await AddAsync(item, cancellationToken);
            }
        }

        public async Task RemoveRangeAsync(List<int> listId, CancellationToken cancellationToken = default)
        {
            foreach (var id in listId)
            {
                await RemoveAsync(id, cancellationToken);
            }
        }

        public virtual Task<List<T>> GetAll(CancellationToken cancellationToken = default)
            => _dbSet.AsNoTracking().ToListAsync(cancellationToken);

        public virtual IQueryable<T> GetQueryableAll() => _dbSet.AsNoTracking();

        public virtual async Task<T> GetItem(int id, CancellationToken cancellationToken = default)
            => await _dbSet.FirstOrDefaultAsync(e => e.Id == id, cancellationToken)
            ?? throw NotFoundException.NotFoundIdProperty<T>(id);

        protected abstract void UpdataItem(T oldItem, T item);
    }
}
