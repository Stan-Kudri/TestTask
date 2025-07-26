using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TestTask.Core.DBContext;
using TestTask.Core.Exeption;
using TestTask.Core.Models.Categories;

namespace TestTask.Core.Models.Types
{
    public class ProductTypeService(AppDbContext appDbContext)
        : BaseService<ProductType>(appDbContext, appDbContext.Type)
    {
        public override async Task AddAsync(ProductType item, CancellationToken cancellationToken = default)
        {
            BusinessLogicException.ThrowIfNull(item);

            if (await _dbSet.AnyAsync(e => e.Id == item.Id, cancellationToken))
            {
                BusinessLogicException.ThrowUniqueIDBusy<ProductType>(item.Id);
            }

            if (await appDbContext.Category.FirstOrDefaultAsync(e => e.Id == item.CategoryId, cancellationToken) == null)
            {
                throw NotFoundException.NotFoundIdProperty<Category>(item.CategoryId);
            }

            if (await _dbSet.AnyAsync(e => e.Name == item.Name, cancellationToken))
            {
                return;
            }

            await _dbSet.AddAsync(item, cancellationToken);
            await appDbContext.SaveChangesAsync(cancellationToken);
        }

        public override async Task UpdataAsync(ProductType item, CancellationToken cancellationToken = default)
        {
            BusinessLogicException.ThrowIfNull(item);

            if (!await appDbContext.Category.AnyAsync(e => e.Id == item.CategoryId, cancellationToken))
            {
                throw new BusinessLogicException("Category ID does not exist.");
            }

            var oldItem = _dbSet.FirstOrDefault(e => e.Id == item.Id)
                            ?? throw NotFoundException.NotFoundIdProperty<ProductType>(item.Id);

            UpdataItem(oldItem, item);
            await appDbContext.SaveChangesAsync(cancellationToken);
        }

        public override async Task UpsertAsync(ProductType item, CancellationToken cancellationToken = default)
        {
            if (await appDbContext.Category.FirstOrDefaultAsync(e => e.Id == item.CategoryId, cancellationToken) == null)
            {
                return;
            }

            var duplicateId = await _dbSet.FirstOrDefaultAsync(e => e.Id == item.Id, cancellationToken);
            if (duplicateId == null)
            {
                await AddAsync(item, cancellationToken);
                return;
            }

            await UpdataAsync(item, cancellationToken);
        }

        public override Task<List<ProductType>> GetAll(CancellationToken cancellationToken = default)
            => _dbSet.Include(e => e.Category).AsNoTracking().ToListAsync(cancellationToken);

        public override IQueryable<ProductType> GetQueryableAll()
            => _dbSet.Include(e => e.Category).Select(e => e);

        public List<ProductType> GetListTypesByCategory(int idCategory)
            => _dbSet.Where(e => e.CategoryId == idCategory).AsNoTracking().ToList();

        public async Task<bool> IsFreeName(string name, CancellationToken cancellationToken = default)
            => await _dbSet.FirstOrDefaultAsync(e => e.Name == name, cancellationToken) == null;

        public async Task<bool> IsFreeNameItemUpsert(ProductType item, CancellationToken cancellationToken = default)
        {
            var busyItem = await _dbSet.FirstOrDefaultAsync(e => e.Name == item.Name, cancellationToken);
            return busyItem == null || item.Id == busyItem.Id;
        }

        protected override void UpdataItem(ProductType oldItem, ProductType item)
        {
            oldItem.Name = item.Name;
            oldItem.CategoryId = item.CategoryId;
        }
    }
}
