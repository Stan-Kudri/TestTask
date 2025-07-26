using FluentAssertions;
using TestTask.Core.Exeption;
using TestTask.Core.Models.Companies;

namespace TestTask.Test.ServiceTest
{
    public class CompanyServiceTest
    {
        public static IEnumerable<object[]> CompanyItems() => new List<object[]>
        {
            new object[]
            {
                new List<Company>()
                {
                    new Company("MF", new DateTime(2002, 10, 15), "Belarus", 1),
                    new Company("Colin's", new DateTime(1983, 7, 12), "Turkey", 2),
                    new Company("Belvest", new DateTime(2005, 2, 13), "Belarus", 3),
                    new Company("Bershka", new DateTime(2002, 4, 22), "Spain", 4),
                    new Company("Apple", new DateTime(1976, 4, 1), "USA", 5),
                    new Company("HP", new DateTime(1939, 1, 1), "USA", 6),
                },
            }
        };

        public static IEnumerable<object[]> AddItemCompany() => new List<object[]>
        {
            new object[]
            {
                new List<Company>()
                {
                    new Company("MF", new DateTime(2002, 10, 15), "Belarus", 1),
                    new Company("Colin's", new DateTime(1983, 7, 12), "Turkey", 2),
                    new Company("Belvest", new DateTime(2005, 2, 13), "Belarus", 3),
                    new Company("Bershka", new DateTime(2002, 4, 22), "Spain", 4),
                    new Company("Apple", new DateTime(1976, 4, 1), "USA", 5),
                },
                new Company("HP", new DateTime(1939, 1, 1), "USA", 6),
                new List<Company>()
                {
                    new Company("MF", new DateTime(2002, 10, 15), "Belarus", 1),
                    new Company("Colin's", new DateTime(1983, 7, 12), "Turkey", 2),
                    new Company("Belvest", new DateTime(2005, 2, 13), "Belarus", 3),
                    new Company("Bershka", new DateTime(2002, 4, 22), "Spain", 4),
                    new Company("Apple", new DateTime(1976, 4, 1), "USA", 5),
                    new Company("HP", new DateTime(1939, 1, 1), "USA", 6),
                },
            },
        };

        public static IEnumerable<object[]> UpdateItemCompany() => new List<object[]>
        {
            new object[]
            {
                new List<Company>()
                {
                    new Company("MF", new DateTime(2002, 10, 15), "Belarus", 1),
                    new Company("Colin's", new DateTime(1983, 7, 12), "Turkey", 2),
                    new Company("Belvest", new DateTime(2005, 2, 13), "Belarus", 3),
                    new Company("Bershka", new DateTime(2002, 4, 22), "Spain", 4),
                    new Company("Apple", new DateTime(1976, 4, 1), "USA", 5),
                    new Company("HP", new DateTime(1939, 1, 1), "USA", 6),
                },
                new Company("MSI", new DateTime(1986, 8, 4), "Taiwan", 6),
                new List<Company>()
                {
                    new Company("MF", new DateTime(2002, 10, 15), "Belarus", 1),
                    new Company("Colin's", new DateTime(1983, 7, 12), "Turkey", 2),
                    new Company("Belvest", new DateTime(2005, 2, 13), "Belarus", 3),
                    new Company("Bershka", new DateTime(2002, 4, 22), "Spain", 4),
                    new Company("Apple", new DateTime(1976, 4, 1), "USA", 5),
                    new Company("MSI", new DateTime(1986, 8, 4), "Taiwan", 6),
                },
            },
        };

        public static IEnumerable<object[]> RemoveRangeCompany() => new List<object[]>
        {
            new object[]
            {
                new List<Company>()
                {
                    new Company("MF", new DateTime(2002, 10, 15), "Belarus", 1),
                    new Company("Colin's", new DateTime(1983, 7, 12), "Turkey", 2),
                    new Company("Belvest", new DateTime(2005, 2, 13), "Belarus", 3),
                    new Company("Bershka", new DateTime(2002, 4, 22), "Spain", 4),
                    new Company("Apple", new DateTime(1976, 4, 1), "USA", 5),
                    new Company("HP", new DateTime(1939, 1, 1), "USA", 6),
                },
                new List<int> { 2, 3, 6 },
                new List<Company>()
                {
                    new Company("MF", new DateTime(2002, 10, 15), "Belarus", 1),
                    new Company("Bershka", new DateTime(2002, 4, 22), "Spain", 4),
                    new Company("Apple", new DateTime(1976, 4, 1), "USA", 5),
                },
            },
        };

