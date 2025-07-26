using FluentAssertions;
using TestTask.Core.Exeption;
using TestTask.Core.Models.Categories;
using TestTask.Core.Models.Types;

namespace TestTask.Test.ServiceTest
{
    public class TypeProductServiceTest
    {

        public static IEnumerable<object[]> TypeProductItems() => new List<object[]>
        {
            new object[]
            {
                new List<Category>()
                {
                    new Category("Food", 1),
                    new Category("Cloth", 2),
                },
                new List<ProductType>()
                {
                    new ProductType("Meat", 1, 1),
                    new ProductType("T-shirt", 2, 2),
                    new ProductType("Sweater", 2, 3),
                },
            }
        };

        public static IEnumerable<object[]> AddItemTypeProduct() => new List<object[]>
        {
            new object[]
            {
                new List<Category>()
                {
                    new Category("Food", 1),
                    new Category("Cloth", 2),
                },
                new List<ProductType>()
                {
                    new ProductType("Meat", 1, 1),
                    new ProductType("T-shirt", 2, 2),
                },
                new ProductType("Sweater", 2, 3),
                new List<ProductType>()
                {
                    new ProductType("Meat", 1, 1),
                    new ProductType("T-shirt", 2, 2),
                    new ProductType("Sweater", 2, 3),
                },
            },
        };

        public static IEnumerable<object[]> UpdateItemTypeProduct() => new List<object[]>
        {
            new object[]
            {
                new List<Category>()
                {
                    new Category("Food", 1),
                    new Category("Cloth", 2),
                },
                new List<ProductType>()
                {
                    new ProductType("Meat", 1, 1),
                    new ProductType("T-shirt", 2, 2),
                    new ProductType("Sweater", 2, 3),
                },
                new ProductType("Shirt", 2, 3),
                new List<ProductType>()
                {
                    new ProductType("Meat", 1, 1),
                    new ProductType("T-shirt", 2, 2),
                    new ProductType("Shirt", 2, 3),
                },
            },
        };

        public static IEnumerable<object[]> RemoveTypeProduct() => new List<object[]>
        {
            new object[]
            {
                new List<Category>()
                {
                    new Category("Food", 1),
                    new Category("Cloth", 2),
                },
                new List<ProductType>()
                {
                    new ProductType("Meat", 1, 1),
                    new ProductType("T-shirt", 2, 2),
                    new ProductType("Sweater", 2, 3),
                },
                2,
                new List<ProductType>()
                {
                    new ProductType("Meat", 1, 1),
                    new ProductType("Sweater", 2, 3),
                },
            },
        };

        public static IEnumerable<object[]> AddTypeProductWithBusyId() => new List<object[]>
        {
            new object[]
            {
                new List<Category>()
                {
                    new Category("Food", 1),
                    new Category("Cloth", 2),
                },
                new List<ProductType>()
                {
                    new ProductType("Meat", 1, 1),
                    new ProductType("T-shirt", 2, 2),
                    new ProductType("Sweater", 2, 3),
                },
                new ProductType("Shirt", 2, 2),
            },
        };

        public static IEnumerable<object[]> AddTypeProductWithCategoryMissing() => new List<object[]>
        {
            new object[]
            {
                new List<Category>()
                {
                    new Category("Food", 1),
                    new Category("Cloth", 2),
                },
                new ProductType("Shirt", 3, 1),
            },
        };

        [Theory]
        [MemberData(nameof(TypeProductItems))]
        public async Task Service_Should_Add_All_The_Item_Of_Database(List<Category> categories, List<ProductType> types)
        {
            //Arrange
            using var dbContext = new TestDbContextFactory().Create();
            var categoryService = new CategoryService(dbContext);
            var typeProductService = new ProductTypeService(dbContext);
            await categoryService.AddRangeAsync(categories);
            await typeProductService.AddRangeAsync(types);

            //Act
            var actualType = dbContext.Type.ToList();

            //Assert
            actualType.Should().Equal(types);
        }

        [Theory]
        [MemberData(nameof(AddItemTypeProduct))]
        public async Task Service_Should_Add_The_Item_To_The_Database(List<Category> categories, List<ProductType> types, ProductType addType, List<ProductType> expectTypes)
        {
            //Arrange
            using var dbContext = new TestDbContextFactory().Create();
            var categoryService = new CategoryService(dbContext);
            var typeProductService = new ProductTypeService(dbContext);
            await categoryService.AddRangeAsync(categories);
            await typeProductService.AddRangeAsync(types);
            await typeProductService.AddAsync(addType);

            //Act
            var actualType = dbContext.Type.ToList();

            //Assert
            actualType.Should().Equal(expectTypes);
        }

        [Theory]
        [MemberData(nameof(UpdateItemTypeProduct))]
        public async Task Service_Should_Update_The_Item_To_The_Database(List<Category> categories, List<ProductType> types, ProductType updateType, List<ProductType> expectTypes)
        {
            //Arrange
            using var dbContext = new TestDbContextFactory().Create();
            var categoryService = new CategoryService(dbContext);
            var typeProductService = new ProductTypeService(dbContext);
            await categoryService.AddRangeAsync(categories);
            await typeProductService.AddRangeAsync(types);
            await typeProductService.UpdataAsync(updateType);

            //Act
            var actualType = dbContext.Type.ToList();

            //Assert
            actualType.Should().Equal(expectTypes);
        }

        [Theory]
        [MemberData(nameof(RemoveTypeProduct))]
        public async Task Service_Should_Remove_Item_By_ID_To_The_Database(List<Category> categories, List<ProductType> types, int removeID, List<ProductType> expectTypes)
        {
            //Arrange
            using var dbContext = new TestDbContextFactory().Create();
            var categoryService = new CategoryService(dbContext);
            var typeProductService = new ProductTypeService(dbContext);
            await categoryService.AddRangeAsync(categories);
            await typeProductService.AddRangeAsync(types);
            await typeProductService.RemoveAsync(removeID);

            //Act
            var actualType = dbContext.Type.ToList();

            //Assert
            actualType.Should().Equal(expectTypes);
        }

        [Theory]
        [MemberData(nameof(AddTypeProductWithBusyId))]
        public async Task Add_Items_Did_Not_Happen_Because_The_ID_Are_Busy(List<Category> categories, List<ProductType> types, ProductType addType)
        {
            //Arrange
            using var dbContext = new TestDbContextFactory().Create();
            var categoryService = new CategoryService(dbContext);
            var typeProductService = new ProductTypeService(dbContext);
            await categoryService.AddRangeAsync(categories);
            await typeProductService.AddRangeAsync(types);

            //Act & Assert
            await Assert.ThrowsAsync<BusinessLogicException>(async () => { await typeProductService.AddAsync(addType); });
        }

        [Theory]
        [MemberData(nameof(AddTypeProductWithCategoryMissing))]
        public async Task Did_Not_Happen_Add_Items_Because_The_Missing_Child_Id(List<Category> categories, ProductType addType)
        {
            //Arrange
            using var dbContext = new TestDbContextFactory().Create();
            var categoryService = new CategoryService(dbContext);
            var typeProductService = new ProductTypeService(dbContext);
            await categoryService.AddRangeAsync(categories);

            //Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(async () => { await typeProductService.AddAsync(addType); });
        }
    }
}
