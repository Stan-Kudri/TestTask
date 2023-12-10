﻿using Microsoft.AspNetCore.Components;
using MudBlazor;
using TestTask.Core.Models;
using TestTask.Core.Models.Categories;
using TestTask.MudBlazors.Extension;
using TestTask.MudBlazors.Model;

namespace TestTask.MudBlazors.Pages.Table.Categories
{
    public partial class CategoryPage
    {
        [Inject] CategoryService CategoryService { get; set; }
        [Inject] IDialogService DialogService { get; set; }
        [Inject] NavigationManager Navigation { get; set; }

        private const string MessageNotSelectedItem = "No items selected";
        private const int NoItemsSelected = 0;

        private IEnumerable<Category>? categories;
        private HashSet<Category> selectedItems = new HashSet<Category>();

        private int totalItems;
        private string? searchString = null;

        private string valueTypeSort { get; set; } = TypeSortField.NoSorting;
        private bool isNotDisableFilter => valueTypeSort == TypeSortField.NoSorting || valueTypeSort == null ? true : false;

        private SortCategories sortField = new SortCategories();
        private TypeSortField typeSortField = new TypeSortField();
        private PageModel pageModel = new PageModel();

        protected override void OnInitialized() => LoadData();

        private void AddCategoryPage() => Navigation.NavigateTo("/addcategory");

        private void EditCategpryPage(int id) => Navigation.NavigateTo($"editcategory/{id}");

        private async Task Update()
        {
            if (selectedItems.Count <= NoItemsSelected)
            {
                await ShowMessageWarning(MessageNotSelectedItem);
                return;
            }

            if (selectedItems.Count > 1)
            {
                await ShowMessageWarning("Select one item.");
                return;
            }

            EditCategpryPage(selectedItems.ToArray()[0].Id);
        }

        private async Task Remove()
        {
            if (selectedItems.Count <= NoItemsSelected)
            {
                await ShowMessageWarning(MessageNotSelectedItem);
                return;
            }

            bool? result = await DialogService.ShowMessageBox(
                "Warning",
                "Delete selecte items?",
                yesText: "Yes", cancelText: "No");

            if (result != true)
            {
                return;
            }

            foreach (var item in selectedItems)
            {
                CategoryService.Remove(item.Id);
            }

            LoadData();
        }

        private void Update(int id) => EditCategpryPage(id);

        private async void Remove(int id)
        {
            bool? result = await DialogService.ShowMessageBox(
               "Warning",
               "Delete selecte items?",
               yesText: "Yes", cancelText: "No");

            if (result != true)
            {
                return;
            }

            CategoryService.Remove(id);
            LoadData();
        }

        private void UseFilter()
        {
            typeSortField.SetSort(valueTypeSort);
            LoadData();
        }

        private void ClearFilter()
        {
            valueTypeSort = TypeSortField.NoSorting;
            typeSortField.SetSort(valueTypeSort);
            LoadData();
        }

        private void OnSearch(string text)
        {
            searchString = text;
            LoadData();
        }

        private void LoadData()
        {
            IQueryable<Category> queriable = CategoryService.GetQueryableAll();
            queriable = GetSearchName(queriable);
            queriable = sortField.Apply(queriable, typeSortField.IsAscending);
            var result = queriable.GetPagedList<Category>(pageModel);
            categories = result.Items;
            StateHasChanged();
        }

        private IQueryable<Category> GetSearchName(IQueryable<Category> items)
                => string.IsNullOrEmpty(searchString)
                ? items
                : items.Where(e => e.Name.Contains(searchString));

        private Task ShowMessageWarning(string message)
        {
            return DialogService.ShowMessageBox(
                    "Warning",
                     message,
                     yesText: "Ok"
                );
        }
    }
}