using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using InsightDerm.Core.Service;
using Microsoft.Extensions.Options;
using Nancy;

namespace InsightDerm.Host.Api.Controllers
{
    public class LoginModule : BaseModule
	{
	    private readonly CityService _cityService;

		public LoginModule(IOptions<ApiSettings> apiSettings, IMapper mapper, CityService cityService) : base(apiSettings, mapper)
		{
		    _cityService = cityService;

			Get(GetPath(), (args, ctk) => Get(args, ctk));
		}

		protected virtual async Task<dynamic> Get(dynamic args, CancellationToken ct)
		{
		    var cityList = _cityService.GetAll(x => x.Name != "");

		    return Response.AsJson(cityList);
		}
	}
}
