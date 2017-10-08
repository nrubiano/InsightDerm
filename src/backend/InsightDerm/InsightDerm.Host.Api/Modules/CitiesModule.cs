using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using InsightDerm.Core.Service;
using Microsoft.Extensions.Options;
using Nancy;

namespace InsightDerm.Host.Api.Modules
{
    public class CitiesModule : BaseModule
    {
        readonly CityService _citiesService;

        public CitiesModule(IOptions<ApiSettings> apiSettings, IMapper mapper, CityService citiesService) : base(apiSettings, mapper)
        {
            _citiesService = citiesService;

            Get(GetPath(), (args, ctk) => Get(args, ctk));
            Get($@"{GetPath()}/{{Id}}", (args, ctk) => GetSingle(args, ctk));
        }

        protected virtual async Task<dynamic> Get(dynamic args, CancellationToken ct)
        {
            var entities = _citiesService.GetAll(x => x.Name != string.Empty);

            return Response.AsJson(entities);
        }

		protected virtual async Task<dynamic> GetSingle(dynamic args, CancellationToken ct)
		{
            if(args.Id != null)
            {
                var id = (Guid)args.Id;
                var entity = _citiesService.GetSingle(x => x.Id == id);

                if(entity != null)
				    return Response.AsJson(entity);

                return HttpStatusCode.NotFound;
            }

            return HttpStatusCode.BadRequest;
		}
    }
}
