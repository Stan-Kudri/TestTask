using TestTask.Core;
using TestTask.Core.DataTable;
using TestTask.Core.Import;
using TestTask.Core.Models.Products;

namespace TestTask.Model.Importer
{
    public class ExcelImporterProduct : ExcelImpoterTable<Product>
    {
        public ExcelImporterProduct(IMessageBox messageBox, ProductService service, ExcelImporter<Product> excelImport)
            : base(messageBox, service, excelImport, Table.Product)
        {
        }
    }
}
