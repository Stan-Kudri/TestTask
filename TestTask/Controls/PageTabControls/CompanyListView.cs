using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;
using TestTask.BindingItem.Pages;
using TestTask.BindingItem.Pages.Sort;
using TestTask.Controls.PageTabControls.Model;
using TestTask.Core;
using TestTask.Core.Models;
using TestTask.Core.Models.Companies;
using TestTask.Core.Models.Page;
using TestTask.Extension;
using TestTask.Forms.Companies;

namespace TestTask.Controls.PageTabControls
{
    public partial class CompanyListView : UserControl, IListViewDataProvider, IInitialize, ILoad
    {
        private const int IndexId = 0;
        private const int IndexColumnName = 1;
        private const int IndexColumnDataCreate = 2;
        private const int IndexColumnCountry = 3;

        private IServiceProvider _serviceProvider;
        private CompanyService _companyRepository;

        private readonly SortCompanyModel _selectSortField = new SortCompanyModel();
        private bool _isAscending = true;

        public CompanyListView() => InitializeComponent();

        public IReadOnlyList<ListViewColumn> Columns { get; } = new List<ListViewColumn>
        {
            new ListViewColumn("ID", 100, e => ((Company)e).Id),
            new ListViewColumn("Name", 300, e => ((Company)e).Name),
            new ListViewColumn("DateCreation", 155, e => ((Company)e).DateCreation.ToString("d")),
            new ListViewColumn("Country", 200, e => ((Company)e).Country),
        };

        public void Initialize(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _companyRepository = _serviceProvider.GetRequiredService<CompanyService>();
            listView.Initialize(this, serviceProvider.GetRequiredService<IMessageBox>());
            checkCmbField.Items.AddRange(_selectSortField.SelectField);
            LoadData();
        }

        public void LoadData() => listView.LoadData();

        public async Task<bool> Add()
        {
            using (var addForm = _serviceProvider.GetRequiredService<AddItemCompanyForm>())
            {
#pragma warning disable CA1849 // Call async methods when in an async method
                if (addForm.ShowDialog() != DialogResult.OK)
                {
                    return false;
                }
#pragma warning restore CA1849 // Call async methods when in an async method

                var item = addForm.GetCompanyModel().ToCompany();
                await _companyRepository.AddAsync(item);
            }

            return true;
        }

        public async Task<bool> Edit(Entity entity)
        {
            var oldItem = (Company)entity;
            using (var editForm = _serviceProvider.GetRequiredService<EditItemCompanyForm>())
            {
                editForm.Initialize(oldItem);

                var dialogResult = await editForm.FormShowDialogAsync();

                if (dialogResult != DialogResult.OK)
                {
                    return false;
                }

                var updateItem = editForm.GetEditCompany();
                await _companyRepository.UpdataAsync(updateItem);
            }

            return true;
        }

        public Entity GetEntity(ListViewItem item)
        {
            var id = item.GetNonNullableString(IndexId).ParseInt();
            var name = item.GetNonNullableString(IndexColumnName) ?? throw new ArgumentException("Name cannot be null.");
            var strDateCreation = item.GetNonNullableString(IndexColumnDataCreate);
            DateTime dateCreation = strDateCreation != null ? DateTime.Parse(strDateCreation) : throw new ArgumentException("Data create cannot be null.");
            var country = item.GetNonNullableString(IndexColumnCountry) ?? throw new ArgumentException("Country cannot be null.");

            return new Company(name, dateCreation, country, id);
        }

        public PagedList<Entity> GetPage(Page page)
        {
            var queriable = _companyRepository.GetQueryableAll();
            queriable = GetSearchName(queriable);
            queriable = _selectSortField.Apply(queriable, _isAscending);
            var result = queriable.GetPagedList(page);
            return new PagedList<Entity>(result, result.PageNumber, result.PageSize, result.TotalItems);
        }

        public async Task Remove(Entity entity) => await _companyRepository.RemoveAsync(entity.Id);

        private void ButtonUseFilter_Click(object sender, EventArgs e)
            => UsedFilter();

        private void BtnClearFilter_Click(object sender, EventArgs e)
        {
            tbSearchStrName.Text = string.Empty;
            _selectSortField.SortFields = new HashSet<CompanySortField>();
            checkCmbField.ClearSelection();
            _isAscending = true;
            btnTypeSort.Text = TypeSortFields.Ascending.Name;
            LoadData();
        }

        private void BtnTypeSort_Click(object sender, EventArgs e)
        {
            if (_isAscending)
            {
                _isAscending = false;
                btnTypeSort.Text = TypeSortFields.Descending.Name;
            }
            else
            {
                _isAscending = true;
                btnTypeSort.Text = TypeSortFields.Ascending.Name;
            }

            UsedFilter();
        }

        private void ListView_SizeChanged(object sender, EventArgs e)
            => listView.ChangeSizeColumnListView();

        private IQueryable<Company> GetSearchName(IQueryable<Company> items)
            => string.IsNullOrEmpty(tbSearchStrName.Text)
            ? items
            : items.Where(e => e.Name.Contains(tbSearchStrName.Text) || e.Country.Contains(tbSearchStrName.Text) || e.DateCreation.ToString().Contains(tbSearchStrName.Text));

        private void SelectSortField()
        {
            var selectField = new HashSet<CompanySortField>();
            foreach (string item in checkCmbField.Items)
            {
                var checkBoxItem = checkCmbField.CheckBoxItems[item];

                if (checkBoxItem.Checked && CompanySortField.TryFromName(item, out var sortField))
                {
                    selectField.Add(sortField);
                }
            }
            _selectSortField.SortFields = selectField;
        }

        private void UsedFilter()
        {
            SelectSortField();
            LoadData();
        }
    }
}
