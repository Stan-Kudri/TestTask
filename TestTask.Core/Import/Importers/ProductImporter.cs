using System;
using TestTask.Core.Models.Products;

namespace TestTask.Core.Import.Importers
{
    public class ProductImporter : BaseImporter<Product, ProductField>
    {
        public override bool IsModelSheet(string sheetName)
            => sheetName.Equals(typeof(Product).Name, StringComparison.OrdinalIgnoreCase);
    }
}
