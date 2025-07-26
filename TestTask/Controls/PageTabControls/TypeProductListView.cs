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
using TestTask.Core.Models.Types;
using TestTask.Extension;
using TestTask.Forms.Type;
using TestTask.Messages;

namespace TestTask.Controls.PageTabControls
{
    public partial class TypeProductListView : UserControl, IListViewDataProvider, IInitialize, ILoad
    {
        private const int IndexId = 0;
        private const int IndexColumnTypeName = 1;
        private const int IndexColumnIdCategory = 3;

        private IServiceProvider _serviceProvider;
        private CategoryService _categoryRepository;
        private ProductTypeService _typeRepository;

        private IMessageBox _messageBox;
        private MessageByTable<ProductType> _messageByTable;
        private readonly SortTypeProductModel _selectSortField = new SortTypeProductModel();
        private bool _isAscending = true;

        public TypeProductListView() => InitializeComponent();

        public IReadOnlyList<ListViewColumn> Columns { get; } = new List<ListViewColumn>
        {
            new ListViewColumn("ID", 100, e => ((ProductType)e).Id),
            new ListViewColumn("Name", 350, e => ((ProductType)e).Name),
            new ListViewColumn("Category", 300, e => ((ProductType)e).Category),
            new ListViewColumn("CategoryId", 1, e => ((ProductType)e).CategoryId),
        };

        public void Initialize(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _categoryRepository = _serviceProvider.GetRequiredService<CategoryService>();
            _typeRepository = _serviceProvider.GetRequiredService<ProductTypeService>();
            _messageBox = _serviceProvider.GetRequiredService<IMessageBox>();
            _messageByTable = _serviceProvider.GetRequiredService<MessageByTable<ProductType>>();
            listView.Initialize(this, serviceProvider.GetRequiredService<IMessageBox>());
            checkCmbField.Items.AddRange(_selectSortField.SelectField);
            LoadData();
        }

        public void LoadData() => listView.LoadData();

        public async Task<bool> Add()
        {
            var listCategory = _categoryRepository.GetQueryableAll();

            if (!await _messageByTable.ShouldNotBeEmpty(listCategory))
            {
                return false;
            }

            using (var addForm = _serviceProvider.GetRequiredService<AddProductTypeForm>())
            {
                addForm.Initialize(listCategory.ToList());

#pragma warning disable CA1849 // Call async methods when in an async method
                if (addForm.ShowDialog() != DialogResult.OK)
                {
                    return false;
                }
#pragma warning restore CA1849 // Call async methods when in an async method

                var item = addForm.GetTypeProductModel().ToProductType();
                await _typeRepository.AddAsync(item);
            }

            return true;
        }

        public async Task<bool> Edit(Entity entity)
        {
            var listCategory = await _categoryRepository.GetAll();
            var oldItem = (ProductType)entity;

            using (var editForm = _serviceProvider.GetRequiredService<EditProductTypeForm>())
            {
                editForm.Initialize(listCategory, oldItem);

                var dialogResult = await editForm.FormShowDialogAsync();

                if (dialogResult != DialogResult.OK)
                {
                    return false;
                }

                var updateItem = editForm.GetEditTypeProduct();
                await _typeRepository.UpdataAsync(updateItem);
            }

            return true;
        }

        public Entity GetEntity(ListViewItem item)
        {
            var id = item.GetNonNullableString(IndexId).ParseInt();
            var name = item.GetNonNullableString(IndexColumnTypeName);
            var categoryId = item.GetNonNullableString(IndexColumnIdCategory).ParseInt();

            return new ProductType(name, categoryId, id);
        }

        public PagedList<Entity> GetPage(Page page)
        {
            var queriable = _typeRepository.GetQueryableAll();
            queriable = GetSearchType(queriable);
            queriable = _selectSortField.Apply(queriable, _isAscending);
            var result = queriable.GetPagedList(page);
            return new PagedList<Entity>(result, result.PageNumber, result.PageSize, result.TotalItems);
        }

        public async Task Remove(Entity entity) => await _typeRepository.RemoveAsync(entity.Id);

        private void ButtonUseFilter_Click(object sender, EventArgs e)
            => UsedFilter();

        private void ButtonClearFilter_Click(object sender, EventArgs e)
        {
            tbSearchStrName.Text = string.Empty;
            _selectSortField.SortFields = new HashSet<ProductTypeSortField>();
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

        private IQueryable<ProductType> GetSearchType(IQueryable<ProductType> items)
            => string.IsNullOrEmpty(tbSearchStrName.Text)
            ? items
            : items.Where(e => e.Name.Contains(tbSearchStrName.Text));

        private void SelectSortField()
        {
            var selectField = new HashSet<ProductTypeSortField>();
            foreach (string item in checkCmbField.Items)
            {
                var checkBoxItem = checkCmbField.CheckBoxItems[item];

                if (checkBoxItem.Checked && ProductTypeSortField.TryFromName(item, out var sortField))
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
