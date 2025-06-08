using Microsoft.AspNetCore.Components;
using MudBlazor;
using TestTask.Core;
using TestTask.Core.Models.Categories;
using TestTask.Core.Models.Companies;
using TestTask.Core.Models.Products;
using TestTask.Core.Models.Types;
using TestTask.MudBlazors.Extension;
using TestTask.MudBlazors.Model.TableComponent;
using TestTask.MudBlazors.Pages.Table.Model;

namespace TestTask.MudBlazors.Dialog.ItemTable
{
    public partial class ProductItemDialog : IItemDialog
    {
        [CascadingParameter] IMudDialogInstance MudDialog { get; set; } = null!;
        [Inject] private ProductRepository ProductRepository { get; set; } = null!;
        [Inject] private CompanyRepository CompanyRepository { get; set; } = null!;
        [Inject] private CategoryRepository CategoryRepository { get; set; } = null!;
        [Inject] private ProductTypeRepository ProductTypeRepository { get; set; } = null!;
        [Inject] private IMessageBox MessageDialog { get; set; } = null!;

        private ProductModel productModel { get; set; } = new ProductModel();
        private string[] errors = { };
        private bool isAddItem = true;
        private bool isDisabledType = true;

        private Product? oldProduct;

        private List<Company> selectCompanies = new List<Company>();
        private List<Category> selectCategories = new List<Category>();
        private List<ProductType> selectTypes = new List<ProductType>();

        [Parameter] public int? Id { get; set; } = null;

        protected override void OnInitialized()
        {
            selectCompanies = CompanyRepository.GetAll();
            selectCategories = CategoryRepository.GetAll();

            if (Id == null)
            {
                isAddItem = true;
                return;
            }

            if (Id <= 0)
            {
                throw new Exception("The ID value can't be less than zero.");
            }

            isAddItem = isDisabledType = false;
            oldProduct = ProductRepository.GetItem((int)Id);
            selectTypes = ProductTypeRepository.GetListTypesByCategory(oldProduct.CategoryId);
            productModel = oldProduct.GetProductModel();
        }

        private void Close() => MudDialog.Cancel();

        private async Task Add()
        {
            if (errors.Length != 0)
            {
                return;
            }

            if (!ValidateFields(out var message))
            {
                await MessageDialog.ShowWarning(message);
                return;
            }

            if (!ProductRepository.IsFreeName(productModel.Name))
            {
                await MessageDialog.ShowWarning("Name is not free.");
                return;
            }

            var product = productModel.GetProductType();
            ProductRepository.Add(product);

            MudDialog.Close();
        }

        private void ClearData() => productModel.ClearData();

        private async Task Updata()
        {
            if (errors.Length != 0)
            {
                return;
            }

            if (!ValidateFields(out var message))
            {
                await MessageDialog.ShowWarning(message);
                return;
            }

            var product = productModel.GetModifyType(oldProduct.Id);

            if (!ProductRepository.IsFreeNameItemUpsert(product))
            {
                await MessageDialog.ShowWarning("Name is not free.");
                return;
            }

            if (!oldProduct.Equals(product))
            {
                ProductRepository.Updata(product);
            }

            MudDialog.Close();
        }

        private void RecoverPastData() => productModel = oldProduct.GetProductModel();

        private void ChangeValueCategory(Category? item)
        {
            productModel.Category = item;

            if (productModel.Category == null || productModel.Category.Types == null)
            {
                isDisabledType = true;
                return;
            }

            isDisabledType = false;
            selectTypes = ProductTypeRepository.GetListTypesByCategory(productModel.Category.Id);
        }

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
