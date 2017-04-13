using System;
using InsightDerm.Core.Data.Domain.Model;
using InsightDerm.Core.Data.Test.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace InsightDerm.Core.Data.Test
{
    [TestClass]
    public class CityTest : RepositoryTest<City>
    {
        [TestMethod]
        public void Should_Create_New_City()
        {
            var id = new Guid("9ef5bfc4-00a7-4a36-8fcf-e7b9a59ad4b3");

            Repository.InsertAsync(new City
                {
                    Id = id,
                    Name = "Bocaraton"
                }
            );

            UnitOfWork.SaveChangesAsync();

            var result = Repository.FindAsync(id);

            Assert.IsNotNull(result.Result);
        }

        public override void Test_SeedDatabase()
        {
            Repository.InsertAsync(
                new City
                {
                    Id = new Guid("e11504cf-e30a-4a5f-b859-0d9478a0db5d"),
                    Name = "New York"
                },
                new City
                {
                    Id = new Guid("b200e2d4-7ce0-4ff2-8024-b21dc4efe42f"),
                    Name = "Miami"
                }
            );

            UnitOfWork.SaveChangesAsync();
        }
    }
}
