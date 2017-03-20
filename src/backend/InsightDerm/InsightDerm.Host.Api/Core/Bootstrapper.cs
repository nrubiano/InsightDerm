using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.TinyIoc;

namespace InsightDerm.Host.Api
{
	public class Bootstrapper : DefaultNancyBootstrapper
	{
		IApplicationBuilder _appBuilder;

		public Bootstrapper(IApplicationBuilder appBuilder)
		{
			_appBuilder = appBuilder;
		}

		protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
		{
			base.ApplicationStartup(container, pipelines);

			var settingsService = (IOptions<ApiSettings>)_appBuilder.ApplicationServices.GetService(typeof(IOptions<ApiSettings>));

			container.Register(settingsService);
		}
	}
}
