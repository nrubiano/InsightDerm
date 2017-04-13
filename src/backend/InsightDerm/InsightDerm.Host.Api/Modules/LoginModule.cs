using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using InsightDerm.Core.Dto;
using InsightDerm.Core.Service;
using Microsoft.Extensions.Options;
using Nancy;

namespace InsightDerm.Host.Api.Controllers
{
	public class LoginModule : BaseModule
	{
	    private readonly CityService _cityService;

		public LoginModule(IOptions<ApiSettings> apiSettings, CityService cityService) : base(apiSettings)
		{
		    _cityService = cityService;

			Get(GetPath(), (args, ctk) => Get(args, ctk));
		}

		protected virtual async Task<dynamic> Get(dynamic args, CancellationToken ct)
		{
		    await _cityService.Create(new CityDto()
		    {
                Name = "Hello Dude"
		    });

		    return "Hello World, it's Nancy on .NET Core";
		}
	}
}
