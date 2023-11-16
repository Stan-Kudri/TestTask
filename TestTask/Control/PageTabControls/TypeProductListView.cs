﻿using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using TestTask.BindingItem.Pages.Products;
using TestTask.BindingItem.Pages.TypeProduct;
using TestTask.Control.PageTabControls.Model;
using TestTask.Core;
using TestTask.Core.Models;
using TestTask.Core.Models.Companies;
using TestTask.Core.Models.Page;
using TestTask.Core.Models.Types;
using TestTask.Extension;
using TestTask.Forms.Type;

namespace TestTask.Control.PageTabControls
{
    public partial class TypeProductListView : UserControl, IListViewDataProvider, IInitialize, ILoad
    {
        private const int IndexId = 0;
        private const int IndexColumnTypeName = 1;
        private const int IndexColumnIdCategory = 2;

        private IServiceProvider _serviceProvider;
        private CategoryService _categoryService;
        private ProductTypeService _typeService;
        private IMessageBox _messageBox;
        private SortTypeProduct _sortType = new SortTypeProduct();

        public TypeProductListView() => InitializeComponent();

        public IReadOnlyList<ListViewColumn> Columns { get; } = new List<ListViewColumn>
        {
            new ListViewColumn("ID", 100, e => ((ProductType)e).Id),
            new ListViewColumn("Name", 355, e => ((ProductType)e).Name),
            new ListViewColumn("Category", 300, e => ((ProductType)e).Category),
        };

        public void Initialize(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _categoryService = _serviceProvider.GetRequiredService<CategoryService>();
            _typeService = _serviceProvider.GetRequiredService<ProductTypeService>();
            _messageBox = _serviceProvider.GetRequiredService<IMessageBox>();
            listView.Initialize(this, serviceProvider.GetRequiredService<IMessageBox>());
            cmbSortField.DataSource = _sortType.Items;
            cmbSortField.SelectedItem = _sortType.SortField;
        }

        public void LoadData() => listView.LoadData();

        public bool Add()
        {
            var listCategory = _categoryService.GetQueryableAll();

            if (listCategory.Count() == 0)
            {
                _messageBox.ShowWarning("Add a category to the table to add a product.");
                return false;
            }

            using (var addForm = _serviceProvider.GetRequiredService<AddProductTypeForm>())
            {
                addForm.Initialize(listCategory.ToList());

                if (addForm.ShowDialog() != DialogResult.OK)
                {
                    return false;
                }

                var item = addForm.GetTypeProductModel().ToProductType();
                _typeService.Add(item);
            }

            return true;
        }

        public bool Edit(Entity entity)
        {
            var listCategory = _categoryService.GetAll();
            var oldItem = (ProductType)entity;

            using (var editForm = _serviceProvider.GetRequiredService<EditProductTypeForm>())
            {
                editForm.Initialize(listCategory, oldItem);

                if (editForm.ShowDialog() != DialogResult.OK)
                {
                    return false;
                }

                var updateItem = editForm.GetEditTypeProduct();
                _typeService.Update(updateItem);
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
            var queriable = _typeService.GetQueryableAll();
            queriable = GetSearchType(queriable);
            queriable = _sortType.Apply(queriable);
            var result = queriable.GetPagedList(page);
            return new PagedList<Entity>(result, result.PageNumber, result.PageSize, result.TotalItems);
        }

        public void Remove(Entity entity)
            => _typeService.Remove(entity.Id);


        private void ButtonUseFilter_Click(object sender, EventArgs e) => LoadData();

        private void ButtonClearFilter_Click(object sender, EventArgs e)
        {
            cmbSortField.SelectedItem = SortProducts.NoSorting;
            _sortType.SortField = cmbSortField.SelectedItem.ToString();
            tbSearchStrName.Text = string.Empty;
            LoadData();
        }

        private void CmbSortField_Changed(object sender, EventArgs e)
        {
            _sortType.SortField = cmbSortField.SelectedItem.ToString();
            LoadData();
        }

        private void ListView_SizeChanged(object sender, EventArgs e)
            => listView.ChangeSizeColumnListView();

        private IQueryable<ProductType> GetSearchType(IQueryable<ProductType> items)
            => string.IsNullOrEmpty(tbSearchStrName.Text)
            ? items
            : items.Where(e => e.Name.Contains(tbSearchStrName.Text));
    }
}