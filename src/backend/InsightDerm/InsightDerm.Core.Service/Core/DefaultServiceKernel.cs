using System.Reflection;
using Autofac;
using AutoMapper;
using InsightDerm.Core.Data.Domain.Infrastructure;
using InsightDerm.Core.Service.Mapping;
using Microsoft.EntityFrameworkCore;
using InsightDerm.Core.Data;

namespace InsightDerm.Core.Service.Core
{
    public static class DefaultServiceKernel
    {
        public static void InitContainer(ContainerBuilder container, string connectionString)
        {
            container.Register(x => InitUnitOfWork(connectionString))
                        .As<IUnitOfWork>()
                        .InstancePerRequest();
            
            container.Register(x => InitMapper())
                        .As<IMapper>()
                        .InstancePerRequest();
            
            container.RegisterAssemblyTypes(Assembly.Load("InsightDerm.Core.Service"))
                .Where(t => t.Name.EndsWith("Service"))
                .AsSelf()
                .InstancePerRequest();
        }

        public static IUnitOfWork InitUnitOfWork(string connectionString)
        {
            var builder = new DbContextOptionsBuilder<InsightDermContext>();
            builder.UseNpgsql(connectionString);

            var context = new InsightDermContext(builder.Options);
            
            return new UnitOfWork<DbContext>(context);
        }

        public static IMapper InitMapper()
        {
            var config = new MapperConfiguration(cfg => {                                     
                cfg.AddProfile<MappingProfile>();                
            });
            
            config.AssertConfigurationIsValid();
            
            return config.CreateMapper();
        }
    }
}
