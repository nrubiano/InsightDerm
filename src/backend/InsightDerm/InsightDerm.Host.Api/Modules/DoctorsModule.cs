using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using InsightDerm.Core.Service;
using Microsoft.Extensions.Options;
using Nancy;

namespace InsightDerm.Host.Api.Controllers
{
    public class DoctorsModule : BaseModule
	{
		private readonly DoctorService _doctorService;

		public DoctorsModule(IOptions<ApiSettings> apiSettings, IMapper mapper, DoctorService doctorService) : base(apiSettings, mapper)
		{
			_doctorService = doctorService;

			Get(GetPath(), (args, ctk) => Get(args, ctk));
		}

		protected virtual async Task<dynamic> Get(dynamic args, CancellationToken ct)
		{
			var entities = _doctorService.GetAll(x => x.Name != "");

			return Response.AsJson(entities);
		}
	}
}
