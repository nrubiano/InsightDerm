using System.Text;
using AutoMapper;
using InsightDerm.Core.Data;
using InsightDerm.Core.Data.Domain.Infrastructure;
using InsightDerm.Core.Data.Domain.Model;
using InsightDerm.Core.Service;
using InsightDerm.Core.Service.Mapping;
using InsightDerm.Host.Backend.Core;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

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

            var key = Encoding.ASCII.GetBytes("y9#tM!D~h?b`*#Kygq4R)J-GJupe:qA8");
            services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });
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
            app.UseAuthentication();
            app.UseMvc();
        }

        private void InitContainer(IServiceCollection container, string connectionString)
        {
            container.AddScoped(x => InitUnitOfWork(connectionString));
            container.AddScoped(x => InitMapper());

            container.AddScoped<CityService>();
            container.AddScoped<ConsultationService>();
            container.AddScoped<DiagnosticImageService>();
            container.AddScoped<DoctorService>();
            container.AddScoped<MaritalStatusService>();
            container.AddScoped<MedicalCenterService>();
            container.AddScoped<PatientService>();
            container.AddScoped<SpecialityService>();
            container.AddScoped<UserService>();
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
