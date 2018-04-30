using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using InsightDerm.Core.Service;
using Microsoft.Extensions.Options;
using Nancy;

namespace InsightDerm.Host.Api.Modules
{
    public class MaritalStatusModule : BaseModule
    {
        readonly MaritalStatusService _maritalStatusService;

        public MaritalStatusModule(IOptions<ApiSettings> apiSettings, IMapper mapper, MaritalStatusService maritalStatusService) : base(apiSettings, mapper)
        {
            _maritalStatusService = maritalStatusService;

            Get(GetPath(), (args, ctk) => Get(args, ctk));
            Get($@"{GetPath()}/{{Id}}", (args, ctk) => GetSingle(args, ctk));
        }

        protected virtual async Task<dynamic> Get(dynamic args, CancellationToken ct)
        {
            var entities = _maritalStatusService.GetAll(x => x.Description != string.Empty);

            return Response.AsJson(entities);
        }

		protected virtual async Task<dynamic> GetSingle(dynamic args, CancellationToken ct)
		{
            if(args.Id != null)
            {
                var id = (Guid)args.Id;
                var entity = _maritalStatusService.GetSingle(x => x.Id == id);

                if(entity != null)
				    return Response.AsJson(entity);

                return HttpStatusCode.NotFound;
            }

            return HttpStatusCode.BadRequest;
		}
    }
}
