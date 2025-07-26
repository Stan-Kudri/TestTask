using TestTask.Core;
using TestTask.Core.DataTable;
using TestTask.Core.Import;
using TestTask.Core.Models.Companies;

namespace TestTask.Model.Importer
{
    public class ExcelImporterCompany : ExcelImpoterTable<Company>
    {
        public ExcelImporterCompany(IMessageBox messageBox, CompanyService service, ExcelImporter<Company> excelImport)
            : base(messageBox, service, excelImport, Table.Company)
        {
        }
    }
}
