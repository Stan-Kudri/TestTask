using System;
using TestTask.Core.Models.Categories;

namespace TestTask.Core.Import.Importers
{
    public class CategoryImporter : BaseImporter<Category, CategoryField>
    {
        public override bool IsModelSheet(string sheetName)
            => sheetName.Equals(typeof(Category).Name, StringComparison.OrdinalIgnoreCase);
    }
}
