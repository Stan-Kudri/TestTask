using FluentAssertions;
using TestTask.Core;
using TestTask.Core.Import;
using TestTask.Core.Import.Importers;
using TestTask.Core.Models.Categories;
using TestTask.Core.Models.Companies;
using TestTask.Core.Models.Products;
using TestTask.Core.Models.Types;
using TestTask.Test.Properties;

namespace TestTask.Test.ImportTest
{
    public class ProductImportTest
    {
        public static IEnumerable<object[]> Items() =>
        [
            [
                new List<Company>()
                {
                    new("MF", new DateTime(2000, 6, 7), "Belarus", 1),
                    new("Apple", new DateTime(1973, 7, 12), "USA", 2),
                },
                new List<Category>()
                {
                    new("Clothe", 1),
                    new("Electronic", 2),
                },
                new List<ProductType>()
                {
                    new("Coat", 1, 1),
                    new("Sweater", 1, 2),
                    new("Shirt", 1, 3),
                    new("Phone", 2, 4),
                    new("Laptop", 2, 5),
                },
                new List<Product>()
                {
                    new("Polivuri", 1, 1, 1, "", 235, 4),
                    new("Rick&Morty", 1, 1, 2, "", 15, 5),
                    new("Iphone 13", 1, 2, 4, "", 400, 6),
                },
            ]
        ];

        public static IEnumerable<object[]> FailReadSheet() =>
        [
            [
                new List<Result<Product>>()
                {
                    Result<Product>.CreateFail("Failed to read sheet.", 0),
                },
            ]
        ];

        public static IEnumerable<object[]> FailReadColumn() =>
        [
            [
                new List<Result<Product>>()
                {
                    Result<Product>.CreateFail("Failed to load title.", 0),
                },
            ]
        ];

        public static IEnumerable<object[]> FailReadItems() =>
        [
            [
                new List<Result<Product>>()
                {
                    Result<Product>.CreateFail("Fewer cells than needed", 1),
                    Result<Product>.CreateFail("Price should be number", 2),
                    Result<Product>.CreateFail("Fewer cells than needed", 3),
                },
            ]
        ];

        [Theory]
        [MemberData(nameof(Items))]
        public async Task Add_All_Item_From_Excel_File(List<Company> companies, List<Category> categories, List<ProductType> types, List<Product> exceptProduct)
        {
            //Arrange
            using var dbContext = new TestDbContextFactory().Create();
            var companyService = new CompanyService(dbContext);
            var categoryService = new CategoryService(dbContext);
            var typeService = new ProductTypeService(dbContext);
            var productService = new ProductService(dbContext);

            await companyService.AddRangeAsync(companies);
            await categoryService.AddRangeAsync(categories);
            await typeService.AddRangeAsync(types);

            var memoryStream = new MemoryStream(Resources.DataIsAllFilledIn);
            var productImporter = new ProductImporter();
            var productRead = new ExcelImporter<Product>(productImporter).Import(memoryStream);

            foreach (var item in productRead)
            {
                if (item.Success)
                {
                    await productService.UpsertAsync(item.Value);
                }
            }

            //Act
            var actualProduct = dbContext.Product.ToList();

            //Assert
            actualProduct.Should().Equal(exceptProduct);
            productRead.Should().AllSatisfy(e => e.Success.Should().BeTrue());
        }

        [Theory]
        [MemberData(nameof(FailReadSheet))]
        public void Reading_File_With_Wrong_Sheet_Name(List<Result<Product>>? exceptResult)
        {
            //Arrange
            var memoryStream = new MemoryStream(Resources.NameSheetIsNotCorrect);
            var productImporter = new ProductImporter();

            //Act                                 
            var actualResult = new ExcelImporter<Product>(productImporter).Import(memoryStream);

            //Assert
            actualResult.Should().Equal(exceptResult);
            actualResult.Should().AllSatisfy(e => e.Success.Should().BeFalse());
        }

        [Theory]
        [MemberData(nameof(FailReadColumn))]
        public void Reading_File_With_Wrong_Column_Name(List<Result<Product>>? exceptResult)
        {
            //Arrange
            var memoryStream = new MemoryStream(Resources.ColumnNameIsNotCorrect);
            var productImporter = new ProductImporter();

            //Act                                 
            var actualResult = new ExcelImporter<Product>(productImporter).Import(memoryStream);

            //Assert
            actualResult.Should().Equal(exceptResult);
            actualResult.Should().AllSatisfy(e => e.Success.Should().BeFalse());
        }

        [Theory]
        [MemberData(nameof(FailReadItems))]
        public void Reading_File_With_Incorrect_Sheet_Data(List<Result<Product>>? exceptResult)
        {
            //Arrange
            var memoryStream = new MemoryStream(Resources.NotCorrectDataIsAllFilledIn);
            var productImporter = new ProductImporter();

            //Act                                 
            var actualResult = new ExcelImporter<Product>(productImporter).Import(memoryStream);

            //Assert
            actualResult.Should().Equal(exceptResult);
            actualResult.Should().AllSatisfy(e => e.Success.Should().BeFalse());
        }
    }
}
