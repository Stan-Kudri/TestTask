﻿using Microsoft.AspNetCore.Components;
using MudBlazor;
using TestTask.Core.Models.Categories;
using TestTask.Core.Models.Companies;
using TestTask.Core.Models.Products;
using TestTask.Core.Models.Types;
using TestTask.MudBlazors.Extension;
using TestTask.MudBlazors.Model;
using TestTask.MudBlazors.Model.TableComponent;

namespace TestTask.MudBlazors.Pages.Table.ItemTable
{
    public partial class ProductItemPage
    {
        [Inject] private ProductRepository ProductRepository { get; set; } = null!;
        [Inject] private CompanyRepository CompanyRepository { get; set; } = null!;
        [Inject] private CategoryRepository CategoryRepository { get; set; } = null!;
        [Inject] private ProductTypeRepository ProductTypeRepository { get; set; } = null!;
        [Inject] private IDialogService DialogService { get; set; } = null!;
        [Inject] private NavigationManager Navigation { get; set; } = null!;

        private ProductModel productModel { get; set; } = new ProductModel();
        private string[] errors = { };
        private bool isAddItem = true;

        private Product? oldProduct;

        private List<Company> selectCompanies = new List<Company>();
        private List<Category> selectCategories = new List<Category>();
        private List<ProductType> selectTypes = new List<ProductType>();

        [Parameter] public int? Id { get; set; } = null;

        protected override void OnInitialized()
        {
            selectCompanies = CompanyRepository.GetAll();
            selectCategories = CategoryRepository.GetAll();
            selectTypes = ProductTypeRepository.GetAll();

            if (Id == null)
            {
                isAddItem = true;
                return;
            }

            if (Id <= 0)
            {
                NavigationInTypeProductTable();
            }

            isAddItem = false;
            oldProduct = ProductRepository.GetItem((int)Id);
            productModel = oldProduct.GetProductModel();
        }

        private void Close() => NavigationInTypeProductTable();

        //Methods for add item type product
        private void Add()
        {
            if (errors.Length != 0)
            {
                return;
            }

            if (!ValidateFields(out var message))
            {
                ShowMessageWarning(message);
                return;
            }

            if (!ProductRepository.IsFreeName(productModel.Name))
            {
                ShowMessageWarning("Name is not free.");
                return;
            }

            var typeProduct = productModel.GetProductType();
            ProductRepository.Add(typeProduct);
            NavigationInTypeProductTable();
        }

        private void ClearData() => productModel.ClearData();

        //Methods for edit item type product
        private void Updata()
        {
            if (errors.Length != 0)
            {
                return;
            }

            if (!ValidateFields(out var message))
            {
                ShowMessageWarning(message);
                return;
            }

            var typeProduct = productModel.GetModifyType(oldProduct.Id);

            if (!oldProduct.Equals(typeProduct))
            {
                ProductRepository.Updata(typeProduct);
            }

            NavigationInTypeProductTable();
        }

        private void RecoverPastData() => productModel = oldProduct.GetProductModel();

        private void NavigationInTypeProductTable() => Navigation.NavigateTo($"/table/{TabTable.Product.ActiveTabIndex}");

        private async Task ShowMessageWarning(string message)
            => await DialogService.ShowMessageBox("Warning", message, yesText: "Ok");

        private IEnumerable<string> ValidFormatText(string str)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                yield return "Field is required.";
            }
        }

        private IEnumerable<string> ValidFormatPrice(string str)
        {
            if (!decimal.TryParse(str, out var value))
            {
                yield return "Field is required.";
            }
        }

        private bool ValidateFields(out string message)
        {
            message = string.Empty;

            if (productModel.Name == null || productModel.Name == string.Empty)
            {
                message = "Name is required.";
                return false;
            }

            if (productModel.Price <= 0)
            {
                message = "Price is required";
                return false;
            }

            if (productModel.Company == null)
            {
                message = "Company not selected.";
                return false;
            }

            if (productModel.Category == null)
            {
                message = "Category not selected.";
                return false;
            }

            if (productModel.ProductType == null)
            {
                message = "Product Type not selected.";
                return false;
            }

            return true;
        }
    }
}
