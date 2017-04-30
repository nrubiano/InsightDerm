using InsightDerm.Core.Service.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.TinyIoc;

namespace InsightDerm.Host.Api
{
	public class Bootstrapper : DefaultNancyBootstrapper
	{
		private IApplicationBuilder _appBuilder;

	    private IConfigurationRoot _configuration;

        public Bootstrapper(IApplicationBuilder appBuilder, IConfigurationRoot configuration)
		{
			_appBuilder = appBuilder;
		    _configuration = configuration;
		}

		protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
		{
			base.ApplicationStartup(container, pipelines);

			var settingsService = (IOptions<ApiSettings>)_appBuilder.ApplicationServices.GetService(typeof(IOptions<ApiSettings>));

			container.Register(settingsService);
            
            ServiceKernel.Init(container, _configuration.GetConnectionString("DefaultConnection"));
		}
	}
}
