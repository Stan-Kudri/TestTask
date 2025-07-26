using System.Globalization;
using TestTask.Core.Models.Products;
using TestTask.MudBlazors.Pages.Table.Model;

namespace TestTask.MudBlazors.Pages.Table.PageTableProvider
{
    public class ProductDetailProvider : ITableDetailProvider<Product>
    {
        private readonly ProductService _productService;

        public ProductDetailProvider(ProductService productService)
            => _productService = productService;

        public IReadOnlyList<ListTableColumn> Columns => new List<ListTableColumn>
        {
            new ListTableColumn("ID", 5, e => ((Product)e).Id),
            new ListTableColumn("Name", 15, e => ((Product)e).Name),
            new ListTableColumn("Company", 15, e => ((Product)e).Company),
            new ListTableColumn("Category", 15, e => ((Product)e).Category),
            new ListTableColumn("Type", 15, e => ((Product)e).Type),
            new ListTableColumn("Price", 15, e => ((Product)e).Price),
        };

        public IQueryable<Product> GetQueryableAll()
            => _productService.GetQueryableAll();

        public IQueryable<Product> GetSearchName(IQueryable<Product> items, string? searchString)
            => string.IsNullOrEmpty(searchString)
                ? items
                : items.Where(e => e.Name.Contains(searchString)
                                || e.Company.Name.Contains(searchString)
                                || e.Category.Name.Contains(searchString)
                                || e.Type.Name.Contains(searchString)
                                || e.Price.ToString(CultureInfo.InvariantCulture).Contains(searchString));

        public async Task Remove(int id) => await _productService.RemoveAsync(id);

        public async Task Upsert(Product entity) => await _productService.UpsertAsync(entity);
    }
}
