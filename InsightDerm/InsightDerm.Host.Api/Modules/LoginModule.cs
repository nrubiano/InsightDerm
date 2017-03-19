using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Nancy;

namespace InsightDerm.Host.Api.Controllers
{
	public class LoginModule : BaseModule
	{
		public LoginModule(IOptions<ApiSettings> apiSettings) : base(apiSettings)
		{
			Get(GetPath(), (args, ctk) => Get(args, ctk));
		}

		protected virtual async Task<dynamic> Get(dynamic args, CancellationToken ct)
		{
			return "Hello World, it's Nancy on .NET Core";
		}
	}
}
