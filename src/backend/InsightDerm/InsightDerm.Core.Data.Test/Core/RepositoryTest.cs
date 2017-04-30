using Microsoft.EntityFrameworkCore;
using InsightDerm.Core.Data.Domain.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace InsightDerm.Core.Data.Test.Core
{
    public abstract class RepositoryTest<TEntity> where TEntity : class
    {
        private DbContext _context;

        protected IUnitOfWork UnitOfWork;

        protected IRepository<TEntity> Repository;

        private void InitContext()
        {
            var options = new DbContextOptionsBuilder<InsightDermContext>()                     
                    .UseInMemoryDatabase()                   
                    .Options;

            _context = new InsightDermContext(options);            
            _context.Database.EnsureCreated();            
        }

        private void InitUnitOfWork()
        {
            UnitOfWork = new UnitOfWork<DbContext>(_context);
            Repository = UnitOfWork.Repository<TEntity>();
        }

        [TestInitialize]
        public void Test_InitContext()
        {
            InitContext();

            InitUnitOfWork();

            Test_SeedDatabase();
        }
        

        [TestCleanup]
        public void Test_Clean()
        {           
            UnitOfWork.Dispose();
            _context.Dispose();
        }

        public abstract void Test_SeedDatabase();
    }
}
