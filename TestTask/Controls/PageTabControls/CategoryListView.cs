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
using TestTask.Core.Models.Categories;
using TestTask.Core.Models.Page;
using TestTask.Extension;
using TestTask.Forms.Categories;

namespace TestTask.Controls.PageTabControls
{
    public partial class CategoryListView : UserControl, IListViewDataProvider, IInitialize, ILoad
    {
        private const int IndexId = 0;
        private const int IndexColumnName = 1;

        private IServiceProvider _serviceProvider;
        private CategoryService _categoryRepository;

        private readonly SortCategoryModel _selectSortField = new SortCategoryModel();
        private bool _isAscending = true;

        public CategoryListView() => InitializeComponent();

        public IReadOnlyList<ListViewColumn> Columns { get; } = new List<ListViewColumn>
        {
            new ListViewColumn("ID", 200, e => ((Category)e).Id),
            new ListViewColumn("Name", 555, e => ((Category)e).Name),
        };

        public void Initialize(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _categoryRepository = _serviceProvider.GetRequiredService<CategoryService>();
            listView.Initialize(this, serviceProvider.GetRequiredService<IMessageBox>());
            checkCmbField.Items.AddRange(_selectSortField.SelectField);
            LoadData();
        }

        public void LoadData() => listView.LoadData();

        public async Task<bool> Add()
        {
            using (var addForm = _serviceProvider.GetRequiredService<AddCategoryForm>())
            {
#pragma warning disable CA1849 // Call async methods when in an async method
                if (addForm.ShowDialog() != DialogResult.OK)
                {
                    return false;
                }
#pragma warning restore CA1849 // Call async methods when in an async method

                var item = addForm.GetItemModel().ToCategory();
                await _categoryRepository.AddAsync(item);
            }

            return true;
        }

        public async Task<bool> Edit(Entity entity)
        {
            var oldItem = (Category)entity;
            using (var editForm = _serviceProvider.GetRequiredService<EditCategoryForm>())
            {
                editForm.Initialize(oldItem);

                var dialogResult = await editForm.FormShowDialogAsync();

                if (dialogResult != DialogResult.OK)
                {
                    return false;
                }

                var updateItem = editForm.EditItem();
                await _categoryRepository.UpdataAsync(updateItem);
            }

            return true;
        }

        public Entity GetEntity(ListViewItem item)
        {
            var id = item.GetNonNullableString(IndexId).ParseInt();
            var name = item.GetNonNullableString(IndexColumnName) ?? throw new ArgumentException("Name cannot be null.");

            return new Category(name, id);
        }

        public PagedList<Entity> GetPage(Page page)
        {
            var queriable = _categoryRepository.GetQueryableAll();
            queriable = GetSearchName(queriable);
            queriable = _selectSortField.Apply(queriable, _isAscending);
            var result = queriable.GetPagedList(page);
            return new PagedList<Entity>(result, result.PageNumber, result.PageSize, result.TotalItems);
        }

        public async Task Remove(Entity entity) => await _categoryRepository.RemoveAsync(entity.Id);

        private void ButtonUseFilter_Click(object sender, EventArgs e)
            => UsedFilter();

        private void ButtonClearFilter_Click(object sender, EventArgs e)
        {
            tbSearchStrName.Text = string.Empty;
            _selectSortField.SortFields = new HashSet<CategoriesSortField>();
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

        private IQueryable<Category> GetSearchName(IQueryable<Category> items)
            => string.IsNullOrEmpty(tbSearchStrName.Text)
            ? items
            : items.Where(e => e.Name.Contains(tbSearchStrName.Text));

        private void SelectSortField()
        {
            var selectField = new HashSet<CategoriesSortField>();
            foreach (string item in checkCmbField.Items)
            {
                var checkBoxItem = checkCmbField.CheckBoxItems[item];

                if (checkBoxItem.Checked && CategoriesSortField.TryFromName(item, out var sortField))
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
