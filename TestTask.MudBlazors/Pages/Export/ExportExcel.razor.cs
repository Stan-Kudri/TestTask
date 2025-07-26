using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;
using NPOI.XSSF.UserModel;
using TestTask.Core.DataTable;
using TestTask.Core.Export.SheetFillers;
using TestTask.Core.Import;
using ItemSelected = TestTask.Core.DataTable.Table;

namespace TestTask.MudBlazors.Pages.Export
{
    public partial class ExportExcel
    {
        [Inject] private CompanySheetFiller CompanySheetFiller { get; set; } = null!;
        [Inject] private CategorySheetFiller CategorySheetFiller { get; set; } = null!;
        [Inject] private TypeSheetFiller TypeSheetFiller { get; set; } = null!;
        [Inject] private ProductSheetFiller ProductSheetFiller { get; set; } = null!;
        [Inject] private IJSRuntime JS { get; set; } = null!;

        [CascadingParameter] private IMudDialogInstance MudDialog { get; set; } = null!;

        private readonly SelectTable selectedTable = new SelectTable();

        private void Cancel() => MudDialog.Cancel();

        private async Task DownloadFile()
        {
            if (!selectedTable.SelectTables.Any())
            {
                return;
            }

            var fillers = SelectExportTable();

            using var workbook = new XSSFWorkbook();
            foreach (var filler in fillers)
            {
                var sheet = workbook.CreateSheet(filler.Name);
                await filler.Fill(sheet);
            }

            using var root = new MemoryStream();
            using var XSSFStream = new NonClosableStream(root);
            workbook.Write(XSSFStream);
            XSSFStream.Position = 0;

            byte[] bytes = root.ToArray();
            await JS.InvokeVoidAsync("BlazorDownloadFile", "Export.xlsx", bytes);
        }

        private ISheetFiller[] SelectExportTable()
        {
            var fillers = new List<ISheetFiller>();

            if (selectedTable.SelectTables.Contains(ItemSelected.Company))
            {
                fillers.Add(CompanySheetFiller);
            }
            if (selectedTable.SelectTables.Contains(ItemSelected.Category))
            {
                fillers.Add(CategorySheetFiller);
            }
            if (selectedTable.SelectTables.Contains(ItemSelected.TypeProduct))
            {
                fillers.Add(TypeSheetFiller);
            }
            if (selectedTable.SelectTables.Contains(ItemSelected.Product))
            {
                fillers.Add(ProductSheetFiller);
            }

            return fillers.ToArray();
        }
    }
}
