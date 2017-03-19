using System;
using Microsoft.Extensions.Options;
using Nancy;

namespace InsightDerm.Host.Api
{
	public class BaseModule : NancyModule
	{
		private readonly ApiSettings ApiSettings;

		public BaseModule(IOptions<ApiSettings> apiSettings)
		{
			ApiSettings = apiSettings.Value;
		}
		/// <summary>
		/// Build a nice path for us
		/// </summary>
		/// <returns>The path.</returns>
		public string GetPath() 
		{
			var currentClassName = GetType().Name.Replace("Module", string.Empty).ToLowerInvariant();
			return $"{ApiSettings.BasePath}/{ApiSettings.Version}/{currentClassName}";
		}
	}
}
