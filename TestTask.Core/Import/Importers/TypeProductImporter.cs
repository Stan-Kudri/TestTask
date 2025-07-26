using System;
using TestTask.Core.Models.Types;

namespace TestTask.Core.Import.Importers
{
    public class TypeProductImporter : BaseImporter<ProductType, ProductTypeField>
    {
        public override bool IsModelSheet(string sheetName)
            => sheetName.Equals(typeof(ProductType).Name, StringComparison.OrdinalIgnoreCase);
    }
}
