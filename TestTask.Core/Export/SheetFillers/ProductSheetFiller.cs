using TestTask.Core.Models.Products;

namespace TestTask.Core.Export.SheetFillers
{
    public class ProductSheetFiller : SheetFiller<Product, ProductField>
    {
        public ProductSheetFiller(ProductService service) : base(service)
        {
        }
    }
}
