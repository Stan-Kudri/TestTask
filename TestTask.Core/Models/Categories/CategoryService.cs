using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TestTask.Core.DBContext;
using TestTask.Core.Exeption;

namespace TestTask.Core.Models.Categories
{
    public class CategoryService(AppDbContext appDbContext)
        : BaseService<Category>(appDbContext, appDbContext.Category)
    {
        public override async Task AddAsync(Category item, CancellationToken cancellationToken = default)
        {
            BusinessLogicException.ThrowIfNull(item);

            if (await _dbSet.AnyAsync(e => e.Id == item.Id, cancellationToken))
            {
                BusinessLogicException.ThrowUniqueIDBusy<Category>(item.Id);
            }
            if (await _dbSet.AnyAsync(e => e.Name == item.Name, cancellationToken))
            {
                return;
            }

            await _dbSet.AddAsync(item, cancellationToken);
            await _appDbContext.SaveChangesAsync(cancellationToken);
        }

        public override async Task UpsertAsync(Category item, CancellationToken cancellationToken = default)
        {
            var duplicateId = await _dbSet.FirstOrDefaultAsync(e => e.Id == item.Id, cancellationToken);

            if (duplicateId == null)
            {
                await AddAsync(item, cancellationToken);
                return;
            }

            await UpdataAsync(item, cancellationToken);
        }

        public async Task<string> GetName(int id, CancellationToken cancellationToken = default)
            => (await _dbSet.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id, cancellationToken)).Name
            ?? throw NotFoundException.NotFoundIdProperty<Category>(id);

        public async Task<bool> IsFreeName(string name, CancellationToken cancellationToken = default)
            => await _dbSet.FirstOrDefaultAsync(e => e.Name == name, cancellationToken) == null;

        public async Task<bool> IsFreeNameItemUpsert(Category item, CancellationToken cancellationToken = default)
        {
            var busyItem = await _dbSet.FirstOrDefaultAsync(e => e.Name == item.Name, cancellationToken);
            return busyItem == null || item.Id == busyItem.Id;
        }

        protected override void UpdataItem(Category oldItem, Category item)
            => oldItem.Name = item.Name;
    }
}
