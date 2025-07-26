using System;
using TestTask.Core.Models.Companies;

namespace TestTask.Core.Import.Importers
{
    public class CompanyImporter : BaseImporter<Company, CompanyField>
    {
        public override bool IsModelSheet(string sheetName)
            => sheetName.Equals(typeof(Company).Name, StringComparison.OrdinalIgnoreCase);
    }
}
