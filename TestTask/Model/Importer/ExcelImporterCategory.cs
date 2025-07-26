using TestTask.Core;
using TestTask.Core.DataTable;
using TestTask.Core.Import;
using TestTask.Core.Models.Categories;

namespace TestTask.Model.Importer
{
    public class ExcelImporterCategory : ExcelImpoterTable<Category>
    {
        public ExcelImporterCategory(IMessageBox messageBox, CategoryService service, ExcelImporter<Category> excelImport)
            : base(messageBox, service, excelImport, Table.Category)
        {
        }
    }
}
