using Microsoft.AspNetCore.Components;
using MudBlazor;
using TestTask.Core;
using TestTask.Core.Models.Categories;
using TestTask.MudBlazors.Extension;
using TestTask.MudBlazors.Model.TableComponent;
using TestTask.MudBlazors.Pages.Table.Model;

namespace TestTask.MudBlazors.Dialog.ItemTable
{
    public partial class CategoryItemDialog : IItemDialog
    {
        private const string ErrorFieldRequired = "Field is required.";

        [CascadingParameter] IMudDialogInstance MudDialog { get; set; } = null!;
        [Inject] private CategoryRepository CategoryRepository { get; set; } = null!;
        [Inject] private IMessageBox MessageDialog { get; set; } = null!;

        private CategoryModel categoryModel { get; set; } = new CategoryModel();
        private string[] errors = { };
        private bool isAddItem = true;

        private Category? oldItem;

        [Parameter] public int? Id { get; set; } = null;

        protected override void OnInitialized()
        {
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
            oldItem = CategoryRepository.GetCategory((int)Id);
            categoryModel = oldItem.GetCategoryModel();
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

            if (!CategoryRepository.IsFreeName(categoryModel.Name))
            {
                await MessageDialog.ShowWarning("Name is not free.");
                return;
            }

            var item = categoryModel.GetCategory();
            CategoryRepository.Add(item);

            MudDialog.Close();
        }

        private void ClearData() => categoryModel.ClearData();

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

            var item = categoryModel.GetModifyCategory(oldItem.Id);

            if (!CategoryRepository.IsFreeNameItemUpsert(item))
            {
                await MessageDialog.ShowWarning("Name is not free.");
                return;
            }

            if (!oldItem.Equals(item))
            {
                CategoryRepository.Updata(item);
            }

            MudDialog.Close();
        }

        private void RecoverPastData() => categoryModel = oldItem.GetCategoryModel();

        private IEnumerable<string> ValidFormatText(string str)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                yield return ErrorFieldRequired;
            }
        }

        private bool ValidateFields(out string message)
        {
            message = string.Empty;

            if (string.IsNullOrWhiteSpace(categoryModel.Name))
            {
                message = "Name is required.";
                return false;
            }

            return true;
        }
    }
}
