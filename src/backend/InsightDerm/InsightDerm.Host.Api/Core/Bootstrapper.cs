﻿using Autofac;
using InsightDerm.Core.Service.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;
using Nancy;
using Nancy.Bootstrappers.Autofac;
using Ninject;

namespace InsightDerm.Host.Api
{
    public class Bootstrapper : AutofacNancyBootstrapper
    {
        IApplicationBuilder _appBuilder;

        IConfigurationRoot _configuration;

        public Bootstrapper(IApplicationBuilder appBuilder, IConfigurationRoot configuration)
        {
            _appBuilder = appBuilder;
            _configuration = configuration;
        }
        
        protected override void ConfigureApplicationContainer(ILifetimeScope existingContainer)
        {
            var builder = new ContainerBuilder();
            
            var settingsService = (IOptions<ApiSettings>)_appBuilder.ApplicationServices.GetService(typeof(IOptions<ApiSettings>));

            builder.RegisterInstance(settingsService).As<IOptions<ApiSettings>>();
            
            ServiceKernel.Init(builder, _configuration.GetConnectionString("DefaultConnection"));
            
            builder.Update(existingContainer.ComponentRegistry);
        }
    }
}
