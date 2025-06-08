using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using TestTask.Core.DataTable;
using TestTask.Core.Import;
using TestTask.Core.Models;
using TestTask.Core.Models.Categories;
using TestTask.Core.Models.Companies;
using TestTask.Core.Models.Products;
using TestTask.Core.Models.Types;

namespace TestTask.MudBlazors.Pages.Import
{
    public partial class DialogImportData
    {
        [Inject] private CompanyRepository CompanyRepository { get; set; } = null!;
        [Inject] private CategoryRepository CategoryRepository { get; set; } = null!;
        [Inject] private ProductTypeRepository ProductTypeRepository { get; set; } = null!;
        [Inject] private ProductRepository ProductRepository { get; set; } = null!;
        [Inject] private ExcelImporter<Company> ExcelImportCompany { get; set; } = null!;
        [Inject] private ExcelImporter<Category> ExcelImportCategory { get; set; } = null!;
        [Inject] private ExcelImporter<ProductType> ExcelImportTypeProduct { get; set; } = null!;
        [Inject] private ExcelImporter<Product> ExcelImportProduct { get; set; } = null!;
        [Inject] private NavigationManager Navigation { get; set; } = null!;

        [CascadingParameter] private IMudDialogInstance MudDialog { get; set; } = null!;

        private SelectTable selectedTable = new SelectTable();

        private void Cancel() => MudDialog.Cancel();

        private async Task UploadData(IBrowserFile fileload)
        {
            if (selectedTable.SelectTables.Count() == 0 || fileload.Size == decimal.Zero)
            {
                return;
            }

            using var memoryStream = new MemoryStream();
            using var fileStream = fileload.OpenReadStream();
            await fileStream.CopyToAsync(memoryStream);

            MudDialog.Cancel();

            Import(Tables.Company, memoryStream, ExcelImportCompany, CompanyRepository);
            Import(Tables.Category, memoryStream, ExcelImportCategory, CategoryRepository);
            Import(Tables.TypeProduct, memoryStream, ExcelImportTypeProduct, ProductTypeRepository);
            Import(Tables.Product, memoryStream, ExcelImportProduct, ProductRepository);

            Navigation.NavigateTo(Navigation.Uri, forceLoad: true);
        }

        private void Import<T>(Tables table, MemoryStream memoryStream, ExcelImporter<T> excelImporter, IRepository<T> repository)
            where T : Entity
        {
            if (!selectedTable.SelectTables.Contains(table))
            {
                return;
            }

            memoryStream.Position = 0;
            var readItems = excelImporter.Import(new NonClosableStream(memoryStream));
            foreach (var row in readItems)
            {
                if (row.Success)
                {
                    repository.Upsert(row.Value);
                }
            }
        }
    }
}
