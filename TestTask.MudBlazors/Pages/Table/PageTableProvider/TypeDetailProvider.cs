using TestTask.Core.Models.Types;
using TestTask.MudBlazors.Pages.Table.Model;

namespace TestTask.MudBlazors.Pages.Table.PageTableProvider
{
    public class TypeDetailProvider : ITableDetailProvider<ProductType>
    {
        private readonly ProductTypeService _typeService;

        public TypeDetailProvider(ProductTypeService productTypeService)
            => _typeService = productTypeService;

        public IReadOnlyList<ListTableColumn> Columns => new List<ListTableColumn>
        {
            new ListTableColumn("ID", 10, e => ((ProductType)e).Id),
            new ListTableColumn("Name", 25, e => ((ProductType)e).Name),
            new ListTableColumn("Category", 35, e => ((ProductType)e).Category),
        };

        public IQueryable<ProductType> GetQueryableAll()
            => _typeService.GetQueryableAll();

        public IQueryable<ProductType> GetSearchName(IQueryable<ProductType> items, string? searchString)
            => string.IsNullOrEmpty(searchString)
                ? items
                : items.Where(e => e.Name.Contains(searchString)
                                || e.Category.Name.Contains(searchString));

        public async Task Remove(int id) => await _typeService.RemoveAsync(id);

        public async Task Upsert(ProductType entity) => await _typeService.UpsertAsync(entity);
    }
}
