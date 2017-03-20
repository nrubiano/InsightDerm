using System;

namespace InsightDerm.Host.Api
{
	/// <summary>
	/// API settings.
	/// </summary>
	public class ApiSettings
	{
		/// <summary>
		/// Gets or sets the base path for the host api.
		/// </summary>
		/// <value>The base path.</value>
		public string BasePath { get; set; }
		/// <summary>
		/// Gets or sets the api version.
		/// </summary>
		/// <value>The api version.</value>
		public string Version { get; set; }
	}
}
