using FluentAssertions;
using TestTask.Core;
using TestTask.Core.Import;
using TestTask.Core.Import.Importers;
using TestTask.Core.Models.Categories;
using TestTask.Core.Models.Types;
using TestTask.Test.Properties;

namespace TestTask.Test.ImportTest
{
    public class TypeImportTest
    {
        public static IEnumerable<object[]> TypeItems() => new List<object[]>
        {
            new object[]
            {
                new List<Category>()
                {
                    new Category("Clothe", 1),
                    new Category("Electronic", 2),
                },
                new List<ProductType>()
                {
                    new ProductType("Coat", 1, 1),
                    new ProductType("Sweater", 1, 2),
                    new ProductType("Shirt", 1, 3),
                    new ProductType("Phone", 2, 4),
                    new ProductType("Laptop", 2, 5),
                },
            }
        };

        public static IEnumerable<object[]> FailReadSheet() => new List<object[]>
        {
            new object[]
            {
                new List<Result<ProductType>>()
                {
                    Result<ProductType>.CreateFail("Failed to read sheet.", 0),
                },
            }
        };

        public static IEnumerable<object[]> FailReadColumn() => new List<object[]>
        {
            new object[]
            {
                new List<Result<ProductType>>()
                {
                    Result<ProductType>.CreateFail("Failed to load title.", 0),
                },
            }
        };

        public static IEnumerable<object[]> FailReadItems() => new List<object[]>
        {
            new object[]
            {
                new List<Result<ProductType>>()
                {
                    Result<ProductType>.CreateFail("Fewer cells than needed", 1),
                    Result<ProductType>.CreateFail("Id should be number", 2),
                    Result<ProductType>.CreateFail("Fewer cells than needed", 3),
                    Result<ProductType>.CreateFail("Fewer cells than needed", 4),
                    Result<ProductType>.CreateFail("Fewer cells than needed", 5),
                },
            }
        };

        [Theory]
        [MemberData(nameof(TypeItems))]
        public void Add_All_Item_From_Excel_File(List<Category> categories, List<ProductType> exceptType)
        {
            //Arrange
            using var dbContext = new TestDbContextFactory().Create();
            var categoryService = new CategoryService(dbContext);
            var typeService = new ProductTypeService(dbContext);

            var memoryStream = new MemoryStream(Resources.DataIsAllFilledIn);
            var typeImporter = new TypeProductImporter();
            var typeRead = new ExcelImporter<ProductType>(typeImporter).Import(memoryStream);

            categoryService.AddRangeAsync(categories);
            foreach (var item in typeRead)
            {
                if (item.Success)
                {
                    typeService.UpsertAsync(item.Value);
                }
            }

            //Act
            var actualCompanies = dbContext.Type.ToList();
            typeRead.Should().AllSatisfy(e => e.Success.Should().BeTrue());

            //Assert
            actualCompanies.Should().Equal(exceptType);
        }

        [Theory]
        [MemberData(nameof(FailReadSheet))]
        public void Reading_File_With_Wrong_Sheet_Name(List<Result<ProductType>>? exceptResult)
        {
            //Arrange
            var memoryStream = new MemoryStream(Resources.NameSheetIsNotCorrect);
            var typeImporter = new TypeProductImporter();

            //Act                                 
            var actualResult = new ExcelImporter<ProductType>(typeImporter).Import(memoryStream);

            //Assert
            actualResult.Should().Equal(exceptResult);
            actualResult.Should().AllSatisfy(e => e.Success.Should().BeFalse());
        }

        [Theory]
        [MemberData(nameof(FailReadColumn))]
        public void Reading_File_With_Wrong_Column_Name(List<Result<ProductType>>? exceptResult)
        {
            //Arrange
            var memoryStream = new MemoryStream(Resources.ColumnNameIsNotCorrect);
            var typeImporter = new TypeProductImporter();

            //Act                                 
            var actualResult = new ExcelImporter<ProductType>(typeImporter).Import(memoryStream);

            //Assert
            actualResult.Should().Equal(exceptResult);
            actualResult.Should().AllSatisfy(e => e.Success.Should().BeFalse());
        }

        [Theory]
        [MemberData(nameof(FailReadItems))]
        public void Reading_File_With_Incorrect_Sheet_Data(List<Result<ProductType>>? exceptResult)
        {
            //Arrange
            var memoryStream = new MemoryStream(Resources.NotCorrectDataIsAllFilledIn);
            var typeImporter = new TypeProductImporter();

            //Act                                 
            var actualResult = new ExcelImporter<ProductType>(typeImporter).Import(memoryStream);

            //Assert
            actualResult.Should().Equal(exceptResult);
            actualResult.Should().AllSatisfy(e => e.Success.Should().BeFalse());
        }
    }
}
