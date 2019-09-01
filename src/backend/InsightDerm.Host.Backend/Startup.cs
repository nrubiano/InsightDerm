using AutoMapper;
using InsightDerm.Core.Data;
using InsightDerm.Core.Data.Domain.Infrastructure;
using InsightDerm.Core.Service;
using InsightDerm.Core.Service.Mapping;
using InsightDerm.Host.Backend.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InsightDerm.Host.Backend
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
            });
            services.AddOptions();
            services.Configure<ApiSettings>(Configuration.GetSection("ApiSettings"));

            InitContainer(services, Configuration.GetConnectionString("DefaultConnection"));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseCors("CorsPolicy");
            app.UseHttpsRedirection();
            app.UseMvc();
        }

        private void InitContainer(IServiceCollection container, string connectionString)
        {
            container.AddScoped(x => InitUnitOfWork(connectionString));
            container.AddScoped(x => InitMapper());

            container.AddScoped<CityService>();
        }

        private IUnitOfWork InitUnitOfWork(string connectionString)
        {
            var builder = new DbContextOptionsBuilder<InsightDermContext>();
            builder.UseNpgsql(connectionString);

            var context = new InsightDermContext(builder.Options);

            return new UnitOfWork<DbContext>(context);
        }

        private IMapper InitMapper()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile<MappingProfile>();
            });

            config.AssertConfigurationIsValid();

            return config.CreateMapper();
        }
    }
}
