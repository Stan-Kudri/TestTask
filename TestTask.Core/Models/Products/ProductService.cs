using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NPOI.SS.Formula.Functions;
using TestTask.Core.DBContext;
using TestTask.Core.Exeption;
using TestTask.Core.Models.Categories;
using TestTask.Core.Models.Companies;
using TestTask.Core.Models.Types;

namespace TestTask.Core.Models.Products
{
    public class ProductService(AppDbContext appDbContext)
        : BaseService<Product>(appDbContext, appDbContext.Product)
    {
        public override async Task AddAsync(Product item, CancellationToken cancellationToken = default)
        {
            BusinessLogicException.ThrowIfNull(item);

            if (await _dbSet.AnyAsync(e => e.Id == item.Id, cancellationToken))
            {
                BusinessLogicException.ThrowUniqueIDBusy<Product>(item.Id);
            }

            if (await _dbSet.AnyAsync(e => e.Name == item.Name, cancellationToken))
            {
                return;
            }

            await InvalidDBForItemProduct(item, cancellationToken);
            await _dbSet.AddAsync(item, cancellationToken);
            await _appDbContext.SaveChangesAsync(cancellationToken);
        }

        public override async Task UpdataAsync(Product item, CancellationToken cancellationToken = default)
        {
            BusinessLogicException.ThrowIfNull(item);

            var oldItem = await _dbSet.FirstOrDefaultAsync(e => e.Id == item.Id, cancellationToken)
                                                    ?? throw NotFoundException.NotFoundIdProperty<Product>(item.Id);

            await InvalidDBForItemProduct(item, cancellationToken);
            UpdataItem(oldItem, item);
            await _appDbContext.SaveChangesAsync(cancellationToken);
        }

        public override async Task UpsertAsync(Product item, CancellationToken cancellationToken = default)
        {
            if (await _appDbContext.Company.FirstOrDefaultAsync(e => e.Id == item.CompanyId, cancellationToken) == null
                || await _appDbContext.Category.FirstOrDefaultAsync(e => e.Id == item.CategoryId, cancellationToken) == null
                || await _appDbContext.Type.FirstOrDefaultAsync(e => e.Id == item.TypeId, cancellationToken) == null)
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

        public override Task<List<Product>> GetAll(CancellationToken cancellationToken = default)
            => _dbSet.Include(e => e.Company).Include(e => e.Category).ThenInclude(e => e.Types).AsNoTracking().ToListAsync(cancellationToken);

        public override IQueryable<Product> GetQueryableAll()
            => _dbSet.Include(e => e.Company).Include(e => e.Category).ThenInclude(e => e.Types).AsNoTracking();

        public override async Task<Product> GetItem(int id, CancellationToken cancellationToken = default)
            => await _dbSet.Include(e => e.Company).Include(e => e.Category).ThenInclude(e => e.Types).FirstOrDefaultAsync(e => e.Id == id, cancellationToken)
            ?? throw NotFoundException.NotFoundIdProperty<T>(id);

        public async Task<bool> IsFreeName(string name, CancellationToken cancellationToken = default)
            => await _dbSet.FirstOrDefaultAsync(e => e.Name == name, cancellationToken) == null;

        public async Task<bool> IsFreeNameItemUpsert(Product item, CancellationToken cancellationToken = default)
        {
            var busyItem = await _dbSet.FirstOrDefaultAsync(e => e.Name == item.Name, cancellationToken);
            return busyItem == null || item.Id == busyItem.Id;
        }

        private async Task InvalidDBForItemProduct(Product item, CancellationToken cancellationToken = default)
        {
            if (!await _appDbContext.Company.AnyAsync(e => e.Id == item.CompanyId, cancellationToken))
            {
                throw NotFoundException.NotFoundIdProperty<Company>(item.CompanyId);
            }

            if (!await _appDbContext.Category.AnyAsync(e => e.Id == item.CategoryId, cancellationToken))
            {
                throw NotFoundException.NotFoundIdProperty<Category>(item.CategoryId);
            }

            if (!await _appDbContext.Type.AnyAsync(e => e.Id == item.TypeId, cancellationToken))
            {
                throw NotFoundException.NotFoundIdProperty<ProductType>(item.TypeId);
            }
        }

        protected override void UpdataItem(Product oldItem, Product item)
        {
            oldItem.Name = item.Name;
            oldItem.CompanyId = item.CompanyId;
            oldItem.CategoryId = item.CategoryId;
            oldItem.TypeId = item.TypeId;
            oldItem.Price = item.Price;
            oldItem.Destination = item.Destination;
        }
    }
}
