using TestTask.Core.Models.Companies;
using TestTask.MudBlazors.Pages.Table.Model;

namespace TestTask.MudBlazors.Pages.Table.PageTableView
{
    public class CompanyDetailProvider : ITableDetailProvider<Company>
    {
        private readonly CompanyRepository _companyRepository;

        public CompanyDetailProvider(CompanyRepository companyRepository)
            => _companyRepository = companyRepository;

        public IReadOnlyList<ListTableColumn> Columns => new List<ListTableColumn>
        {
            new ListTableColumn("ID", 5, e => ((Company)e).Id),
            new ListTableColumn("Name", 15, e => ((Company)e).Name),
            new ListTableColumn("DateCreation", 20, e => ((Company)e).DateCreation.ToString("d")),
            new ListTableColumn("Country", 25, e => ((Company)e).Country),
        };

        public IQueryable<Company> GetQueryableAll()
            => _companyRepository.GetQueryableAll();

        public IQueryable<Company> GetSearchName(IQueryable<Company> items, string? searchString)
            => string.IsNullOrEmpty(searchString)
                ? items
                : items.Where(e => e.Name.Contains(searchString)
                                || e.Country.Contains(searchString)
                                || e.DateCreation.ToString().Contains(searchString));

        public async Task Remove(int id) => await _companyRepository.RemoveAsync(id);

        public async Task Upsert(Company entity) => await _companyRepository.UpsertAsync(entity);
    }
}
