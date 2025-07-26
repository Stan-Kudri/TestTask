using TestTask.Core.Models.Categories;
using TestTask.MudBlazors.Pages.Table.Model;

namespace TestTask.MudBlazors.Pages.Table.PageTableProvider
{
    public class CategoryDetailProvider : ITableDetailProvider<Category>
    {
        private readonly CategoryService _categoryService;

        public CategoryDetailProvider(CategoryService categoryService)
            => _categoryService = categoryService;

        public IReadOnlyList<ListTableColumn> Columns => new List<ListTableColumn>
        {
            new ListTableColumn("ID", 25, e => ((Category)e).Id),
            new ListTableColumn("Name", 35, e => ((Category)e).Name),
        };

        public IQueryable<Category> GetQueryableAll()
            => _categoryService.GetQueryableAll();

        public IQueryable<Category> GetSearchName(IQueryable<Category> items, string? searchString)
            => string.IsNullOrEmpty(searchString)
                ? items
                : items.Where(e => e.Name.Contains(searchString));

        public async Task Remove(int id) => await _categoryService.RemoveAsync(id);

        public async Task Upsert(Category entity) => await _categoryService.UpsertAsync(entity);
    }
}
