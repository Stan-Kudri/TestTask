using Microsoft.AspNetCore.Components;
using MudBlazor;
using TestTask.Core;
using TestTask.Core.Models.Categories;
using TestTask.Core.Models.Types;
using TestTask.MudBlazors.Extension;
using TestTask.MudBlazors.Model.TableComponent;
using TestTask.MudBlazors.Pages.Table.Model;

namespace TestTask.MudBlazors.Dialog.ItemTable
{
    public partial class ProductTypeItemDialog : IItemDialog
    {
        [CascadingParameter] IMudDialogInstance MudDialog { get; set; } = null!;
        [Inject] private ProductTypeRepository ProductTypeRepository { get; set; } = null!;
        [Inject] private CategoryRepository CategoryRepository { get; set; } = null!;
        [Inject] private IMessageBox MessageDialog { get; set; } = null!;

        private TypeProductModel typeProductModel { get; set; } = new TypeProductModel();
        private string[] errors = { };
        private bool isAddItem = true;

        private ProductType? oldTypeProduct;

        private List<Category> selectCategories = new List<Category>();

        [Parameter] public int? Id { get; set; } = null;

        protected override void OnInitialized()
        {
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

            isAddItem = false;
            oldTypeProduct = ProductTypeRepository.GetItem((int)Id);
            typeProductModel = oldTypeProduct.GetTypeProductModel();
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

            if (!ProductTypeRepository.IsFreeName(typeProductModel.Name))
            {
                await MessageDialog.ShowWarning("Name is not free.");
                return;
            }

            var typeProduct = typeProductModel.GetProductType();
            ProductTypeRepository.Add(typeProduct);

            MudDialog.Close();
        }

        private void ClearData() => typeProductModel.ClearData();

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

            var typeProduct = typeProductModel.GetModifyType(oldTypeProduct.Id);

            if (!ProductTypeRepository.IsFreeNameItemUpsert(typeProduct))
            {
                await MessageDialog.ShowWarning("Name is not free.");
                return;
            }

            if (!oldTypeProduct.Equals(typeProduct))
            {
                ProductTypeRepository.Updata(typeProduct);
            }

            MudDialog.Close();
        }

        private void RecoverPastData() => typeProductModel = oldTypeProduct.GetTypeProductModel();

        private IEnumerable<string> ValidFormatText(string str)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                yield return "Field is required.";
            }
        }

        private bool ValidateFields(out string message)
        {
            message = string.Empty;

            if (typeProductModel.Name == null || typeProductModel.Name == string.Empty)
            {
                message = "Name is required.";
                return false;
            }

            if (typeProductModel.Category == null)
            {
                message = "Category not selected.";
                return false;
            }

            return true;
        }
    }
}
