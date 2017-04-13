using AutoMapper;
using InsightDerm.Core.Data.Domain.Infrastructure;
using InsightDerm.Core.Service.Mapping;
using Microsoft.EntityFrameworkCore;
using Nancy.TinyIoc;

namespace InsightDerm.Core.Service.Core
{
    public static class ServiceKernel
    {
        public static void Init(TinyIoCContainer container, string connectionString)
        {
            InitContainer(container, connectionString);
        }

        private static void InitContainer(TinyIoCContainer container, string connectionString)
        {
            container.Register(InitUnitOfWork(connectionString));
            container.Register(InitMapper());
            container.Register<CityService>();
        }

        private static IUnitOfWork InitUnitOfWork(string connectionString)
        {
            var builder = new DbContextOptionsBuilder<InsightDermContext>();
            builder.UseNpgsql(connectionString);

            var context = new InsightDermContext(builder.Options);

            return new UnitOfWork<DbContext>(context);
        }

        private static IMapper InitMapper()
        {
            var config = new MapperConfiguration(cfg => {                
                cfg.AddProfile<MappingProfile>();
            });
            
            return config.CreateMapper();
        }
    }
}
