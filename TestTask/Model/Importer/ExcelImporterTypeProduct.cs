using TestTask.Core;
using TestTask.Core.DataTable;
using TestTask.Core.Import;
using TestTask.Core.Models.Types;

namespace TestTask.Model.Importer
{
    public class ExcelImporterTypeProduct : ExcelImpoterTable<ProductType>
    {
        public ExcelImporterTypeProduct(IMessageBox messageBox, ProductTypeService service, ExcelImporter<ProductType> excelImport)
            : base(messageBox, service, excelImport, Table.TypeProduct)
        {
        }
    }
}
