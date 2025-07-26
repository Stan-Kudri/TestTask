using FluentAssertions;
using TestTask.Core;
using TestTask.Core.Import;
using TestTask.Core.Import.Importers;
using TestTask.Core.Models.Categories;
using TestTask.Test.Properties;

namespace TestTask.Test.ImportTest
{
    public class CategoryImportTest
    {
        public static IEnumerable<object[]> CategoryItems() => new List<object[]>
        {
            new object[]
            {
                new List<Category>()
                {
                    new Category("Clothe", 1),
                    new Category("Electronic", 2),
                },
            }
        };

        public static IEnumerable<object[]> FailReadSheet() => new List<object[]>
        {
            new object[]
            {
                new List<Result<Category>>()
                {
                    Result<Category>.CreateFail("Failed to read sheet.", 0),
                },
            }
        };

        public static IEnumerable<object[]> FailReadColumn() => new List<object[]>
        {
            new object[]
            {
                new List<Result<Category>>()
                {
                    Result<Category>.CreateFail("Failed to load title.", 0),
                },
            }
        };

        public static IEnumerable<object[]> FailReadItems() => new List<object[]>
        {
            new object[]
            {
                new List<Result<Category>>()
                {
                    Result<Category>.CreateFail("Fewer cells than needed", 1),
                    Result<Category>.CreateFail("Id should be number", 2),
                },
            }
        };

        [Theory]
        [MemberData(nameof(CategoryItems))]
        public void Import_Should_Add_All_File_Items(List<Category> exceptCategory)
        {
            //Arrange
            using var dbContext = new TestDbContextFactory().Create();
            var categoryService = new CategoryService(dbContext);
            var memoryStream = new MemoryStream(Resources.DataIsAllFilledIn);
            var categoryImporter = new CategoryImporter();
            var categoryRead = new ExcelImporter<Category>(categoryImporter).Import(memoryStream);

            foreach (var item in categoryRead)
            {
                if (item.Success)
                {
                    categoryService.UpsertAsync(item.Value);
                }
            }

            //Act
            var actualCompanies = dbContext.Category.ToList();

            //Assert
            actualCompanies.Should().Equal(exceptCategory);
            categoryRead.Should().AllSatisfy(e => e.Success.Should().BeTrue());
        }

        [Theory]
        [MemberData(nameof(FailReadSheet))]
        public void Reading_File_With_Wrong_Sheet_Name(List<Result<Category>>? exceptResult)
        {
            //Arrange
            var memoryStream = new MemoryStream(Resources.NameSheetIsNotCorrect);
            var categoryImporter = new CategoryImporter();

            //Act                                 
            var actualResult = new ExcelImporter<Category>(categoryImporter).Import(memoryStream);

            //Assert
            actualResult.Should().Equal(exceptResult);
            actualResult.Should().AllSatisfy(e => e.Success.Should().BeFalse());
        }

        [Theory]
        [MemberData(nameof(FailReadColumn))]
        public void Reading_File_With_Wrong_Column_Name(List<Result<Category>>? exceptResult)
        {
            //Arrange
            var memoryStream = new MemoryStream(Resources.ColumnNameIsNotCorrect);
            var categoryImporter = new CategoryImporter();

            //Act                                 
            var actualResult = new ExcelImporter<Category>(categoryImporter).Import(memoryStream);

            //Assert
            actualResult.Should().Equal(exceptResult);
            actualResult.Should().AllSatisfy(e => e.Success.Should().BeFalse());
        }

        [Theory]
        [MemberData(nameof(FailReadItems))]
        public void Reading_File_With_Incorrect_Sheet_Data(List<Result<Category>>? exceptResult)
        {
            //Arrange
            var memoryStream = new MemoryStream(Resources.NotCorrectDataIsAllFilledIn);
            var categoryImporter = new CategoryImporter();

            //Act                                 
            var actualResult = new ExcelImporter<Category>(categoryImporter).Import(memoryStream);

            //Assert
            actualResult.Should().Equal(exceptResult);
            actualResult.Should().AllSatisfy(e => e.Success.Should().BeFalse());
        }
    }
}
