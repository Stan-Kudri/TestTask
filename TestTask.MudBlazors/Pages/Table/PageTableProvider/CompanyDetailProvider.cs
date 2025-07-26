using TestTask.Core.Models.Companies;
using TestTask.MudBlazors.Pages.Table.Model;

namespace TestTask.MudBlazors.Pages.Table.PageTableView
{
    public class CompanyDetailProvider : ITableDetailProvider<Company>
    {
        private readonly CompanyService _companyService;

        public CompanyDetailProvider(CompanyService companyService)
            => _companyService = companyService;

        public IReadOnlyList<ListTableColumn> Columns => new List<ListTableColumn>
        {
            new ListTableColumn("ID", 5, e => ((Company)e).Id),
            new ListTableColumn("Name", 15, e => ((Company)e).Name),
            new ListTableColumn("DateCreation", 20, e => ((Company)e).DateCreation.ToString("d")),
            new ListTableColumn("Country", 25, e => ((Company)e).Country),
        };

        public IQueryable<Company> GetQueryableAll()
            => _companyService.GetQueryableAll();

        public IQueryable<Company> GetSearchName(IQueryable<Company> items, string? searchString)
            => string.IsNullOrEmpty(searchString)
                ? items
                : items.Where(e => e.Name.Contains(searchString)
                                || e.Country.Contains(searchString)
                                || e.DateCreation.ToString().Contains(searchString));

        public async Task Remove(int id) => await _companyService.RemoveAsync(id);

        public async Task Upsert(Company entity) => await _companyService.UpsertAsync(entity);
    }
}