        public static IEnumerable<object[]> AddCompanyWithBusyId() => new List<object[]>
        {
            new object[]
            {
                new List<Company>()
                {
                    new Company("MF", new DateTime(2002, 10, 15), "Belarus", 1),
                    new Company("Colin's", new DateTime(1983, 7, 12), "Turkey", 2),
                    new Company("Belvest", new DateTime(2005, 2, 13), "Belarus", 3),
                    new Company("Bershka", new DateTime(2002, 4, 22), "Spain", 4),
                    new Company("Apple", new DateTime(1976, 4, 1), "USA", 5),
                },
                new Company("HP", new DateTime(1939, 1, 1), "USA", 3),
            },
        };

        public static IEnumerable<object[]> NameCompanyById() => new List<object[]>
        {
            new object[]
            {
                new Company("MF", new DateTime(2002, 10, 15), "Belarus", 1),
                1,
                "MF",
            },
        };

        [Theory]
        [MemberData(nameof(CompanyItems))]
        public async Task Service_Should_Add_All_The_Item_Of_Database(List<Company> companies)
        {
            //Arrange
            using var dbContext = new TestDbContextFactory().Create();
            var companyRepository = new CompanyService(dbContext);
            await companyRepository.AddRangeAsync(companies);

            //Act
            var actualCompanies = dbContext.Company.ToList();

            //Assert
            actualCompanies.Should().Equal(companies);
        }

        [Theory]
        [MemberData(nameof(AddItemCompany))]
        public async Task Service_Should_Add_The_Item_To_The_Database(List<Company> companies, Company addCompany, List<Company> expectCompanies)
        {
            //Arrange
            using var dbContext = new TestDbContextFactory().Create();
            var companyRepository = new CompanyService(dbContext);
            dbContext.Company.AddRange(companies);
            await dbContext.SaveChangesAsync();
            await companyRepository.AddAsync(addCompany);

            //Act
            var actualCompanies = dbContext.Company.ToList();

            //Assert
            actualCompanies.Should().Equal(expectCompanies);
        }

        [Theory]
        [MemberData(nameof(UpdateItemCompany))]
        public async Task Service_Should_Update_The_Item_To_The_Database(List<Company> companies, Company updateCompany, List<Company> expectCompanies)
        {
            //Arrange
            using var dbContext = new TestDbContextFactory().Create();
            var companyRepository = new CompanyService(dbContext);
            dbContext.Company.AddRange(companies);
            await dbContext.SaveChangesAsync();
            await companyRepository.UpdataAsync(updateCompany);

            //Act
            var actualCompanies = dbContext.Company.ToList();

            //Assert
            actualCompanies.Should().Equal(expectCompanies);
        }

        [Theory]
        [MemberData(nameof(RemoveRangeCompany))]
        public async Task Service_Should_Remove_Range_Items_By_ID_To_The_Database(List<Company> companies, List<int> removeID, List<Company> expectCompanies)
        {
            //Arrange
            using var dbContext = new TestDbContextFactory().Create();
            var companyRepository = new CompanyService(dbContext);
            dbContext.Company.AddRange(companies);
            await dbContext.SaveChangesAsync();
            await companyRepository.RemoveRangeAsync(removeID);

            //Act
            var actualCompanies = dbContext.Company.ToList();

            //Assert
            actualCompanies.Should().Equal(expectCompanies);
        }

        [Theory]
        [MemberData(nameof(NameCompanyById))]
        public async Task Get_Name_By_ID_From_The_Database(Company company, int id, string expectName)
        {
            //Arrange
            using var dbContext = new TestDbContextFactory().Create();
            var companyRepository = new CompanyService(dbContext);
            await companyRepository.AddAsync(company);

            //Act
            var actualName = companyRepository.CompanyName(id);

            //Assert
            actualName.Equals(expectName);
        }

        [Theory]
        [MemberData(nameof(AddCompanyWithBusyId))]
        public async Task Add_Items_Did_Not_Happen_Because_The_ID_Are_Busy(List<Company> companies, Company company)
        {
            //Arrange
            using var dbContext = new TestDbContextFactory().Create();
            var companyRepository = new CompanyService(dbContext);
            await companyRepository.AddRangeAsync(companies);

            //Assert
            await Assert.ThrowsAsync<BusinessLogicException>(async () => { await companyRepository.AddAsync(company); });
        }
    }
}
