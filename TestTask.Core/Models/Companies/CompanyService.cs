using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TestTask.Core.DBContext;
using TestTask.Core.Exeption;

namespace TestTask.Core.Models.Companies
{
    public class CompanyService(AppDbContext appDbContext)
        : BaseService<Company>(appDbContext, appDbContext.Company)
    {
        public override async Task AddAsync(Company item, CancellationToken cancellationToken = default)
        {
            BusinessLogicException.ThrowIfNull(item);

            if (await _dbSet.AnyAsync(e => e.Id == item.Id, cancellationToken))
            {
                BusinessLogicException.ThrowUniqueIDBusy<Company>(item.Id);
            }
            if (await _dbSet.AnyAsync(e => e.Name == item.Name, cancellationToken))
            {
                return;
            }

            await _dbSet.AddAsync(item, cancellationToken);
            await _appDbContext.SaveChangesAsync(cancellationToken);
        }

        public override async Task UpsertAsync(Company item, CancellationToken cancellationToken = default)
        {
            var duplicateId = _dbSet.FirstOrDefault(e => e.Id == item.Id);

            if (duplicateId == null)
            {
                await AddAsync(item, cancellationToken);
                return;
            }

            await UpdataAsync(item, cancellationToken);
        }

        public async Task<string> CompanyName(int id, CancellationToken cancellationToken = default)
            => (await _dbSet.FirstOrDefaultAsync(e => e.Id == id, cancellationToken)).Name
            ?? throw NotFoundException.NotFoundIdProperty<Company>(id);

        public async Task<bool> IsFreeName(string name, CancellationToken cancellationToken = default)
            => await _dbSet.FirstOrDefaultAsync(e => e.Name == name, cancellationToken) == null;

        public async Task<bool> IsFreeNameItemUpsert(Company item, CancellationToken cancellationToken = default)
        {
            var busyItem = await _dbSet.FirstOrDefaultAsync(e => e.Name == item.Name, cancellationToken);
            return busyItem == null || item.Id == busyItem.Id;
        }

        protected override void UpdataItem(Company oldItem, Company item)
        {
            oldItem.Name = item.Name;
            oldItem.DateCreation = item.DateCreation;
            oldItem.Country = item.Country;
        }
    }
}
