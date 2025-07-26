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
        [Inject] private CompanyService CompanyService { get; set; } = null!;
        [Inject] private CategoryService CategoryService { get; set; } = null!;
        [Inject] private ProductTypeService ProductTypeService { get; set; } = null!;
        [Inject] private ProductService ProductService { get; set; } = null!;
        [Inject] private ExcelImporter<Company> ExcelImportCompany { get; set; } = null!;
        [Inject] private ExcelImporter<Category> ExcelImportCategory { get; set; } = null!;
        [Inject] private ExcelImporter<ProductType> ExcelImportTypeProduct { get; set; } = null!;
        [Inject] private ExcelImporter<Product> ExcelImportProduct { get; set; } = null!;
        [Inject] private NavigationManager Navigation { get; set; } = null!;

        [CascadingParameter] private IMudDialogInstance MudDialog { get; set; } = null!;

        private readonly SelectTable _selectedTable = new SelectTable();

        private void Cancel() => MudDialog.Cancel();

        private async Task UploadData(IBrowserFile fileload)
        {
            if (!_selectedTable.SelectTables.Any() || fileload.Size == decimal.Zero)
            {
                return;
            }

            using var memoryStream = new MemoryStream();
            using var fileStream = fileload.OpenReadStream();
            await fileStream.CopyToAsync(memoryStream);

            MudDialog.Cancel();

            Import(Core.DataTable.Table.Company, memoryStream, ExcelImportCompany, CompanyService);
            Import(Core.DataTable.Table.Category, memoryStream, ExcelImportCategory, CategoryService);
            Import(Core.DataTable.Table.TypeProduct, memoryStream, ExcelImportTypeProduct, ProductTypeService);
            Import(Core.DataTable.Table.Product, memoryStream, ExcelImportProduct, ProductService);

            Navigation.NavigateTo(Navigation.Uri, forceLoad: true);
        }

        private void Import<T>(Core.DataTable.Table table, MemoryStream memoryStream, ExcelImporter<T> excelImporter, BaseService<T> service)
            where T : Entity
        {
            if (!_selectedTable.SelectTables.Contains(table))
            {
                return;
            }

            memoryStream.Position = 0;
            using var nonClosableStream = new NonClosableStream(memoryStream);
            var readItems = excelImporter.Import(nonClosableStream);
            foreach (var row in readItems)
            {
                if (row.Success)
                {
                    service.UpsertAsync(row.Value);
                }
            }
        }
    }
}
